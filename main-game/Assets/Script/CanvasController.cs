using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance;
    public bool canClickBox = true;
    
    public void SetCanClickBox(bool canClick)
    {
        canClickBox = canClick;
    }

    public void DebugLogger()
    {
        
    }
}
