using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCamera : MonoBehaviour
{
    public float mouseSpeed = 0.5f;
    public float keySpeed = 5.0f;

    public GameObject editorGrid;
    public GameObject editorGridSelect;

    private int layer = 0;

	void Update ()
    {
        Vector3 translate;

        // First handle smooth horizontal/vertical movement, choosing whether to get mouse or key input
        if (Input.GetButton("Drag"))
        {
            translate = new Vector3(Input.GetAxis("MouseX"), 0f, Input.GetAxis("MouseY")) * mouseSpeed * Time.deltaTime;
        }
        else
        {
            translate = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized * keySpeed * Time.deltaTime;
        }

        // Then handle switching layers, moving both the camera and editing grid discretely
        string layerSwitchInput = "Depth";

        if (Input.GetAxis("MouseWheel") != 0f)
        {
            layerSwitchInput = "MouseWheel";
        }

        // Get relative layer to switch to
        int layerSwitchDirection = 0;

        if (Input.GetAxis(layerSwitchInput) > 0f && layer < 9)
        {
            layerSwitchDirection = 1;

        }
        else if (Input.GetAxis(layerSwitchInput) < 0f && layer > 0)
        {
            layerSwitchDirection = -1;
        }

        // Switch
        translate.y = layerSwitchDirection;
        layer += layerSwitchDirection;

        // When changing position of the editor grid and friends, the direction is inverted since the quads face down
        editorGrid.transform.Translate(new Vector3(0f, 0f, -layerSwitchDirection));
        editorGridSelect.transform.Translate(new Vector3(0f, 0f, -layerSwitchDirection));

        // Move camera
        transform.Translate(translate);
	}
}
