using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Texture2D cursorIcon;
    private void Start()
    {
        Cursor.SetCursor(cursorIcon, new Vector2(cursorIcon.width / 2, cursorIcon.height / 2), CursorMode.Auto);
    }
    public void OnClickGameStartButton()
    {
        Debug.Log("Click");
    }
    public void OnClickExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void OnnClickHowToButton()
    {
        Debug.Log("Click");
    }
}
