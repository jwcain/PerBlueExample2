using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Hexquisite.LevelBuilder
{
    public class WallPainting : LevelBuilderTool
    {

        public override void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Bestagon.Hexagon.Hex hex = MouseInfo.Hex();
                Vector2Int t = hex.ToTilemap();
                //Dont paint walls where there already are walls, already are goals, already are shapes, or outside of bounds
                if (LevelData.openLevel.walls.Contains(hex) == false && LevelData.openLevel.goals.Contains(hex) == false &&
                    LevelData.openLevel.shapes.Exists(s => s.pieces.Exists(p => p.position == hex)) == false &&
                    t.x >= 0 && t.y >= 0 && t.x < LevelData.openLevel.mapSize.x && t.y < LevelData.openLevel.mapSize.y)
                {
                    LevelBuilderController.RunCommand(new DrawWallAt() { position = hex });
                }
            }
        }
    }
}