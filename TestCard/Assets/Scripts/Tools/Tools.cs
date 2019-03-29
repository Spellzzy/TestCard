using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class Tools {

    public static Vector2 GetScreenXY(Vector3 pos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
        //原点位置转换
        screenPos.y = Screen.height - screenPos.y;
        return GRoot.inst.GlobalToLocal(screenPos);
    }
}
