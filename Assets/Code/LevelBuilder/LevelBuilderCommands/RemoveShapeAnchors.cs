using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public struct RemoveShapeAnchors : ILevelBuilderCommand
    {
        public Shape shape;
        public void Execute(LevelData levelObj)
        {
            foreach (var item in shape.pieces)
            {
                item.isAnchor = false;
            }
        }

        public void Undo(LevelData levelObj)
        {

        }
    }
}