using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorGrid : MonoBehaviour
{
    public Stage stage;
    public EditorGridSelect editorGridSelect;

    public InputField widthInput;
    public InputField depthIinput;

    public void SetGridSizeX(string x)
    {
        // Set new grid size
        stage.Width = int.Parse(x);

        // Set inbounds value back to input
        widthInput.text = stage.Width.ToString();

        // Remove all blocks outside grid
        stage.RemoveBlocksOutsideWidth();
        stage.BuildMesh();

        // Update grid object to match new size
        UpdateEditorGrid();
    }

    public void SetGridSizeZ(string z)
    {
        // Set new grid size
        stage.Depth = int.Parse(z);

        // Set inbounds value back to input
        depthIinput.text = stage.Depth.ToString();

        // Remove all blocks outside grid
        stage.RemoveBlocksOutsideDepth();
        stage.BuildMesh();

        // Update grid object to match new size
        UpdateEditorGrid();
    }

    private void UpdateEditorGrid()
    {
        // Shift and resize editor grid to match new size
        transform.localScale = new Vector3(stage.Width, stage.Depth, 1f);
        transform.position = new Vector3(stage.Width / 2f, transform.position.y, stage.Depth / 2f);

        // Set tiling of texture in shader to match new size
        MeshRenderer meshRender = GetComponent<MeshRenderer>();
        meshRender.material.SetVector("_MainTex_ST", new Vector4(stage.Width, stage.Depth, 0f, 0f));

        // If needed, reset grid select position
        if (editorGridSelect.GridX >= stage.Width)
        {
            editorGridSelect.GridX = stage.Width - 1;
            editorGridSelect.UpdateWorldPosition();
        }

        if (editorGridSelect.GridZ >= stage.Depth)
        {
            editorGridSelect.GridZ = stage.Depth - 1;
            editorGridSelect.UpdateWorldPosition();
        }
    }
}
