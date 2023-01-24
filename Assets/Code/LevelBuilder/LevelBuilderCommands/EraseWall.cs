using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public struct EraseWall : ILevelBuilderCommand
    {
        public Bestagon.Hexagon.Hex position;
        public void Execute(LevelData levelObj)
        {
            levelObj.walls.Remove(position);
            TileRenderer.DrawTile(TileRenderer.Channel.Walls, position.ToTilemap(), null, true);

        }

        public void Undo(LevelData levelObj)
        {

        }
    }
}