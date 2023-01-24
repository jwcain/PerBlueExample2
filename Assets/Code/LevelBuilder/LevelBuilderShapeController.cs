using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilderShapeController : MonoBehaviour
{

    public UnityEngine.UI.Button anchorsButton;
    public UnityEngine.UI.Button drawButton;
    public UnityEngine.UI.Button eraseButton;

    public void Deselect()
    {
        //If we closed this window without anything remaining, delete the empty shape
        if (LevelData.openLevel.selectedShape != null && LevelData.openLevel.selectedShape.pieces.Count == 0)
        {
            //We want to delete and deselect the shape wihtout creating a log
            Shape toDelete = LevelData.openLevel.selectedShape;
            LevelBuilderController.RunCommandUnlogged(new Hexquisite.LevelBuilder.DeselectShape());
            LevelBuilderController.RunCommandUnlogged(new Hexquisite.LevelBuilder.DeleteShape() { shape = toDelete }); ;
        }

        LeaveMenu();
    }

    private void LeaveMenu()
    {
        //Switch to the default tool in case we had one selected. Shouldnt cause a conflict if we did already.
        LevelBuilderTool.SwitchTool<Hexquisite.LevelBuilder.DefaultTool>(null);
        LevelBuilderController.ChangeWindow(LevelBuilderController.WindowState.Default);
    }

    public void Delete()
    {
        Shape toDelete = LevelData.openLevel.selectedShape;
        LevelBuilderController.RunCommand(new Hexquisite.LevelBuilder.DeselectShape());
        LevelBuilderController.RunCommand(new Hexquisite.LevelBuilder.DeleteShape() { shape = toDelete }); ;
        LeaveMenu();
    }

    public void PlaceAnchors()
    {
        LevelBuilderTool.SwitchTool<Hexquisite.LevelBuilder.AnchorPlacer>(anchorsButton);
    }

    public void ToggleKey()
    {
        LevelBuilderController.RunCommand(new Hexquisite.LevelBuilder.ToggleShapeKeyState() { shape = LevelData.openLevel.selectedShape });
    }

    public void Draw()
    {
        LevelBuilderTool.SwitchTool<Hexquisite.LevelBuilder.ShapePainting>(drawButton);
    }

    public void Erase()
    {
        LevelBuilderTool.SwitchTool<Hexquisite.LevelBuilder.ShapeEraser>(eraseButton);

    }
}
