using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilderMapSizeController : MonoBehaviour
{
    public void Back()
    {
        LevelBuilderController.ChangeWindow(LevelBuilderController.WindowState.Default);
    }

    public void IncreaseWidth()
    {
        LevelBuilderController.RunCommand(new Hexquisite.LevelBuilder.IncreaseMapWidth());
    }

    public void DecreaseWidth()
    {
        LevelBuilderController.RunCommand(new Hexquisite.LevelBuilder.DecreaseMapWidth());
    }

    public void IncreaseHeight()
    {
        LevelBuilderController.RunCommand(new Hexquisite.LevelBuilder.IncreaseMapHeight());
    }

    public void DecreaseHeight()
    {
        LevelBuilderController.RunCommand(new Hexquisite.LevelBuilder.DecreaseMapHeight());
    }
}
