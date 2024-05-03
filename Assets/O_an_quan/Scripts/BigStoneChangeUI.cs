using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigStoneChangeUI : MonoBehaviour
{
    public int index;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;
    public Sprite sprite10;
    public Sprite sprite11;
    public Sprite sprite12;
    public Sprite sprite13;
    public Sprite sprite14;
    public Sprite sprite15;

    private new SpriteRenderer renderer;
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Phương thức để thay đổi Sprite thành sprite1
    public void changeSprite(int point)
    {
        if (point == 0)
        {
            renderer.sprite = null;
        }
        else if (point == 1)
        {
            renderer.sprite = sprite1;
        }
        else if (point == 2)
        {
            renderer.sprite = sprite2;
        }
        else if (point == 3)
        {
            renderer.sprite = sprite3;
        }
        else if (point == 4)
        {
            renderer.sprite = sprite4;
        }
        else if (point == 5)
        {
            renderer.sprite = sprite5;
        }
        else if (6 <= point && point <= 9)
        {
            renderer.sprite = sprite6;
        }
        else if (point == 10)
        {
            renderer.sprite = sprite10;
        }
        else if (point == 11)
        {
            renderer.sprite = sprite11;
        }
        else if (point == 12)
        {
            renderer.sprite = sprite12;
        }
        else if (point == 13)
        {
            renderer.sprite = sprite13;
        }
        else if (point == 14)
        {
            renderer.sprite = sprite14;
        }
        else
        {
            renderer.sprite = sprite15;
        }
    }
}
