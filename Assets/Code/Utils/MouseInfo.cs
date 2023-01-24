using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bestagon.Hexagon;


/// <summary>
/// Provides access to information relative to the Mouse and its positioning within the game
/// </summary>
public class MouseInfo : Bestagon.Behaviours.ProtectedSingletonBehaviour<MouseInfo>
{
    static UnityEngine.Tilemaps.Tilemap _referenceMap;
    static UnityEngine.Tilemaps.Tilemap referenceMap
    {
        get
        {
            if (_referenceMap == null)
                _referenceMap = Object.FindObjectOfType<UnityEngine.Tilemaps.Tilemap>(false);
            return _referenceMap;
        }
    }
    public static Hex Hex()
    {
        return RoundAnyToHex(World());
    }

    public static Hex RoundAnyToHex(Vector3 v)
    {
        return Bestagon.Hexagon.Hex.FromTilemap(referenceMap.WorldToCell(v));
    }

    public static Vector3 World()
    {
        //Vector3 r = CustomCursor.GetWorldPosition();
        Vector3 r = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        r.z = 0.0f;
        return r;
    }

    protected override void Destroy()
    {
        _referenceMap = null;
    }
}
