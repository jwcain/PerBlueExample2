using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MixedData = System.Collections.Generic.List<LevelDataValues>;


/// <summary>
/// This class is used during ANTLR parsing to collect data from an input string relevant to building a level.
/// </summary>
public class LevelVisitor : HexquisiteLevelBaseVisitor<List<LevelDataValues>>
{

    /// <summary>
    /// The data for the level saved as an object
    /// </summary>
    public LevelData levelData = new LevelData();

    /// <summary>
    /// Converts the text Version number to a number and stores it
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override MixedData VisitVersion([NotNull] HexquisiteLevelParser.VersionContext context)
    {
        MixedData childResults = VisitChildren(context);
        if (int.TryParse(childResults[1].rawText, out levelData.version) == false) throw new ParseCanceledException("Unable to interpret Version number: " + context.GetText());
        return default;
    }

    /// <summary>
    /// Parses raw text into a Vector2Int
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override MixedData VisitVector2int([NotNull] HexquisiteLevelParser.Vector2intContext context)
    {
        if (int.TryParse(context.children[1].GetText(), out int x) == false) throw new ParseCanceledException("Unable to interpret Vector2Int");
        if (int.TryParse(context.children[3].GetText(), out int y) == false) throw new ParseCanceledException("Unable to interpret Vector2Int");
        return new MixedData() { new LevelDataValues() { vectorData = new Vector2Int(x, y) } };
    }

    /// <summary>
    /// Stores all Vector2Int wall positions to the wall list
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override MixedData VisitWalls([NotNull] HexquisiteLevelParser.WallsContext context)
    {
        MixedData childResults = VisitChildren(context);
        for (int i = 2; i < childResults.Count -1; i++)
        {
            Vector2Int vector = (Vector2Int)childResults[i].vectorData;
            levelData.walls.Add(new Bestagon.Hexagon.Hex(vector));
        }
        return childResults;
    }

    /// <summary>
    /// Adds a new shape to the shapes list so pieces can get added to it without having to track the shape they should belong to.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override MixedData VisitShape([NotNull] HexquisiteLevelParser.ShapeContext context)
    {
        //Start a new shape
        levelData.shapes.Add(new Shape() { isKeyShape = false });
        return base.VisitShape(context);
    }

    /// <summary>
    /// Adds a new shape to the shapes list so pieces can get added to it without having to track the shape they should belong to. Flags it as a key shape
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override MixedData VisitKeyshape([NotNull] HexquisiteLevelParser.KeyshapeContext context)
    {
        levelData.shapes.Add(new Shape() { isKeyShape = true}); ;
        return base.VisitKeyshape(context);
    }

    /// <summary>
    /// Stores all Vector2Int goal positions into the goals list
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override MixedData VisitGoals([NotNull] HexquisiteLevelParser.GoalsContext context)
    {
        MixedData childResults = VisitChildren(context);
        for (int i = 2; i < childResults.Count - 1; i++)
        {
            Vector2Int vector = (Vector2Int)childResults[i].vectorData;
            levelData.goals.Add(new Bestagon.Hexagon.Hex(vector));
        }
        return childResults;
    }

    /// <summary>
    /// The top tier node, grabs and saves the level size
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override MixedData VisitLevel([NotNull] HexquisiteLevelParser.LevelContext context)
    {
        MixedData childResults = VisitChildren(context);
        levelData.mapSize = (Vector2Int)childResults[0].vectorData;
        return childResults;
    }

    /// <summary>
    /// Adds the piece to the most recent visited Shape
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override MixedData VisitPiece([NotNull] HexquisiteLevelParser.PieceContext context)
    {
        MixedData childResults = VisitChildren(context);
        levelData.shapes[levelData.shapes.Count - 1].pieces.Add(new Shape.Piece() { shape = levelData.shapes[levelData.shapes.Count - 1], isAnchor = childResults[0].isAnchor, position = new Bestagon.Hexagon.Hex((Vector2Int)childResults[0].vectorData)});
        return childResults;
    }

    /// <summary>
    /// Combines a Vector2Int and an Anchor flag
    /// An 'Anchor' is a part of a shape about which the shape can rotate.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override MixedData VisitAnchor([NotNull] HexquisiteLevelParser.AnchorContext context)
    {
        MixedData childResults = VisitChildren(context);
        childResults[1].isAnchor = true;
        return new MixedData() { childResults[1] };
    }

    /// <summary>
    /// Visiting the terminal node of the parse tree
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public override MixedData VisitTerminal(ITerminalNode node)
    {
        //We dont know what the context of this terminal node is, so just pass its raw value up so a higher-order node can parse it with context.
        return new MixedData() { new LevelDataValues() { rawText = node.GetText() } };
    }

    protected override MixedData AggregateResult(MixedData aggregate, MixedData nextResult)
    {
        //If there is no existing left operand, return the right
        if (aggregate == null)
            return nextResult;
        
        //If there is no existing right operand, return the left
        if (nextResult == null)
            return aggregate;

        //Append the right to the left in order.
        for (int i = 0; i < nextResult.Count; i++)
        {
            aggregate.Add(nextResult[i]);
        }
        return aggregate;
    }

}

/// <summary>
/// This class can store multiple types of values that we want to return from visiting children.
/// All values should be nullable, and the only non-null value should be the expected type.
/// </summary>
public class LevelDataValues
{
    public string rawText = null;
    public Vector2Int? vectorData = null;
    public bool isAnchor = false;

    public override string ToString()
    {
        return string.Format("raw:\"{0}\" vector:{1} anchor:{2}",  rawText, vectorData, isAnchor);
    }

}