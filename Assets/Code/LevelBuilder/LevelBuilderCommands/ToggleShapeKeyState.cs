using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public struct ToggleShapeKeyState : ILevelBuilderCommand
    {
        public Shape shape;
        public void Execute(LevelData levelObj)
        {
            shape.isKeyShape = !shape.isKeyShape;
            shape.UpdateColor();
        }

        public void Undo(LevelData levelObj)
        {

        }
    }
}