using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bestagon.Mobile
{
    public static class MobileUtils
    {
        /// <summary>
        /// Returns the resolution of the screen, or of the set resolution of the game view panel in the editor
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetMainGameViewSize()
        {
#if UNITY_EDITOR
            System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
            System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            System.Object Res = GetSizeOfMainGameView.Invoke(null, null);

            return (Vector2)Res;
#else
        return new Vector2(Screen.width, Screen.height);
#endif
        }
    }
}
