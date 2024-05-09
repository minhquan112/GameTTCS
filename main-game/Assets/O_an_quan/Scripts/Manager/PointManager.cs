using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    private static PointManager ins;
    public static PointManager Ins
    {
        get { return PointManager.ins; }
    }
    void Awake()
    {
        PointManager.ins = this;
    }
    public TextMeshProUGUI[] listScoreText;
    public stoneChangeUI[] listUIStone;
    public BigStoneChangeUI[] listUIBigStone;

    void Start()
    {
        initScore();
    }

    public void initScore()
    {
        for (int i = 0; i < 14; i++)
        {
            listScoreText[i].text = PointModel.Ins.dsPoint[i].ToString();
        }
    }

    private void setEnabledText(int index)
    {
        if (PointModel.Ins.dsPoint[index] == 0) listScoreText[index].enabled = false;
        else listScoreText[index].enabled = true;
    }
    public void updateScore()
    {
        for (int i = 0; i < 14; i++)
        {
            if (i >= 2) setEnabledText(i);
            listScoreText[i].text = PointModel.Ins.dsPoint[i].ToString();
            if (listUIStone[i] != null) listUIStone[i].changeSprite(PointModel.Ins.dsPoint[i]);
            if (listUIBigStone[i] != null) listUIBigStone[i].changeSprite(PointModel.Ins.dsPoint[i]);
        }
    }
}
