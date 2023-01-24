
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public class GoalPainting : LevelBuilderTool
    {

        public override void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Bestagon.Hexagon.Hex hex = MouseInfo.Hex();
                Vector2Int t = hex.ToTilemap();
                //Dont draw goals where there are walls, already are goals, or outside of bounds.
                //Goals and shapes do not collide, so they can overlap
                if (LevelData.openLevel.walls.Contains(hex) == false && LevelData.openLevel.goals.Contains(hex) == false &&
                    t.x >= 0 && t.y >= 0 && t.x < LevelData.openLevel.mapSize.x && t.y < LevelData.openLevel.mapSize.y)
                {
                    LevelBuilderController.RunCommand(new DrawGoalAt() { position = hex });
                }
            }
        }
    }
}