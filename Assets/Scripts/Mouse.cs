using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Texture2D cursorTexture;

    public Texture2D onClickTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        Cursor.visible = true;
    }

    void Update()
    {

    }

    private void OnMouseDown()
    {

    }

    private void OnMouseUp()
    {

    }
    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }


}