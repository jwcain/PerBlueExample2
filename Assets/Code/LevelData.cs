using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bestagon.Hexagon;
using System.Linq;

public class LevelData
{
    public const int MAX_LEVEL_SIZE = 128;
    public static LevelData openLevel = null;
    public static LevelData playModeBackup = null;
    public Shape selectedShape = null;

    /// <summary>
    /// The version of the level editor this level was created under
    /// </summary>
    public int version = -1;
    /// <summary>
    /// The size of the map
    /// </summary>
    public Vector2Int mapSize = new Vector2Int(-1, -1);
    /// <summary>
    /// Locations to place walls
    /// </summary>
    public List<Hex> walls = new List<Hex>();
    /// <summary>
    /// Locations to place goals
    /// </summary>
    public List<Hex> goals = new List<Hex>();
    /// <summary>
    /// Locations to place shapes.
    /// </summary>
    public List<Shape> shapes = new List<Shape>();

    /// <summary>
    /// Converts the data stored in this object into a string that can be saved and parsed later
    /// </summary>
    /// <returns></returns>
    public string CompileForTextSaving()
    {
        System.Func<Vector2Int, string> VectorToText = (Vector2Int v) => { return string.Format("({0},{1})", v.x, v.y); };
        string result = "";
        //Save version
        result += string.Format("V{0}",version);
        
        //Save size
        result += VectorToText(mapSize);

        //Save walls
        result += "W{";
        for (int i = 0; i < walls.Count; i++)
        {
            result += VectorToText(new Vector2Int(walls[i].q, walls[i].r));
        }
        result += "}";

        //Save goals
        result += "G{";
        for (int i = 0; i < goals.Count; i++)
        {
            result += VectorToText(new Vector2Int(goals[i].q, goals[i].r));
        }
        result += "}";

        //Save Shapes
        for (int s = 0; s < shapes.Count; s++)
        {
            result += (shapes[s].isKeyShape) ? "K" : "S";
            result += "{";
            for (int i = 0; i < shapes[s].pieces.Count; i++)
            {
                result += (shapes[s].pieces[i].isAnchor ? "A" : "") + VectorToText(new Vector2Int(shapes[s].pieces[i].position.q, shapes[s].pieces[i].position.r));
            }
            result += "}";
        }

        return result;
    }

    public enum CollisionType { None, Wall, Shape }
    /// <summary>
    /// Checks for collisions, ignoring pieces that are part of the current shape
    /// </summary>
    /// <param name="targetShape"></param>
    /// <param name="collisonHexes"></param>
    /// <returns></returns>
    public CollisionType CheckCollisions(Shape targetShape, Hex[] collisonHexes, out Hex[] hexes)
    {
        CollisionType collisionType = CollisionType.None;
        List<Hex> hitHexes = new List<Hex>();
        foreach (var hex in collisonHexes)
        {
            Vector2Int tilePos = hex.ToTilemap();
            if (TileRenderer.TileExists(TileRenderer.Channel.Walls, hex.ToTilemap()) || tilePos.x < 0 || tilePos.y < 0 || tilePos.x >= mapSize.x  || tilePos.y >= mapSize.y)
            {
                hitHexes.Add(hex);
                collisionType = CollisionType.Wall;
                continue;
            }

            foreach (var shape in shapes)
            {
                if (shape == targetShape)
                    continue;
                if (shape.pieces.Exists(p => p.position == hex))
                {
                    hitHexes.Add(hex);
                    if (collisionType == CollisionType.None)
                        collisionType = CollisionType.Shape;
                }
            }

        }

        hexes = hitHexes.ToArray();
        return collisionType;
    }

    /// <summary>
    /// Validates the data stored and gives back text describing the problems. This will attempt to report all problems with the data
    /// </summary>
    /// <param name="errorText"></param>
    /// <returns></returns>
    public bool Validate(out string[] errorText, out string[] warningText)
    {
        List<string> errors = new List<string>();
        List<string> warnings = new List<string>();
        bool valid = true;

        
        void CheckCondition(bool conditionResult, bool isError, string text)
        {
            if (conditionResult)
                return;
            
            if (isError)
            {
                valid = false;
                errors.Add(text);
            }
            else
                warnings.Add(text);
        }
        
        //Version has to match
        CheckCondition(LevelLoadingOperations.SUPPORTED_VERSION.Equals(version), true, string.Format("Version did not match. Expected:{0}, Found:{1}", LevelLoadingOperations.SUPPORTED_VERSION, version));
        //There must be at least one goal tile
        CheckCondition(goals.Count > 0, false, "There must be at least one goal position.");
        //Level size has to not be too big
        CheckCondition(mapSize.x < MAX_LEVEL_SIZE && mapSize.y < MAX_LEVEL_SIZE, true, string.Format("Map too large, max supported size is ({2}x{2}). Found:({0}x{1})", mapSize.x, mapSize.y, MAX_LEVEL_SIZE));
        //Key Shapes has to equal goal tiles
        CheckCondition(shapes.Sum((Shape s) => s.isKeyShape ? s.pieces.Count : 0) is var keyPieceCount && keyPieceCount == goals.Count, false, string.Format("There is a mismatch between the number of key pieces {0} and the number of goal positions {1}.", keyPieceCount, goals.Count));
        //Wall positions must not overlap with shape or goal positions (but shape and goal positions may overlap)
        CheckCondition(goals.Find((Hex hex) => walls.Contains(hex)) is var found && found != null, true, string.Format("Wall position overlaps with goal position: {0}", found));
        //Every shape must have two anchors
        CheckCondition(shapes.Count((Shape s) => { return s.pieces.Count((Shape.Piece p) => p.isAnchor) != 2; }) is var unanchoredShapes && unanchoredShapes == 0, true, string.Format("There are {0} shapes that do not have exactly 2 anchors", unanchoredShapes) );
        //Shape anchors must be in line.
        CheckCondition(
            shapes.Count( (Shape s) => s.Anchors.Count == 2 /*Only check this condition on the shapes that have the appropriate amount of anchors, so we dont double report an error*/ 
            && Hex.InLine(s.Anchors[0].position, s.Anchors[1].position) == false) is var unalignedAnchorShapes && unalignedAnchorShapes == 0
            ,
            true, string.Format("All anchors on shapes must be in a straight line. {0} shapes are not.", unalignedAnchorShapes));

        bool OutOfBounds(Hex h)
        {
            Vector2Int t = h.ToTilemap();
            return t.x < 0 || t.x >= mapSize.x || t.y < 0 || t.y >= mapSize.y;
        }

        //Goals, Walls, and Shape positions should be within bounds of the map size
        CheckCondition(
            shapes.Sum((Shape s) => { return s.pieces.Count((Shape.Piece p) => OutOfBounds(p.position)); }) is var outOfBoundsPieces &&
            goals.Count((Hex h) => OutOfBounds(h)) is var outOfBoundsGoals &&
            walls.Count((Hex h) => OutOfBounds(h)) is var outOfBoundsWalls && outOfBoundsGoals == 0 && outOfBoundsPieces == 0 && outOfBoundsWalls == 0,
            true,
            string.Format("Object was found outside of map bounds. # of Walls:{0}, of Pieces:{1}, of Goal Positions:{2}", outOfBoundsWalls, outOfBoundsPieces, outOfBoundsGoals));

        //TODO: There are multiple checks above that use LINQ to loop through all shapes. Can we loop through each shape manually to save looping the same list so many times?
        //The shape count is likely not that high, so I am not too worried about the cost of looping through them many times.
        errorText = errors.ToArray();
        warningText = warnings.ToArray();
        return valid;
    }

    public LevelData Clone()
    {
        LevelData clone = new LevelData();
        clone.mapSize = mapSize;
        clone.version = version;
        clone.walls = new List<Hex>();
        clone.goals = new List<Hex>();
        clone.shapes = new List<Shape>();
        foreach (var wall in walls)
            clone.walls.Add(wall);
        foreach (var goal in goals)
            clone.goals.Add(goal);
        foreach (var shape in shapes)
            clone.shapes.Add(shape.Clone());
        return clone;
    }
}
