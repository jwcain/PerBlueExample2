using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringImporterWindow : MonoBehaviour
{

    public TMPro.TMP_InputField stringInput;

    public void Import()
    {
        LevelBuilderController.RunCommand(new Hexquisite.LevelBuilder.ImportLevelViaString() { text = stringInput.text });
        Back();
    }
    public void Clear()
    {
        stringInput.text = string.Empty;
    }
    public void Back()
    {
        LevelBuilderController.ChangeWindow(LevelBuilderController.WindowState.Default);
    }
}
