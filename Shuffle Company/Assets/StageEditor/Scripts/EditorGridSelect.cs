using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorGridSelect : MonoBehaviour
{
    public Stage stage;
    public EditorOperationManager editorOperationManager;

    private int gridX;
    public int GridX
    {
        get
        {
            return gridX;
        }
        set
        {
            gridX = Mathf.Clamp(value, 0, stage.Width - 1);
        }
    }

    private int gridY;
    public int GridY
    {
        get
        {
            return gridY;
        }
        set
        {
            gridY = Mathf.Clamp(value, 0, stage.Height - 1);
        }
    }

    private int gridZ;
    public int GridZ
    {
        get
        {
            return gridZ;
        }
        set
        {
            gridZ = Mathf.Clamp(value, 0, stage.Depth - 1);
        }
    }


    void Update ()
    {
        // Cast ray into scene to decide where to place editor grid selector
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Get mesh renderer to turn on and off if the mouse leaves the grid
        MeshRenderer meshRender = GetComponent<MeshRenderer>();

        if (Physics.Raycast(ray, out hit, 30))
        {
            meshRender.enabled = true;

            // Since changes in the grid y axis are not caused by collision with the edior grid, calculate it regardless
            GridY = Mathf.FloorToInt(transform.position.y);

            // Make sure the cast is against the editor grid
            if (hit.collider.gameObject.name == "EditorGrid")
            {
                GridX = Mathf.FloorToInt(hit.point.x);
                GridZ = Mathf.FloorToInt(hit.point.z);

                UpdateWorldPosition();
            }

            // If selecting cell in grid and mouse clicked, do proper operation
            // Add block
            if (Input.GetMouseButton(0) && stage.GetBlock(gridX, gridY, gridZ) != 1)
            {
                SetBlock setBlockOp = new SetBlock(1, gridX, gridY, gridZ, stage);
                editorOperationManager.doOperation(setBlockOp);
            }

            // Remove block
            if (Input.GetMouseButton(1))
            {
                SetBlock setVoidBlockOp = new SetBlock(0, gridX, gridY, gridZ, stage);
                editorOperationManager.doOperation(setVoidBlockOp);
            }
        }
        else
        {
            meshRender.enabled = false;
        }
    }

    public void UpdateWorldPosition()
    {
        // Move selector to world space location coresponding to the grid space
        float worldSpaceX = gridX + 0.5f;
        float worldSpaceZ = gridZ + 0.5f;
        transform.position = new Vector3(worldSpaceX, transform.position.y, worldSpaceZ);
    }
}
