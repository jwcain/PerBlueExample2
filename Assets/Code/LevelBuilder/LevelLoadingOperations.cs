using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// Provides Coroutines for building levels
/// </summary>
public class LevelLoadingOperations : Bestagon.Behaviours.SingletonBehaviour<LevelLoadingOperations>
{
    public const int SUPPORTED_VERSION = 1;
    public const int MAX_STRING_LENGTH = 2048;

    [SerializeField] public UnityEngine.Tilemaps.Tile goalTile;
    [SerializeField] public UnityEngine.Tilemaps.Tile wallTile;
    [SerializeField] public UnityEngine.Tilemaps.Tile playAreaTile;
    [SerializeField] public Color baseShapeColor;
    [SerializeField] public Color keyShapeColor;


    [ContextMenu("Run Test String")]
    public void Start()
    {
#if UNITY_EDITOR
        if (Application.isPlaying == false)
        {
            Debug.LogError("Can only be ran in play mode.");
            return;
        }
#endif

        StartCoroutine(MakeDefaultLevel());
    }

    public static IEnumerator MakeDefaultLevel()
    {
        yield return ClearCurrentLevel();
        LevelData.openLevel = new LevelData();
        LevelData.openLevel.version = SUPPORTED_VERSION;
        LevelData.openLevel.mapSize = new Vector2Int(12, 7);

        RectInt area = new RectInt(0, 0, LevelData.openLevel.mapSize.x, LevelData.openLevel.mapSize.y);
        TileRenderer.DrawArea(TileRenderer.Channel.Background, area, Instance.playAreaTile, true);

        CameraController.CenterCameraOnArea(LevelData.openLevel.mapSize, 0.1f);
    }

    public static void MakeLevelFromStringATNLRUnbound(string levelText)
    {
        Instance.StartCoroutine(MakeLevelFromStringATNLR(levelText));
    }

    public static IEnumerator ClearCurrentLevel()
    {
        if (LevelData.openLevel == null)
            yield break;
        foreach (var shape in LevelData.openLevel.shapes)
        {
            shape.DestroyVisuals();
        }
        RectInt area = new RectInt(-10, -10, LevelData.MAX_LEVEL_SIZE + 10, LevelData.MAX_LEVEL_SIZE + 10);

        TileRenderer.DrawArea(TileRenderer.Channel.Background, area, null, true);
        TileRenderer.DrawArea(TileRenderer.Channel.Special, area, null, true);
        TileRenderer.DrawArea(TileRenderer.Channel.Walls, area, null, true);

        LevelData.openLevel = null;
    }

    public static IEnumerator MakeLevelFromStringATNLR(string levelText)
    {
        yield return ClearCurrentLevel();
        if (levelText.Length > MAX_STRING_LENGTH)
            yield break;

        //Use ANTLR generated files to being processing text
        HexquisiteLevelLexer lexer = new HexquisiteLevelLexer(new Antlr4.Runtime.AntlrInputStream(levelText));
        HexquisiteLevelParser parser = new HexquisiteLevelParser(new Antlr4.Runtime.CommonTokenStream(lexer));

        //Use custom parser tree visitor to build level data from the text
        LevelVisitor levelVisitor = new LevelVisitor();
        levelVisitor.Visit(parser.level());

        //Validate the parsed level data
        var levelData = levelVisitor.levelData;
        if (levelData.Validate(out var errors, out var warnings) is var valid)
        {
            foreach (var error in errors)
            {
                Debug.LogError(error);
            }
            foreach (var warning in warnings)
            {
                Debug.LogWarning(warning);
            }
            if (valid == false)
                yield break;
        }

        //Place the level objects onto the scene.
        RectInt area = new RectInt(0, 0, levelData.mapSize.x, levelData.mapSize.y);
        TileRenderer.DrawArea(TileRenderer.Channel.Background, area, Instance.playAreaTile, true);

        CameraController.CenterCameraOnArea(levelData.mapSize, 0.1f);

        foreach (var wall in levelData.walls)
        {
            TileRenderer.DrawTile(TileRenderer.Channel.Walls, wall.ToTilemap(), Instance.wallTile, true);
        }
        foreach (var goal in levelData.goals)
        {
            TileRenderer.DrawTile(TileRenderer.Channel.Special, goal.ToTilemap(), Instance.goalTile, true);
        }

        Shape.createdShapeCount = 0;
        foreach (var shape in levelData.shapes)
        {
            shape.CreateVisuals();
        }

        //Store the data we loaded as the current open level
        LevelData.openLevel = levelData;
        yield break;

    }

    protected override void Destroy()
    {
        //Nothing needed
    }
}
