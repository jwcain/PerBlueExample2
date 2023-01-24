using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public struct DecreaseMapHeight : ILevelBuilderCommand
    {
        public void Execute(LevelData levelObj)
        {
            TileRenderer.DrawArea(TileRenderer.Channel.Background, new RectInt(0, 0, LevelData.openLevel.mapSize.x, LevelData.openLevel.mapSize.y), null, true);
            levelObj.mapSize = new Vector2Int(levelObj.mapSize.x, levelObj.mapSize.y >= 1 ? levelObj.mapSize.y - 1 : 0);
            TileRenderer.DrawArea(TileRenderer.Channel.Background, new RectInt(0, 0, LevelData.openLevel.mapSize.x, LevelData.openLevel.mapSize.y), LevelLoadingOperations.Instance.playAreaTile, true);
            CameraController.CenterCameraOnArea(LevelData.openLevel.mapSize, 0.1f);

            Debug.LogWarning("This should handle all objects that are now outside of the map size, but does not.");
        }

        public void Undo(LevelData levelObj)
        {

        }
    }
}