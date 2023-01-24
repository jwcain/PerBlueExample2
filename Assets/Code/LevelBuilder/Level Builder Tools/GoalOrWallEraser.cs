
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public class GoalOrWallEraser : LevelBuilderTool
    {

        public override void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Bestagon.Hexagon.Hex hex = MouseInfo.Hex();
                if (LevelData.openLevel.walls.Contains(hex))
                    LevelBuilderController.RunCommand(new EraseWall() { position = hex });
                if (LevelData.openLevel.goals.Contains(hex))
                    LevelBuilderController.RunCommand(new EraseGoal() { position = hex });
            }
        }
    }
}