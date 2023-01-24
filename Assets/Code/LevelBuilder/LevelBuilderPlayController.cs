using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilderPlayController : MonoBehaviour
{
    public void ExitPlay()
    {
        foreach (var shape in LevelData.openLevel.shapes)
        {
            shape.DestroyVisuals();
        }
        LevelData.openLevel = LevelData.playModeBackup;
        LevelData.playModeBackup = null;
        foreach (var shape in LevelData.openLevel.shapes)
        {
            shape.CreateVisuals();
        }
        LevelBuilderTool.SwitchTool<Hexquisite.LevelBuilder.DefaultTool>(null);
        LevelBuilderController.ChangeWindow(LevelBuilderController.WindowState.Default);
    }
}
