using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelBuilderTool
{
    public UnityEngine.UI.Button button;
    public static LevelBuilderTool currentTool = null;

    public virtual void Update()
    {

    }

    public virtual void SelectTool() {
    }

    public virtual void DeselectTool()
    {
        
    }

    public static void SwitchTool<T>(UnityEngine.UI.Button relatedButton) where T : LevelBuilderTool, new()
    {
        if (LevelBuilderTool.currentTool != null)
        {
            if (LevelBuilderTool.currentTool.button != null)
                LevelBuilderTool.currentTool.button.image.color = LevelBuilderController.Instance.toolDefaultColor;
            LevelBuilderTool.currentTool.DeselectTool();
        }
        //Return to the default tool if we press the same button
        if (LevelBuilderTool.currentTool != null && typeof(T).Equals(LevelBuilderTool.currentTool.GetType()))
        {
            LevelBuilderTool.currentTool = null;
            SwitchTool<Hexquisite.LevelBuilder.DefaultTool>(null);
        }
        else
        {
            LevelBuilderTool.currentTool = new T();
            if (relatedButton != null)
            {
                LevelBuilderTool.currentTool.button = relatedButton;
                LevelBuilderTool.currentTool.button.image.color = LevelBuilderController.Instance.toolSelectedColor;
            }
            LevelBuilderTool.currentTool.SelectTool();
        }
    }
}
