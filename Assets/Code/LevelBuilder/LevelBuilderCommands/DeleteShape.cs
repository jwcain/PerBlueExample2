using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public struct DeleteShape : ILevelBuilderCommand
    {
        public Shape shape;
        public void Execute(LevelData levelObj)
        {
            shape.DestroyVisuals();
            levelObj.shapes.Remove(shape);
        }

        public void Undo(LevelData levelObj)
        {

        }
    }
}