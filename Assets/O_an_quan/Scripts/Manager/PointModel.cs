using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointModel : MonoBehaviour
{
    private static PointModel ins;
    public static PointModel Ins
    {
        get { return PointModel.ins; }
    }

    public int[] dsPoint;
    void Awake()
    {
        PointModel.ins = this;
        initPoint();
    }

    void initPoint()
    {
        dsPoint = new int[14];
        dsPoint[0] = 0; // nv1
        dsPoint[1] = 0; // nv2
        for (int i = 2; i < 7; i++)
        {
            dsPoint[i] = 5; // hang tren
        }
        dsPoint[7] = 10; // Quan 1
        for (int i = 8; i < 13; i++)
        {
            dsPoint[i] = 5; // hang duoi
        }
        dsPoint[13] = 10; // Quan 2
    }

    public void resetPoint()
    {
        dsPoint[0] = 0; // nv1
        dsPoint[1] = 0; // nv2
        for (int i = 2; i < 7; i++)
        {
            dsPoint[i] = 5; // hang tren
        }
        dsPoint[7] = 10; // Quan 1
        for (int i = 8; i < 13; i++)
        {
            dsPoint[i] = 5; // hang duoi
        }
        dsPoint[13] = 10; // Quan 2
    }

    public int getValue(int index)
    {
        return dsPoint[index];
    }

    public void updatePoint(int index, int value)
    {
        dsPoint[index] = value;
    }
}
