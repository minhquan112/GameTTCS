using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    
   
    public List<SpriteRenderer> listAreas = new List<SpriteRenderer>();

    void OnMouseDown()
    {

        // if (!CanvasController.Instance.canClickBox) return;
        // Debug.Log(CanvasController.Instance.canClickBox);

        // Debug.Log("Click hover");
        if (PlayerHealth.currentHealth >= 10)
        {
            Controller.Instance.OnCLick(this.GetComponent<SpriteRenderer>());
        }
       
       
    }
}
