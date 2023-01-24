using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilderDefaultToolsController : MonoBehaviour
{
    public UnityEngine.UI.Button goalButton;
    public UnityEngine.UI.Button wallButton;
    public UnityEngine.UI.Button eraseButton;
    public UnityEngine.UI.Button shapeManagementShapeDrawButton;
    public UnityEngine.UI.Button exitPlayButton;

    public void Play()
    {
        if (LevelData.openLevel.Validate(out var errors, out var warnings) is var valid)
        {
            foreach (var error in errors)
            {
                Debug.LogError(error);
            }
            foreach (var warning in warnings)
            {
                Debug.LogWarning(warning);
            }
            if (valid == false)
                return;
        }
        LevelData.playModeBackup = LevelData.openLevel.Clone();
        LevelBuilderTool.SwitchTool<Hexquisite.LevelBuilder.PlayModeHost>(exitPlayButton);
        LevelBuilderController.ChangeWindow(LevelBuilderController.WindowState.Play);
    }

    public void ImportLevel()
    {
        LevelBuilderController.ChangeWindow(LevelBuilderController.WindowState.StringImport);
    }

    public void ExportLevel()
    {
        GUIUtility.systemCopyBuffer = LevelData.openLevel.CompileForTextSaving();
    }

    public void MapSize()
    {
        LevelBuilderController.ChangeWindow(LevelBuilderController.WindowState.MapSizing);
    }

    public void Goal()
    {
        LevelBuilderTool.SwitchTool<Hexquisite.LevelBuilder.GoalPainting>(goalButton);
    }

    public void Wall()
    {
        LevelBuilderTool.SwitchTool<Hexquisite.LevelBuilder.WallPainting>(wallButton);
    }

    public void Shape()
    {
        //If this button gets pressed that means we are making a new shape!
        Shape newShape = new Shape();
        LevelData.openLevel.shapes.Add(newShape);
        LevelBuilderController.RunCommand(new Hexquisite.LevelBuilder.SelectShape() { shape = newShape });
        LevelBuilderController.ChangeWindow(LevelBuilderController.WindowState.Shape);
        LevelBuilderTool.SwitchTool<Hexquisite.LevelBuilder.ShapePainting>(shapeManagementShapeDrawButton);
    }

    public void Erase()
    {
        LevelBuilderTool.SwitchTool<Hexquisite.LevelBuilder.GoalOrWallEraser>(eraseButton);
    }
}
