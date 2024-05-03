using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClickArLeft : MonoBehaviour
{
    void OnMouseDown()
    {
        StateManager.Ins.setDirect("left");
    }
}
