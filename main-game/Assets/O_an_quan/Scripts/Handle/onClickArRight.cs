using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClickArRight : MonoBehaviour
{
    void OnMouseDown()
    {
        StateManager.Ins.setDirect("right");
    }
}
