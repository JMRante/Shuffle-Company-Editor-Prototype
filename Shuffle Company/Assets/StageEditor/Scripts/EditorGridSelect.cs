using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorGridSelect : MonoBehaviour
{
    public Stage stage;

    private int gridX;
    public int GridX
    {
        get
        {
            return gridX;
        }
        set
        {
            gridX = Mathf.Clamp(value, 0, stage.width - 1);
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
            gridY = Mathf.Clamp(value, 0, stage.height - 1);
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
            gridZ = Mathf.Clamp(value, 0, stage.depth - 1);
        }
    }


    void Update ()
    {
        // Cast ray into scene to decide where to place editor grid selector
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30))
        {
            // Make sure the cast is against the editor grid
            if (hit.collider.gameObject.name == "EditorGrid")
            {
                gridX = Mathf.Clamp(Mathf.FloorToInt(hit.point.x), 0, stage.width - 1);
                gridY = (int)transform.position.y;
                gridZ = Mathf.Clamp(Mathf.FloorToInt(hit.point.z), 0, stage.depth - 1);

                UpdatePosition();
            }
        }
    }

    public void UpdatePosition()
    {
        // Move selector to world space location coresponding to the grid space
        float worldSpaceX = gridX + 0.5f;
        float worldSpaceZ = gridZ + 0.5f;
        transform.position = new Vector3(worldSpaceX, transform.position.y, worldSpaceZ);
    }
}
