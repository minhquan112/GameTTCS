using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class replay : MonoBehaviour
{
    void OnMouseDown()
    {
        PointModel.Ins.resetPoint();
        PointManager.Ins.updateScore();
        StateManager.Ins.resetStage();
    }
}
