using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager ins;
    public static UIManager Ins
    {
        get { return UIManager.ins; }
    }

    [SerializeField]
    private GameObject[] prefabs;

    private GameObject[] roots;
    private GameObject[] canvas;

    protected void Awake()
    {
        UIManager.ins = this;
        this.roots = new GameObject[prefabs.Length];
        this.canvas = new GameObject[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            this.roots[i] = new GameObject();
            this.roots[i].transform.SetParent(this.transform);
        }
    }

    // Open UI by index
    public void OnOpen(int index)
    {
        if (this.canvas[index] == null)
        {
            this.canvas[index] = Instantiate(this.prefabs[index]);
            this.canvas[index].transform.SetParent(this.roots[index].transform);
        }

        this.canvas[index].SetActive(true);
    }

    // Close UI by index
    public void OnClose(int index)
    {
        if (this.canvas[index] != null)
        {
            this.canvas[index].SetActive(false);
        }
    }
}