using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public struct ShapeErase : ILevelBuilderCommand
    {
        public Shape shape;
        public Shape.Piece piece;
        public void Execute(LevelData levelObj)
        {
            shape.pieces.Remove(piece);
        }

        public void Undo(LevelData levelObj)
        {

        }
    }
}