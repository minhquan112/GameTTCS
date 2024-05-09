using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class route2to0 : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseDown()
    {
        UIManager.Ins.OnClose(2);
        UIManager.Ins.OnOpen(0);
    }
}
