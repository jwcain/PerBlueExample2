using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Hexquisite.LevelBuilder
{
    public class ShapeEraser : LevelBuilderTool
    {

        public override void Update()
        {
            if (LevelData.openLevel.selectedShape is var shape && shape != null && Input.GetMouseButton(0))
            {
                var hex = MouseInfo.Hex();
                if (shape.pieces.Find(p => p.position == hex) is var piece && piece != null)
                {
                    shape.pieces.Remove(piece);
                    shape.CreateVisuals();
                }
            }
        }
    }
}