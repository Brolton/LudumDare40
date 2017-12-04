using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTexture;

    void Start ()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(20.5f, 20.5f), CursorMode.Auto);
    }
}
