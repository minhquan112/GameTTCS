using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneChangeUI : MonoBehaviour
{
    public int index;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;

    private new SpriteRenderer renderer;
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Phương thức để thay đổi Sprite thành sprite1
    public void changeSprite(int point)
    {
        switch (point)
        {
            case 0:
                renderer.sprite = null;
                break;
            case 1:
                renderer.sprite = sprite1;
                break;
            case 2:
                renderer.sprite = sprite2;
                break;
            case 3:
                renderer.sprite = sprite3;
                break;
            case 4:
                renderer.sprite = sprite4;
                break;
            case 5:
                renderer.sprite = sprite5;
                break;
            default:
                renderer.sprite = sprite6;
                break;
        }
    }
}
