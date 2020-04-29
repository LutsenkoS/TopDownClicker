using UnityEngine;
using System.Collections;

public static class ScreenHelper
{
    private static Vector2 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    
    public static Vector2 ScreenBounds()
    {
        return new Vector2(bounds.x, bounds.y);
    }
    
}
