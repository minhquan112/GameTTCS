using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class route1toDuo : MonoBehaviour
{
    private string typeGame = "duo";
    void OnMouseDown()
    {
        UIManager.Ins.OnClose(1);
        StateManager.Ins.setTypeGame(typeGame);
        StateManager.Ins.openGamePlay();
    }
}
