using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorOperation
{
    public virtual void operate() { }    
    public virtual void reverse() { }
}

public class SetBlock : EditorOperation
{
    private int newBrush;
    private int oldBrush;
    private int cellX;
    private int cellY;
    private int cellZ;
    private Stage stage;

    public SetBlock(int brush, int cellX, int cellY, int cellZ, Stage stage)
    {
        newBrush = brush;
        oldBrush = 0;
        this.cellX = cellX;
        this.cellY = cellY;
        this.cellZ = cellZ;
        this.stage = stage;
    }

    public override void operate()
    {
        oldBrush = stage.GetBlock(cellX, cellY, cellZ);
        stage.SetBlock(newBrush, cellX, cellY, cellZ);
        stage.BuildMesh();
    }

    public override void reverse()
    {
        stage.SetBlock(oldBrush, cellX, cellY, cellZ);
        stage.BuildMesh();
    }
}