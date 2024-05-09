using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class route1toMedium : MonoBehaviour
{
    private string typeGame = "medium";
    void OnMouseDown()
    {
        UIManager.Ins.OnClose(1);
        StateManager.Ins.setTypeGame(typeGame);
        StateManager.Ins.openGamePlay();
    }
}
