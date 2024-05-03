using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAI : MonoBehaviour
{
    private static HandleAI ins;
    public static HandleAI Ins
    {
        get { return HandleAI.ins; }
    }
    void Awake()
    {
        HandleAI.ins = this;
        // Ai AI = new Ai();
    }
    public int[] handle(string typeGame)
    {
        switch (typeGame)
        {
            case "easy":
                return caseEasy();

            case "medium":
                return caseMedium();

            case "hard":
                return caseHard();

            default:
                return null;
        }
    }

    private int[] caseEasy()
    {
        int[] result = Ai.EasyAi(PointModel.Ins.dsPoint, 2);
        return result;
    }
    private int[] caseMedium()
    {
        int[] result = Ai.MediumAi(PointModel.Ins.dsPoint, 2);
        return result;
    }
    private int[] caseHard()
    {
        int[] result = Ai.HardAi(PointModel.Ins.dsPoint, 2);
        return result;
    }
}
