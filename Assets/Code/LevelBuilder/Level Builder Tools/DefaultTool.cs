
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public class DefaultTool : LevelBuilderTool
    {

        public override void Update()
        {
            //Attempt to select a Shape if we clicked on one.
            if (Input.GetMouseButtonDown(0))
            {
                var hex = MouseInfo.Hex();
                bool breakAll = false;
                foreach (var shape in LevelData.openLevel.shapes)
                {
                    if (breakAll)
                        break;

                    foreach (var piece in shape.pieces)
                    {
                        if (piece.position == hex)
                        {
                            LevelBuilderController.RunCommand(new SelectShape() { shape = shape });
                            LevelBuilderController.ChangeWindow(LevelBuilderController.WindowState.Shape);
                            breakAll = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}