using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public struct DeselectShape : ILevelBuilderCommand
    {
        public void Execute(LevelData levelObj)
        {
            LevelData.openLevel.selectedShape = null;
        }

        public void Undo(LevelData levelObj)
        {

        }
    }
}