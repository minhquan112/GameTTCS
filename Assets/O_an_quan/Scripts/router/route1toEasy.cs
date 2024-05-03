using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class route1toEasy : MonoBehaviour
{
    private string typeGame = "easy";
    void OnMouseDown()
    {
        UIManager.Ins.OnClose(1);
        StateManager.Ins.setTypeGame(typeGame);
        StateManager.Ins.openGamePlay();
    }
}
