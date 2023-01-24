using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public struct DrawWallAt : ILevelBuilderCommand
    {
        public Bestagon.Hexagon.Hex position;

        public void Execute(LevelData levelObj)
        {
            levelObj.walls.Add(position);
            TileRenderer.DrawTile(TileRenderer.Channel.Walls, position.ToTilemap(), LevelLoadingOperations.Instance.wallTile, true);
        }

        public void Undo(LevelData levelObj)
        {

        }
    }
}