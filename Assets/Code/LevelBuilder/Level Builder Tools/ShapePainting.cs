
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public class ShapePainting : LevelBuilderTool
    {

        public override void Update()
        {
            if (LevelData.openLevel.selectedShape != null && Input.GetMouseButton(0))
            {
                Bestagon.Hexagon.Hex hex = MouseInfo.Hex();
                Vector2Int t = hex.ToTilemap();
                if (LevelData.openLevel.walls.Contains(hex) == false &&
                    LevelData.openLevel.shapes.Exists(s => s.pieces.Exists(p => p.position == hex)) == false &&
                    t.x >= 0 && t.y >= 0 && t.x < LevelData.openLevel.mapSize.x && t.y < LevelData.openLevel.mapSize.y)
                {
                    Debug.Log(LevelData.openLevel.selectedShape);
                    LevelBuilderController.RunCommand(new ShapeDraw() { shape = LevelData.openLevel.selectedShape, position = hex });
                }
            }
        }
    }
}