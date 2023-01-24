using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the scaling of UI Elements to explicit percentages of screen space.
/// </summary>
public class CustomUIScaler : MonoBehaviour
{

    [System.Serializable]
    public struct UIElementScalingOptions
    {
        [System.Serializable]
        public struct EdgeData
        {
            [SerializeField] public bool doScale;
            [SerializeField] public float scale;
            [SerializeField] public float padding;
        }

        [SerializeField] public RectTransform target;
        [SerializeField] public EdgeData right;
        [SerializeField] public EdgeData top;
        [SerializeField] public EdgeData left;
        [SerializeField] public EdgeData bottom;

    }

    [SerializeField] private UIElementScalingOptions[] toScale;


    // Start is called before the first frame update
    void Start()
    {
        UpdateToScreenSize();
    }

    [ContextMenu("Resize UI")]
    void UpdateToScreenSize()
    {
        Vector2 screen = Bestagon.Mobile.MobileUtils.GetMainGameViewSize();

        //The following seem to be what alternative names of values map to in the inspector window
        //Y = TOP
        //HEIGHT = bottom
        //X = LEFT
        //width = RIGHT
        foreach (var item in toScale)
        {
            float x = item.left.doScale ? (screen.x * item.left.scale) + (item.left.padding * screen.x): 0f;
            float y = item.top.doScale ? (screen.y * item.top.scale) + (item.top.padding * screen.x) : 0f;
            float height = item.bottom.doScale ? (screen.y * item.bottom.scale) + (item.bottom.padding * screen.y) : 0f;
            float width = item.right.doScale ? (screen.x * item.right.scale) + (item.right.padding * screen.y) : 0f;
            item.target.position = new Vector3(x,y,0f);
            item.target.sizeDelta = new Vector2(width, height);
        }
        Canvas.ForceUpdateCanvases();
    }
}
