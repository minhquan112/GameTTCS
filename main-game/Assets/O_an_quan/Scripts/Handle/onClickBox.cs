using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClickBox : MonoBehaviour
{
    public int index;
    void OnMouseDown()
    {
        handleState();
    }

    void handleState()
    {
        if (PointModel.Ins.getValue(index) == 0) return;
        StateManager.Ins.getCurIndex(index);
        StateManager.Ins.updatePosHand(index);
        StateManager.Ins.showDirect(index);
    }
}
