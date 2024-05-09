using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class route0to1 : MonoBehaviour
{
    void OnMouseDown()
    {
        UIManager.Ins.OnClose(0);
        UIManager.Ins.OnOpen(1);
    }
}
