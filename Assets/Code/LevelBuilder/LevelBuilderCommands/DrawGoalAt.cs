using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public struct DrawGoalAt : ILevelBuilderCommand
    {
        public Bestagon.Hexagon.Hex position;
        public void Execute(LevelData levelObj)
        {
            levelObj.goals.Add(position);
            TileRenderer.DrawTile(TileRenderer.Channel.Special, position.ToTilemap(), LevelLoadingOperations.Instance.goalTile, true);
        }

        public void Undo(LevelData levelObj)
        {

        }
    }
}