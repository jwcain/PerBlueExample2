using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public struct AddAnchorToShape : ILevelBuilderCommand
    {
        public Shape.Piece piece;
        public void Execute(LevelData levelObj)
        {
            piece.isAnchor = true;
        }

        public void Undo(LevelData levelObj)
        {

        }
    }
}