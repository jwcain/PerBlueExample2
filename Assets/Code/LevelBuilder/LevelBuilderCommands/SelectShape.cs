using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public struct SelectShape : ILevelBuilderCommand
    {
        public Shape shape;
        public void Execute(LevelData levelObj)
        {
            LevelData.openLevel.selectedShape = shape;
        }

        public void Undo(LevelData levelObj)
        {

        }
    }
}