using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilderController : Bestagon.Behaviours.SingletonBehaviour<LevelBuilderController>
{
    List<ILevelBuilderCommand> commands = new List<ILevelBuilderCommand>();

    public Color toolSelectedColor;
    public Color toolDefaultColor;

    public enum WindowState { Default, StringImport, MapSizing, Shape, Play }
    [EditorReadOnly, SerializeField] private WindowState windowState = WindowState.Default;

    [System.Serializable]
    public struct StateGameObjectSet
    {
        [SerializeField] public WindowState stateEnabledFor;
        [SerializeField] public GameObject[] objs;
    }
    [SerializeField] StateGameObjectSet[] windowObjectSets;

    public void Update()
    {
        if (LevelBuilderTool.currentTool != null)
            LevelBuilderTool.currentTool.Update();
    }

    public void Start()
    {
        PlayerController.Instance.gameObject.SetActive(false);
        ChangeWindow(WindowState.Default);
        LevelBuilderTool.SwitchTool<Hexquisite.LevelBuilder.DefaultTool>(null);
    }

    public static void ChangeWindow(WindowState windowState)
    {
        if (Instance.windowState == windowState)
            return;
        Instance.windowState = windowState;
        //Disabler all Objects
        foreach (var item in Instance.windowObjectSets)
        {
            foreach (var obj in item.objs)
            {
                obj.SetActive(false);
            }
        }

        //Enable all objects
        //It must be done in two seperate loops so we do not disable an object that is used acrossed sets
        foreach (var item in Instance.windowObjectSets)
        {
            if (Instance.windowState != item.stateEnabledFor)
                continue;
            foreach (var obj in item.objs)
            {
                obj.SetActive(true);
            }
        }
    }

    public static void RunCommand(ILevelBuilderCommand command)
    {
        Instance.commands.Add(command);
        RunCommandUnlogged(command);
    }
    public static void RunCommandUnlogged(ILevelBuilderCommand command)
    {
        command.Execute(LevelData.openLevel);
    }

    protected override void Destroy()
    {
        //Nothing
    }
}
