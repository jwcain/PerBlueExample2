using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bestagon.Hexagon;
using System.Linq;

public class PlayerController : Bestagon.Behaviours.SingletonBehaviour<PlayerController>
{
    Shape.Piece heldPiece = null;
    Shape.Piece mainAnchor = null;
    Shape.Piece secondAnchor = null;
    Hex.Side anchorSide = Hex.Side.Right;

    float piecePickupRange = .5f;
    float maxCollisionDistPercent = .28f;
    public float slideSpeed = 1f;

    [EditorReadOnly, SerializeField] Hex hoverHex;

    enum PlayerState { Free, Rotating, Sliding }
    [EditorReadOnly, SerializeField] PlayerState playerState = PlayerState.Free;

    Hex.Side[] counterClockwiseRotations;
    Hex.Side[] clockwiseRotations;


    // Update is called once per frame
    public void Update()
    {
        hoverHex = MouseInfo.Hex();
        switch (playerState)
        {
            default:
            case PlayerState.Free:
                HandleFreeState();
                break;
            case PlayerState.Rotating:
                HandleRotatingState();
                break;
            case PlayerState.Sliding:
                HandleSlidingState();
                break;
        }
    }

    void HandleFreeState()
    {
        if (Input.GetMouseButtonDown(0) && TryGrabPiece(false))
        {
            playerState = PlayerState.Sliding;
            heldPiece.shape.transform.position = heldPiece.position.UnityPosition();
            heldPiece.shape.UpdateVisuals();
            return;
        }
        if (Input.GetMouseButtonDown(1) && TryGrabPiece(true))
        {
            playerState = PlayerState.Rotating;
            SetShapeVisualsForRotation();
            RadialRegion.SetPosition(mainAnchor.position.UnityPosition());
            RadialRegion.SetSizeForSpan((heldPiece.position.UnityPosition() - mainAnchor.position.UnityPosition()).magnitude);
            //Calculate what final sides we can rotate to
            clockwiseRotations = heldPiece.shape.ValidRotationSidesInDirection(mainAnchor, heldPiece, true);
            counterClockwiseRotations = heldPiece.shape.ValidRotationSidesInDirection(mainAnchor, heldPiece, false);

            var allValid = new List<Hex.Side>(clockwiseRotations);
            for (int i = 0; i < counterClockwiseRotations.Length; i++)
            {
                if (allValid.Contains(counterClockwiseRotations[i]) == false)
                    allValid.Add(counterClockwiseRotations[i]);
            }

            float lowerBound = 0f;
            float upperBound = 0f;
            if (allValid.Count != 6 && allValid.Count != 0)
            {
                lowerBound = clockwiseRotations[clockwiseRotations.Length - 1].Radians() - 0.03f;
                upperBound = counterClockwiseRotations[counterClockwiseRotations.Length - 1].Radians() + 0.03f;

                while (lowerBound < 0.0f)
                    lowerBound += Mathf.PI * 2;
                while (upperBound < 0.0f)
                    upperBound += Mathf.PI * 2;
                while (lowerBound > Mathf.PI * 2)
                    lowerBound -= Mathf.PI * 2;
                while (upperBound > Mathf.PI * 2)
                    upperBound -= Mathf.PI * 2;
            }

            RadialRegion.UpdateToValidSides(allValid.ToArray(), mainAnchor.shape, (lowerBound, upperBound), mainAnchor.position.UnityPosition(), secondAnchor.position.UnityPosition());

            return;
        }
    }
    void ExitSlidingState()
    {
        playerState = PlayerState.Free;
        ResetShapeVisuals();
        heldPiece = null;
    }

    void ResetShapeVisuals()
    {
        if (heldPiece == null)
            return;
        heldPiece.shape.transform.position = Vector3.zero;
        heldPiece.shape.transform.localRotation = Quaternion.identity;
        heldPiece.shape.UpdateVisuals();
        RadialRegion.UpdateToValidSides(new Hex.Side[] { }, null, (0f, 0f), Vector3.zero, Vector3.zero);
    }

    void ExitRotatingState()
    {
        ResetShapeVisuals();
        playerState = PlayerState.Free;
        heldPiece = null;
    }

    void HandleRotatingState()
    {
        if (heldPiece != null && Input.GetMouseButtonUp(1))
        {
            ExitRotatingState();
            return;
        }
        RadialRegion.MouseInRegion(MouseInfo.World(), out var clampedCursorWorld, out var onThetaEdge, out var _, out var clockwiseEdge);
        float theta = Mathf.Atan2(clampedCursorWorld.y - mainAnchor.position.UnityPosition().y, clampedCursorWorld.x - mainAnchor.position.UnityPosition().x);

        Hex.RoundToSide(mainAnchor.position, clampedCursorWorld, out var roundedSide);
        if (roundedSide != anchorSide)
        {
            if (clockwiseRotations.Contains(roundedSide))
            {
                RotateHeld(true);
            }
            else if (counterClockwiseRotations.Contains(roundedSide))
            {
                RotateHeld(false);
            }
            else
            {
                //Debug.LogError($"Confused rotation: {theta * Mathf.Rad2Deg:000.0} | {roundedSide}");
            }
        }

        mainAnchor.shape.transform.rotation = Quaternion.Euler(0f, 0f, (theta - anchorSide.Radians()) * Mathf.Rad2Deg);
    }

    void RotateHeld(bool clockwise)
    {
        if (heldPiece == null || mainAnchor == null)
            return;
        heldPiece.shape.Rotate(mainAnchor.position, clockwise);
        Hex.GetRelativeSide(mainAnchor.position, heldPiece.position, out anchorSide, out _);
        SetShapeVisualsForRotation();
    }

    void SetShapeVisualsForRotation()
    {
        if (heldPiece == null || mainAnchor == null)
            return;
        heldPiece.shape.transform.position = mainAnchor.position.UnityPosition();
        heldPiece.shape.transform.localRotation = Quaternion.identity;
        heldPiece.shape.UpdateVisuals();
    }

    void HandleSlidingState()
    {
        if (heldPiece == null || Input.GetMouseButtonUp(0))
        {
            ExitSlidingState();
            return;
        }

        var projectedPoint = ProjectPointOnDirection(heldPiece.position.UnityPosition(), anchorSide.Offset().UnityPosition(), MouseInfo.World(), out float slideT);
        var RoundedHex = MouseInfo.RoundAnyToHex(projectedPoint);
        Hex.RoundToSide(heldPiece.position, projectedPoint, out var side);
        var positionsAfterSlide = heldPiece.shape.PositionsAfterSlide(side);
        bool setPosition = false;
        LevelData.CollisionType collisionInDirection = LevelData.openLevel.CheckCollisions(heldPiece.shape, positionsAfterSlide, out var lastCollisionHexes);
        if (collisionInDirection != LevelData.CollisionType.None)
        {
            var testPosition = (side.Offset().UnityPosition() * (maxCollisionDistPercent * (collisionInDirection == LevelData.CollisionType.Wall ? 1f : 2f)));
            //Clamp the visuals if the limit is closer
            if ((projectedPoint - (Vector2)heldPiece.position.UnityPosition()).magnitude > testPosition.magnitude)
            {
                setPosition = true;
                heldPiece.shape.transform.position = heldPiece.position.UnityPosition() + testPosition;
            }
        }
        else if (RoundedHex != heldPiece.position)//and implicitly there is no collision
        {
            heldPiece.shape.Slide(side);
        }
        if (setPosition == false)
            heldPiece.shape.transform.position = projectedPoint;
    }

    /// <summary>
    /// Returns the closest point on a ray to the specified position. Out specifies the distance 
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <param name="freePoint"></param>
    /// <returns></returns>
    Vector2 ProjectPointOnDirection(Vector2 origin, Vector2 direction, Vector2 freePoint, out float dist)
    {
        if (direction.magnitude < float.Epsilon)
        {
            dist = 0f;
            return origin;
        }

        dist = (Vector2.Dot(freePoint - origin, direction) / direction.magnitude);
        return origin + (direction.normalized * dist);
    }

    bool TryGrabPiece(bool anchorOnly = false)
    {
        if (heldPiece != null)
            return false;

        Hex mouseHover = MouseInfo.Hex();
        foreach (var shape in LevelData.openLevel.shapes)
        {
            foreach (var piece in shape.pieces)
            {
                if (anchorOnly && piece.isAnchor == false)
                    continue;

                if (piece.position.Equals(mouseHover))
                {
                    //We found the piece we are trying to pickup, but is it close enough to the anchor positon?
                    if (anchorOnly && (MouseInfo.World() - piece.position.UnityPosition()).magnitude > piecePickupRange)
                    {
                        break;
                    }

                    heldPiece = piece;
                    mainAnchor = shape.Anchors.First(p => p != heldPiece);
                    secondAnchor = shape.Anchors.First(p => p != mainAnchor);
                    Hex.GetRelativeSide(mainAnchor.position, secondAnchor.position, out anchorSide, out _);
                    return true;
                }
            }
        }
        return false;
    }
    public void ForceDrop()
    {
        switch (playerState)
        {
            default:
            case PlayerState.Free:
                break;
            case PlayerState.Rotating:
                ExitRotatingState();
                break;
            case PlayerState.Sliding:
                ExitSlidingState();
                break;
        }
    }

    protected override void Destroy()
    {

    }
}
