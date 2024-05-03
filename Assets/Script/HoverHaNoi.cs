using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverHaNoi : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnMouseExit() {
    
        var m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = Color.red;
       
    }
    void OnMouseDown()
    {
        var m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = Color.green;
       
    }
}
