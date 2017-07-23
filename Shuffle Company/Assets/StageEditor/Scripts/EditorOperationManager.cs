using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorOperationManager : MonoBehaviour
{
    public int operationMemorySize = 64;

    private int currentOperation = 0;
    private List<EditorOperation> operationMemory = new List<EditorOperation>();

    public void doOperation(EditorOperation op)
    {
        op.operate();

        if (currentOperation != 0)
        {
            operationMemory.RemoveRange(0, currentOperation);
            currentOperation = 0;
        }

        operationMemory.Insert(0, op);

        if (operationMemory.Count >= operationMemorySize)
        {
            operationMemory.RemoveAt(operationMemory.Count - 1);
        }
    }

    public void undoOperation()
    {
        if (currentOperation < operationMemory.Count)
        {
            operationMemory[currentOperation].reverse();
            currentOperation++;
        }
    }

    public void redoOperation()
    {
        if (currentOperation > 0)
        {
            currentOperation--;
            operationMemory[currentOperation].operate();
        }
    }

    void Update()
    {
        // Undo Command
        if (Input.GetKeyDown(KeyCode.Z))
        {
            undoOperation();
        }

        // Redo Command
        if (Input.GetKeyDown(KeyCode.Y))
        {
            redoOperation();
        }
    }
}
