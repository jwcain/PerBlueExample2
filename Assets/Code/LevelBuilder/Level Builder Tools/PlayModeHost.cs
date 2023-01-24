
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexquisite.LevelBuilder
{
    public class PlayModeHost : LevelBuilderTool
    {
        public override void SelectTool()
        {
            base.SelectTool();
            PlayerController.Instance.gameObject.SetActive(true);
        }

        public override void DeselectTool()
        {
            base.DeselectTool();
            //Stop all operations on the game mode player before we go back to edit mode
            PlayerController.Instance.ForceDrop();
            PlayerController.Instance.gameObject.SetActive(false);
        }
    }
}