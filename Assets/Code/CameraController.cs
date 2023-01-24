using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Bestagon.Behaviours.ProtectedSingletonBehaviour<CameraController>
{
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    public static void CenterCameraOnArea(Vector2Int area, float padding)
    {
        //Find the camera vert to use based on the amount of needed width
        float orthoSizeRaw;
        int horizCount;

        //Loop until we find the calculated vertical size that allows us to fit the needed vertical and horizontal space
        int orthoFitHeight = area.y - 1;
        do
        {
            orthoFitHeight++;
            orthoSizeRaw = CalculateOrthoSizeForVertHexFit(orthoFitHeight);
            horizCount = CalcHorizontalHexFit(orthoFitHeight);
        } while (horizCount < area.x);
        Instance.cam.orthographicSize = (orthoSizeRaw / 2f) + padding;
        Vector2 a = Bestagon.Hexagon.Hex.FromTilemap(new Vector3Int(0, 0)).UnityPosition();
        Vector2 b = Bestagon.Hexagon.Hex.FromTilemap(new Vector3Int(area.x - 1, area.y - 1)).UnityPosition();
        Vector2 lerpedPos = Vector2.Lerp(a, b, 0.5f);
        Instance.transform.position = new Vector3(((area.x - .5f) * Bestagon.Hexagon.Hex.SQRT3DIV2) / 2f, lerpedPos.y, -10f);
    }

    static float CalculateOrthoSizeForVertHexFit(int amtVertHexes)
    {
        return (1 + ((3f / 4f) * (amtVertHexes - 1))); ;
    }


    static int CalcHorizontalHexFit(int amtVertHexes)
    {
        //How many horizontal hexes we have on the screen
        float horizontalCountRaw = CalculateOrthoSizeForVertHexFit(amtVertHexes) * CalcScreenRatio();
        //Since hexagon rows are offset, we need N + 0.5f space for the row above, so make sure we have enough space for the offset row, so the actual count can be different from the floor of horizontal count raw
        return Mathf.FloorToInt(horizontalCountRaw) - ((horizontalCountRaw - Mathf.FloorToInt(horizontalCountRaw) >= 0.5f) ? 0 : 1);
    }

    static float CalcScreenRatio()
    {
        Vector2 screen = Bestagon.Mobile.MobileUtils.GetMainGameViewSize();
        //How many Hexes appear horizontally on screen
        //We have ortho size set to 1, so we know that there is always the ratio amount wide.
        float ratio = (float)(screen.x * Instance.cam.rect.width) / (float)(screen.y * Instance.cam.rect.height) / Bestagon.Hexagon.Hex.SQRT3DIV2;
        //Debug.LogFormat("{0}x{1} = {2}", screen.x, screen.y, ratio);

        return ratio;
    }



    protected override void Destroy()
    {
    }
}
