using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public struct ShapeDraw : ILevelBuilderCommand
    {
        public Bestagon.Hexagon.Hex position;
        public Shape shape;
        public void Execute(LevelData levelObj)
        {
            shape.pieces.Add(new Shape.Piece() { shape = shape, position = position });
            shape.CreateVisuals();
        }

        public void Undo(LevelData levelObj)
        {

        }
    }
}