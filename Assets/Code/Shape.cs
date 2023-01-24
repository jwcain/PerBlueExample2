using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bestagon.Hexagon;
using System.Linq;


public class Shape
{
    public bool isKeyShape;
    [SerializeField]
    public List<Piece> pieces = new List<Piece>();
    public List<Piece> Anchors => new List<Piece>(pieces.Where(p => p.isAnchor).ToArray());

    static readonly Hex.Side[] sideRef = new Hex.Side[] { Hex.Side.DownRight, Hex.Side.DownLeft, Hex.Side.Left, Hex.Side.UpLeft, Hex.Side.UpRight, Hex.Side.Right };

    private GameObject visualOBJ;
    public static int createdShapeCount = 0;

    public Transform transform => visualOBJ.transform;

    [System.Serializable]
    public class Piece
    {
        [SerializeField] public Hex position;
        [SerializeField] public bool isAnchor;
        [System.NonSerialized] public Shape shape;
        [System.NonSerialized] public SpriteRenderer renderer;
    }

    public void DestroyVisuals()
    {
        if (visualOBJ != null)
            GameObject.Destroy(visualOBJ);
    }

    public void UpdateColor()
    {
        foreach (var piece in pieces)
        {
            piece.renderer.color = isKeyShape ? LevelLoadingOperations.Instance.keyShapeColor : LevelLoadingOperations.Instance.baseShapeColor;
        }
    }


    public Hex[] PositionsAfterSlide(Hex.Side slideDir)
    {
        return pieces.Select(piece => piece.position + slideDir.Offset()).ToArray();
    }

    public void Slide(Hex.Side slideDir)
    {
        foreach (var piece in pieces)
        {
            piece.renderer.gameObject.name = piece.position.ToString();
            piece.position += slideDir.Offset();
        }
    }

    public void Rotate(Hex about, bool clockwise, bool updateVisuals = true)
    {
        foreach (var piece in pieces)
            piece.position = Hex.Rotate(about, piece.position, clockwise);

        if (updateVisuals)
            UpdateVisuals();
    }

    private static Hex[] PositionalSweep(Hex about, Hex[] positions, bool clockwise)
    {
        List<Hex> results = new List<Hex>();
        foreach (var position in positions)
        {
            foreach (var hex in Hex.RotationSweep(about, position, clockwise))
            {
                results.Add(hex);
            }
        }

        return results.ToArray();
    }

    public Hex.Side[] ValidRotationSidesInDirection(Piece anchor, Piece handle, bool clockwise)
    {
        List<Hex.Side> validSides = new List<Hex.Side>();
        Hex.Side currentSide;
        //Figure out what the current orientation is between anchors and set that as the 'current orientation'
        Hex.GetRelativeSide(anchor.position, handle.position, out currentSide, out _);
        validSides.Add(currentSide);
        //  Create a frame of reference set of positions based on the starting orientation
        Hex[] piecePositions = pieces.Select(piece => piece.position).ToArray();
        //Start a loop
        int safteyIter = 32;
        while (safteyIter-- > 0)
        {
            Hex[] rotationsCollisionHexes = PositionalSweep(anchor.position, piecePositions, clockwise);
            if (LevelData.openLevel.CheckCollisions(this, rotationsCollisionHexes, out var _) != LevelData.CollisionType.None)
            {
                //There was a collision, we are done, break the loop
                break;
            }
            else
            {
                currentSide = currentSide.RotationalExpansion(1, clockwise)[0];
                //Stop if we've been here before
                if (validSides.Contains(currentSide))
                    break;
                validSides.Add(currentSide);
                piecePositions = piecePositions.Select(position => Hex.Rotate(anchor.position, position, clockwise)).ToArray();
            }

        }
        return validSides.ToArray();
    }

    public void CreateVisuals()
    {
        DestroyVisuals();
        visualOBJ = new GameObject();
        visualOBJ.name = string.Format("Shape {0}", createdShapeCount++);


        foreach (var piece in pieces)
        {
            //Calculate the adjacent ones into a UID connection
            string UID = "";
            for (int h = 0; h < 6; h++)
            {
                UID += pieces.Any((Piece p) => p.position.Equals(piece.position + sideRef[h].Offset())) ? "1" : "0";
            }

            piece.renderer = MakeSpriteOBJ();
            piece.renderer.gameObject.name = piece.position.ToString();
            piece.renderer.sprite = Resources.Load<Sprite>("PieceSprites/piece_" + UID);
            piece.renderer.transform.localPosition = piece.position.UnityPosition();
            piece.renderer.sortingLayerName = "Shapes";
            piece.renderer.color = isKeyShape ? LevelLoadingOperations.Instance.keyShapeColor : LevelLoadingOperations.Instance.baseShapeColor;
            if (piece.isAnchor)
            {
                var anchorRenderer = MakeSpriteOBJ();
                anchorRenderer.transform.parent = piece.renderer.gameObject.transform;
                anchorRenderer.sprite = Resources.Load<Sprite>("PieceSprites/piece_000000");
                anchorRenderer.transform.localPosition = Vector3.zero;
                anchorRenderer.color = Color.black;
                anchorRenderer.sortingLayerName = "Shapes";
                anchorRenderer.sortingOrder = piece.renderer.sortingOrder + 1;
                anchorRenderer.transform.localScale = Vector3.one * 0.66f;
            }
        }
    }

    internal void UpdateVisuals()
    {
        foreach (var piece in pieces)
        {
            piece.renderer.transform.position = piece.position.UnityPosition();
            string UID = "";
            for (int h = 0; h < 6; h++)
            {
                UID += pieces.Any((Piece p) => p.position.Equals(piece.position + sideRef[h].Offset())) ? "1" : "0";
            }
            piece.renderer.sprite = Resources.Load<Sprite>("PieceSprites/piece_" + UID);
        }
    }

    SpriteRenderer MakeSpriteOBJ()
    {
        GameObject obj = new GameObject();
        obj.transform.parent = visualOBJ.transform;
        return obj.AddComponent<SpriteRenderer>();
    }

    public Shape Clone()
    {
        Shape clone = new Shape();
        clone.isKeyShape = isKeyShape;
        foreach (var piece in pieces)
        {
            clone.pieces.Add(new Piece() { isAnchor = piece.isAnchor, position = piece.position, shape = clone });
        }

        return clone;
    }
}
