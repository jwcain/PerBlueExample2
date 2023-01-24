using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public struct EraseGoal : ILevelBuilderCommand
    {
        public Bestagon.Hexagon.Hex position;
        public void Execute(LevelData levelObj)
        {
            levelObj.goals.Remove(position);
            TileRenderer.DrawTile(TileRenderer.Channel.Special, position.ToTilemap(), null, true);

        }

        public void Undo(LevelData levelObj)
        {

        }
    }
}