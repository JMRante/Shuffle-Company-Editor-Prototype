using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorUI : MonoBehaviour
{
    public void OpenEditorMenu(GameObject go)
    {
        go.SetActive(true);
    }

    public void CloseEditorMenu(GameObject go)
    {
        go.SetActive(false);
    }
}
