using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public struct ImportLevelViaString : ILevelBuilderCommand
    {
        public string text;
        public void Execute(LevelData levelObj)
        {
            LevelLoadingOperations.MakeLevelFromStringATNLRUnbound(text);
        }

        public void Undo(LevelData levelObj)
        {

        }
    }
}
