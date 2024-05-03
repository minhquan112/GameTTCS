using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class exit : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("MapScenes");
        PointModel.Ins.resetPoint();
        PointManager.Ins.updateScore();
        StateManager.Ins.resetStage();
        StateManager.Ins.closeGamePlay();
        UIManager.Ins.OnOpen(0);
    }
}
