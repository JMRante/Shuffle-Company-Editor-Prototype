using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorUI : MonoBehaviour
{
    public Stage stage;

    public InputField offsetXInput;
    public InputField offsetYInput;
    public InputField offsetZInput;

    public void SubmitOffset()
    {
        // Get input for offsetting; no input for an axis means no offsetting.
        int shiftX = 0;
        int shiftY = 0;
        int shiftZ = 0;

        if (offsetXInput.text != "")
        {
            shiftX = int.Parse(offsetXInput.text);
        }

        if (offsetYInput.text != "")
        {
            shiftY = int.Parse(offsetYInput.text);
        }

        if (offsetZInput.text != "")
        {
            shiftZ = int.Parse(offsetZInput.text);
        }

        // Offset
        stage.ShiftBlocks(shiftX, shiftY, shiftZ);
        stage.BuildMesh();

        // Reset input
        offsetXInput.text = "";
        offsetYInput.text = "";
        offsetZInput.text = "";
    }
}
