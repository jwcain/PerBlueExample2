using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Hexquisite.LevelBuilder
{
    public class AnchorPlacer : LevelBuilderTool
    {

        public override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var hex = MouseInfo.Hex();
                //First ensure we are clicking on a piece on the selected shape
                if (LevelData.openLevel.selectedShape.pieces.Find(p => p.position == hex) is var piece && piece != null)
                {
                    //If there are already two anchors, the only operation we can perfome is a deselect
                    if (LevelData.openLevel.selectedShape.Anchors.Count == 2)
                    {
                        //And then only if it is an anchor (this check is redunant, but helps code clarity)
                        if (piece.isAnchor)
                            piece.isAnchor = false;
                    }
                    else
                    {
                        //Otherwise, toggle the anchor state of the piece
                        piece.isAnchor = !piece.isAnchor;
                    }
                    //TODO: Not a production valid way to update these visuals, but I am short on time to prep for the demo
                    piece.shape.CreateVisuals();
                }
            }
        }
    }
}