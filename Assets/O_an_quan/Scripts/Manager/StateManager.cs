using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static StateManager ins;
    public static StateManager Ins
    {
        get { return StateManager.ins; }
    }
    void Awake()
    {
        StateManager.ins = this;
    }
    private int curIndex;
    private int turn = 2;
    private bool isBot = false;
    private bool isLoseGame = false;
    private string typeGame = "";
    public Hand hand;
    public GameObject direct;
    public GameObject top;
    public GameObject bot;
    public GameObject flagTop;
    public GameObject flagBot;
    public GameObject nv1Win;
    public GameObject nv2Win;
    public GameObject UIEndGame;
    public GameObject gamePlay;

    public GameObject[] Stage;
    public GameObject[] StageAct;

    void Start()
    {
        UIManager.Ins.OnOpen(0);
    }

    public void openGamePlay()
    {
        gamePlay.SetActive(true);
        bot.SetActive(true);
    }
    public void closeGamePlay()
    {
        gamePlay.SetActive(false);
        bot.SetActive(false);
        top.SetActive(false);
    }
    public void resetStage()
    {
        turn = 1;
        isLoseGame = false;
        UIEndGame.SetActive(false);
        changeTurn();
        nv1Win.SetActive(false);
        nv2Win.SetActive(false);
        for (int i = 2; i < 14; i++)
        {
            Stage[i].SetActive(true);
        }
    }
    private void changeTurn()
    {
        if (turn == 1)
        {
            top.SetActive(false);
            flagTop.SetActive(false);
            bot.SetActive(true);
            flagBot.SetActive(true);
            turn = 2;
        }
        else if (turn == 2)
        {
            if (!isBot)
            { // neu la nguoi
                top.SetActive(true);
                flagTop.SetActive(true);
                bot.SetActive(false);
                flagBot.SetActive(false);
            }
            else
            {
                bot.SetActive(false);
                flagBot.SetActive(false);
            }
            turn = 1;
        }
    }
    public void getCurIndex(int index)
    {
        curIndex = index;
    }

    private int editIndex(int index)
    {
        if (index == 1) index = 13;
        if (index == 14) index = 2;
        if (index == 15) index = 3;
        if (index == 0) index = 12;
        return index;
    }

    private void onLoseGame()
    {
        top.SetActive(false);
        bot.SetActive(false);
        if (PointModel.Ins.dsPoint[0] > PointModel.Ins.dsPoint[1])
        {
            nv2Win.SetActive(true);
        }
        else
        {
            nv1Win.SetActive(true);
        }
        UIEndGame.SetActive(true);
    }

    private void checkStateGame()
    {
        hand.hide();
        checkOutOfStone();
        if (checkGameLose())
        {
            onLoseGame();
            isLoseGame = true;
        }
    }

    public void setTypeGame(string type)
    {
        this.typeGame = type;
    }
    public void setDirect(string dir)
    {
        hideDirect();
        switch (typeGame)
        {
            case "duo":
                caseDuo(dir);
                break;

            default:
                caseBot(dir);
                break;
        }
    }
    private void caseDuo(string dir)
    {
        int times = PointModel.Ins.dsPoint[curIndex];
        int isTop = 1;
        updateState(curIndex, 0);
        if (2 <= curIndex && curIndex <= 6) isTop = 1;
        else isTop = -1;
        if (dir == "left")
        {
            StartCoroutine(RepeatedAction(times, -1 * isTop));
        }
        else if (dir == "right")
        {
            StartCoroutine(RepeatedAction(times, 1 * isTop));
        }
        changeTurn();
    }
    private void caseBot(string dir)
    {
        isBot = true;
        int times = PointModel.Ins.dsPoint[curIndex];
        updateState(curIndex, 0);
        if (dir == "left")
        {
            StartCoroutine(WaitForRepeatedActionCompletion(times, 1));
        }
        else if (dir == "right")
        {
            StartCoroutine(WaitForRepeatedActionCompletion(times, -1));
        }
        changeTurn();
    }

    private IEnumerator WaitForRepeatedActionCompletion(int times, int direction)
    {
        yield return StartCoroutine(RepeatedAction(times, direction)); // Chờ đợi cho đến khi RepeatedAction hoàn thành

        // Sau khi RepeatedAction hoàn thành, thực hiện DelayedAIPlay
        if (!isLoseGame) StartCoroutine(DelayedAIPlay());
    }

    private IEnumerator DelayedAIPlay()
    {
        yield return new WaitForSeconds(1f); // Chờ 1 giây
        AIPlay(); // Gọi hàm AIPlay sau khi chờ
    }

    private void AIPlay()
    {
        checkGameLose();
        int[] resultAI = HandleAI.Ins.handle(typeGame);
        int indexAI = resultAI[0] + 2;
        int dirAI = resultAI[1];
        getCurIndex(indexAI);
        updatePosHand(indexAI);
        int times = PointModel.Ins.dsPoint[curIndex];
        updateState(curIndex, 0);
        if (dirAI == 0)
        {
            StartCoroutine(RepeatedAction(times, -1));
        }
        else if (dirAI == 1)
        {
            StartCoroutine(RepeatedAction(times, 1));
        }
        changeTurn();
    }
    IEnumerator RepeatedAction(int times, int dir)
    {
        while (true)
        {
            for (int i = 0; i < times; i++)
            {
                curIndex += dir;
                curIndex = editIndex(curIndex);
                handleAction(curIndex);
                yield return new WaitForSeconds(0.4f);
            }
            hand.hide();
            if (curIndex + dir == 7 || curIndex + dir == 13 || curIndex + dir == 1)
            {
                checkStateGame();
                break;
            }
            int nextIndex = curIndex + dir;
            nextIndex = editIndex(nextIndex);
            if (PointModel.Ins.dsPoint[nextIndex] > 0) // choi tiep
            {
                curIndex = nextIndex;
                times = PointModel.Ins.dsPoint[curIndex];
                handleHand(curIndex);
                yield return new WaitForSeconds(0.6f);
                updateState(curIndex, 0);
            }
            else if (PointModel.Ins.dsPoint[nextIndex] == 0) // an
            {
                int idxEat = nextIndex + dir;
                idxEat = editIndex(idxEat);
                if (PointModel.Ins.dsPoint[idxEat] > 0)
                {
                    handleHand(idxEat);
                    yield return new WaitForSeconds(0.4f);
                    updateResult(idxEat); // them diem
                    updateState(idxEat, 0);
                    nextIndex = idxEat + dir;
                    nextIndex = editIndex(nextIndex);
                    if (idxEat + dir == 7 || idxEat + dir == 13 || idxEat + dir == 1)
                    {
                        checkStateGame();
                        break;
                    }
                    if (PointModel.Ins.dsPoint[nextIndex] == 0)
                    {
                        idxEat += 2 * dir;
                        idxEat = editIndex(idxEat);
                        if (PointModel.Ins.dsPoint[idxEat] > 0)
                        {
                            handleHand(idxEat);
                            yield return new WaitForSeconds(0.4f);
                            hand.hide();
                            updateResult(idxEat);
                            updateState(idxEat, 0);
                            nextIndex = idxEat + dir;
                            nextIndex = editIndex(nextIndex);
                            if (idxEat + dir == 7 || idxEat + dir == 13 || idxEat + dir == 1)
                            {
                                checkStateGame();
                                break;
                            }
                            if (PointModel.Ins.dsPoint[nextIndex] == 0)
                            {
                                idxEat += 2 * dir;
                                idxEat = editIndex(idxEat);
                                if (PointModel.Ins.dsPoint[idxEat] > 0)
                                {
                                    handleHand(idxEat);
                                    yield return new WaitForSeconds(0.4f);
                                    hand.hide();
                                    updateResult(idxEat);
                                    updateState(idxEat, 0);
                                    nextIndex = idxEat + dir;
                                    nextIndex = editIndex(nextIndex);
                                    if (idxEat + dir == 7 || idxEat + dir == 13 || idxEat + dir == 1)
                                    {
                                        checkStateGame();
                                        break;
                                    }
                                    if (PointModel.Ins.dsPoint[nextIndex] == 0)
                                    {
                                        idxEat += 2 * dir;
                                        idxEat = editIndex(idxEat);
                                        if (PointModel.Ins.dsPoint[idxEat] > 0)
                                        {
                                            handleHand(idxEat);
                                            yield return new WaitForSeconds(0.4f);
                                            hand.hide();
                                            updateResult(idxEat);
                                            updateState(idxEat, 0);
                                            nextIndex = idxEat + dir;
                                            nextIndex = editIndex(nextIndex);
                                            if (idxEat + dir == 7 || idxEat + dir == 13 || idxEat + dir == 1)
                                            {
                                                checkStateGame();
                                                break;
                                            }
                                            if (PointModel.Ins.dsPoint[nextIndex] == 0)
                                            {
                                                idxEat += 2 * dir;
                                                idxEat = editIndex(idxEat);
                                                if (PointModel.Ins.dsPoint[idxEat] > 0)
                                                {
                                                    handleHand(idxEat);
                                                    yield return new WaitForSeconds(0.4f);
                                                    hand.hide();
                                                    updateResult(idxEat);
                                                    updateState(idxEat, 0);
                                                    nextIndex = idxEat + dir;
                                                    nextIndex = editIndex(nextIndex);
                                                    if (idxEat + dir == 7 || idxEat + dir == 13 || idxEat + dir == 1)
                                                    {
                                                        checkStateGame();
                                                        break;
                                                    }
                                                    if (PointModel.Ins.dsPoint[nextIndex] == 0)
                                                    {
                                                        idxEat += 2 * dir;
                                                        idxEat = editIndex(idxEat);
                                                        if (PointModel.Ins.dsPoint[idxEat] > 0)
                                                        {
                                                            handleHand(idxEat);
                                                            yield return new WaitForSeconds(0.4f);
                                                            hand.hide();
                                                            updateResult(idxEat);
                                                            updateState(idxEat, 0);
                                                            nextIndex = idxEat + dir;
                                                            nextIndex = editIndex(nextIndex);
                                                            if (idxEat + dir == 7 || idxEat + dir == 13 || idxEat + dir == 1)
                                                            {
                                                                checkStateGame();
                                                                break;
                                                            }
                                                            if (PointModel.Ins.dsPoint[nextIndex] == 0)
                                                            {
                                                                idxEat += 2 * dir;
                                                                idxEat = editIndex(idxEat);
                                                                if (PointModel.Ins.dsPoint[idxEat] > 0)
                                                                {
                                                                    handleHand(idxEat);
                                                                    yield return new WaitForSeconds(0.4f);
                                                                    hand.hide();
                                                                    updateResult(idxEat);
                                                                    updateState(idxEat, 0);
                                                                }
                                                                else hand.hide();
                                                                checkStateGame();
                                                            }
                                                        }
                                                        else hand.hide();
                                                        checkStateGame();
                                                    }
                                                }
                                                else hand.hide();
                                                checkStateGame();
                                            }
                                        }
                                        else hand.hide();
                                        checkStateGame();
                                    }
                                }
                                else hand.hide();
                                checkStateGame();
                            }
                            else hand.hide();
                        }
                    }
                    else hand.hide();
                }
                checkStateGame();
                break;
            };
        }
    }

    private bool checkGameLose()
    {
        if (PointModel.Ins.dsPoint[7] == 0 && PointModel.Ins.dsPoint[13] == 0)
        {
            return true;
        }
        return false;
    }

    private void checkOutOfStone()
    {
        if (isLoseGame) return;
        if (turn == 1 &&
            PointModel.Ins.dsPoint[2] == 0 &&
            PointModel.Ins.dsPoint[3] == 0 &&
            PointModel.Ins.dsPoint[4] == 0 &&
            PointModel.Ins.dsPoint[5] == 0 &&
            PointModel.Ins.dsPoint[6] == 0
        )
        {
            updateState(2, 1);
            updateState(3, 1);
            updateState(4, 1);
            updateState(5, 1);
            updateState(6, 1);
            updateUI(0, PointModel.Ins.dsPoint[0] - 5);
        }
        if (turn == 2 &&
            PointModel.Ins.dsPoint[8] == 0 &&
            PointModel.Ins.dsPoint[9] == 0 &&
            PointModel.Ins.dsPoint[10] == 0 &&
            PointModel.Ins.dsPoint[11] == 0 &&
            PointModel.Ins.dsPoint[12] == 0
        )
        {
            updateState(8, 1);
            updateState(9, 1);
            updateState(10, 1);
            updateState(11, 1);
            updateState(12, 1);
            updateUI(1, PointModel.Ins.dsPoint[0] - 5);
        }
    }

    private void updateResult(int index)
    {
        int indexNv = 2 - turn;
        int newValue = PointModel.Ins.dsPoint[indexNv] + PointModel.Ins.dsPoint[index];
        PointModel.Ins.updatePoint(indexNv, newValue);
        PointManager.Ins.updateScore();
    }

    private void handleHand(int index)
    {
        hand.show();
        hand.moveTo(Stage[index].transform.position);
    }

    private void handleAction(int index)
    {
        handleHand(index);
        updateState(index, PointModel.Ins.dsPoint[index] + 1);
    }


    private void updateUI(int index, int value)
    {
        PointModel.Ins.updatePoint(index, value);
        PointManager.Ins.updateScore();
    }

    private void updateState(int index, int value)
    {
        index = editIndex(index);
        if (value > 0) Stage[index].SetActive(true);
        else if (value == 0) Stage[index].SetActive(false);
        updateUI(index, value);
    }

    public void showDirect(int index)
    {
        direct.SetActive(true);
        direct.transform.position = StageAct[curIndex].transform.position;
    }
    private void hideDirect()
    {
        direct.SetActive(false);
    }

    public void updatePosHand(int index)
    {
        Vector3 newPos = Stage[index].transform.position;
        hand.show();
        hand.updatePos(newPos);
    }
}
