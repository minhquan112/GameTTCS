using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

namespace GameVSPLAYER {

    public class GameScriptPVP : MonoBehaviour
    {
        public string gameMode;
        public GameObject cross, nought, bar;

        public Text Instructions, player2Name;

        public enum Seed { EMPTY, CROSS, NOUGHT };

        public Seed Turn;

        public GameObject[] allSpawns = new GameObject[25];

        public Seed[] player = new Seed[25];

        Vector2 pos1, pos2;

        private void Awake()
        {
            GameObject persistantObj = GameObject.FindGameObjectWithTag ("PersistantObj") as GameObject;
            gameMode = persistantObj.GetComponent<PersistantScript> ().gameMode;
            Destroy (persistantObj);

            player2Name.text = "2nd Player";

            Turn = Seed.CROSS;

            Instructions.text = "Turn: 1st Player";

            for (int i = 0; i < 25; i++)
                player[i] = Seed.EMPTY;

        }

        public void SpawnPVP(GameObject emptycell, int id)
        {
            if (Turn == Seed.CROSS)
            {
                allSpawns[id] = Instantiate(cross, emptycell.transform.position, Quaternion.identity);
                player[id] = Turn;

                if (Won(Turn))
                {
                    Turn = Seed.EMPTY;

                    Instructions.text = "Player-1 has won!!!";

                    float slope = calculateSlope();
                    Instantiate(bar, calculateCenter(), Quaternion.Euler(0, 0, slope));
                }
                else
                {
                    Turn = Seed.NOUGHT;

                    Instructions.text = "Turn: 2nd Player";
                }
            }

            else if (Turn == Seed.NOUGHT && gameMode == "2player")
            {
                allSpawns[id] = Instantiate(nought, emptycell.transform.position, Quaternion.identity);
                player[id] = Turn;

                if (Won(Turn))
                {
                    Turn = Seed.EMPTY;

                    Instructions.text = "Player-2 has won!!!";

                    float slope = calculateSlope();

                    Instantiate(bar, calculateCenter(), Quaternion.Euler(0, 0, slope));
                }
                else
                {
                    Turn = Seed.CROSS;
                    Instructions.text = "Turn: 1st Player";
                }
            }

            if (IsDraw())
            {
                Turn = Seed.EMPTY;

                Instructions.text = "It is a draw!!";
            }

            Destroy(emptycell);
        }

        bool IsAnyEmpty()
        {
            bool empty = false;
            for (int i = 0; i < 25; i++)
            {
                if (player[i] == Seed.EMPTY)
                {
                    empty = true;
                    break;
                }
            }
            return empty;
        }

        bool Won(Seed currPlayer)
        {
            bool hasWon = false;

            int[,] allConditions = new int[28, 4] {
                {0, 1, 2, 3}, {1, 2, 3, 4},
                {5, 6, 7, 8}, {6, 7, 8, 9},
                {10, 11, 12, 13}, {11, 12, 13, 14},
                {15, 16, 17, 18}, {16, 17, 18, 19},
                {20, 21, 22, 23}, {21, 22, 23, 24},

                {0, 5, 10, 15}, {5, 10, 15, 20},
                {1, 6, 11, 16}, {6, 11, 16, 21},
                {2, 7, 12, 17}, {7, 12, 17, 22},
                {3, 8, 13, 18}, {8, 13, 18, 23},
                {4, 9, 14, 19}, {9, 14, 19, 24},

                {0, 6, 12, 18}, {1, 7, 13, 19},
                {5, 11, 17, 23}, {6, 12, 18, 24},

                {3, 7, 11, 15}, {4, 8, 12, 16},
                {8, 12, 16, 20}, {9, 13, 17, 21}
            };

            for (int i = 0; i < 28; i++)
            {
                if (player[allConditions[i, 0]] == currPlayer &
                    player[allConditions[i, 1]] == currPlayer &
                    player[allConditions[i, 2]] == currPlayer &
                    player[allConditions[i, 3]] == currPlayer)
                {
                    hasWon = true;

                    pos1 = allSpawns[allConditions[i, 0]].transform.position;
                    pos2 = allSpawns[allConditions[i, 3]].transform.position;
                    break;
                }
            }
            return hasWon;
        }

        bool IsDraw()
        {
            bool player1Won, player2Won, anyEmpty;

            player1Won = Won(Seed.CROSS);

            player2Won = Won(Seed.NOUGHT);

            anyEmpty = IsAnyEmpty();

            bool isDraw = false;

            if (player1Won == false & player2Won == false & anyEmpty == false)
                isDraw = true;

            return isDraw;
        }

        Vector2 calculateCenter()
        {
            float x = (pos1.x + pos2.x) / 2,
                y = (pos1.y + pos2.y) / 2;

            return new Vector2(x, y);
        }

        float calculateSlope()
            {
                float slope;

                if (pos1.x == pos2.x)
                    slope = 0.0f;
                else if (pos1.y == pos2.y)
                    slope = 90.0f;
                else if (pos1.x > 0.0f)
                    slope = -45.0f;
                else
                    slope = 45.0f;

                return slope;
            }

    }
}