using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorGrid : MonoBehaviour
{
    public Stage stage;
    public EditorGridSelect editorGridSelect;

    public void SetGridSizeX(string x)
    {
        stage.width = int.Parse(x);
        UpdateEditorGrid();
    }

    public void SetGridSizeZ(string z)
    {
        stage.depth = int.Parse(z);
        UpdateEditorGrid();
    }

    private void UpdateEditorGrid()
    {
        // Shift and resize editor grid to match new size
        transform.localScale = new Vector3(stage.width, stage.depth, 1f);
        transform.position = new Vector3(stage.width / 2f, transform.position.y, stage.depth / 2f);

        // Set tiling of texture in shader to match new size
        MeshRenderer meshRender = GetComponent<MeshRenderer>();
        meshRender.material.SetVector("_MainTex_ST", new Vector4(stage.width, stage.depth, 0f, 0f));

        // If needed, reset grid select position
        if (editorGridSelect.GridX >= stage.width)
        {
            editorGridSelect.GridX = stage.width - 1;
            editorGridSelect.UpdatePosition();
        }

        if (editorGridSelect.GridZ >= stage.depth)
        {
            editorGridSelect.GridZ = stage.depth - 1;
            editorGridSelect.UpdatePosition();
        }
    }
}
