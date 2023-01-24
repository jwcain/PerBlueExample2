using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public struct IncreaseMapHeight : ILevelBuilderCommand
    {
        public void Execute(LevelData levelObj)
        {
            TileRenderer.DrawArea(TileRenderer.Channel.Background, new RectInt(0, 0, LevelData.openLevel.mapSize.x, LevelData.openLevel.mapSize.y), null, true);
            levelObj.mapSize = new Vector2Int(levelObj.mapSize.x, levelObj.mapSize.y >= LevelData.MAX_LEVEL_SIZE - 1 ? LevelData.MAX_LEVEL_SIZE - 1 : levelObj.mapSize.y + 1);
            TileRenderer.DrawArea(TileRenderer.Channel.Background, new RectInt(0, 0, LevelData.openLevel.mapSize.x, LevelData.openLevel.mapSize.y), LevelLoadingOperations.Instance.playAreaTile, true);
            CameraController.CenterCameraOnArea(LevelData.openLevel.mapSize, 0.1f);
        }

        public void Undo(LevelData levelObj)
        {

        }
    }
}