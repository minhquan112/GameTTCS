using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ai : MonoBehaviour
{
    public static BanCo banCo = new BanCo();

    // player 1: Người chơi
    // player 2: Máy chơi

    public static bool checkEmpty(int[] board, int player)
    {
        if (player == 1)
        {
            for (int i = 6; i < 11; i++) // Xét các ô của người từ 6 đến 10
            {
                if (board[i] > 0)  // neu o con dan
                {
                    return false;
                }
            }
        }
        else
        {
            for (int i = 0; i < 5; i++) // Xét các ô của máy từ 0 đến 4
            {
                if (board[i] > 0)  // neu o con dan
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static void UpdateEmptyBoard(BanCo boardTemp)
    {

        if (checkEmpty(boardTemp.getBoard(), 2))
        {
            for (int k = 0; k < 5; k++) // Xét các ô của người từ 6 đến 10
            {
                boardTemp.setBoardByIdx(k, 1);
            }
        }
        if (checkEmpty(boardTemp.getBoard(), 1))
        {
            for (int k = 6; k < 11; k++) // Xét các ô của người từ 6 đến 10
            {
                boardTemp.setBoardByIdx(k, 1);
            }
        }
    }


    public static int[] EasyAi(int[] arr, int player)
    {
        BanCo Board = new BanCo(arr, 14);
        Board.goal_1 = arr[0];
        Board.goal_2 = arr[1];

        List<int> lsBox = new List<int>(); // list box còn dân
        if (player == 1)
        {
            for (int i = 6; i < 11; i++) // Xét các ô của người từ 6 đến 10
            {
                if (Board.getBoard()[i] > 0)  // neu o con dan
                {
                    lsBox.Add(i);
                }
            }
        }
        else
        {
            for (int i = 0; i < 5; i++) // Xét các ô của máy từ 0 đến 4
            {
                if (Board.getBoard()[i] > 0)  // neu o con dan
                {
                    lsBox.Add(i);
                }
            }
        }

        System.Random rd = new System.Random();

        return new int[] { lsBox[rd.Next(0, lsBox.Count)], rd.Next(0, 2) }; // random 1 ô và chiều bất kỳ
    }


    public static int[] MediumAi(int[] arr, int player)
    {
        BanCo Board = new BanCo(arr, 14);
        Board.goal_1 = arr[0];
        Board.goal_2 = arr[1];

        int[] board = (int[])Board.getBoard().Clone();
        List<int> lsBox = new List<int>(); // list box còn dân

        // Xét các ô còn dân
        if (player == 1)
        {
            for (int i = 6; i < 11; i++) // Xét các ô của người từ 6 đến 10
            {
                if (Board.getBoard()[i] > 0)  // neu o con dan
                {
                    lsBox.Add(i);
                }
            }
        }
        else if (player == 2)
        {
            for (int i = 0; i < 5; i++) // Xét các ô của máy từ 0 đến 4
            {
                if (Board.getBoard()[i] > 0)  // neu o con dan
                {
                    lsBox.Add(i);
                }
            }
        }

        // khởi tạo SortedDictionary với key là điểm số sau mỗi lượt và được sắp xếp giảm dần
        SortedDictionary<int, List<Choice>> map = new SortedDictionary<int, List<Choice>>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        foreach (int i in lsBox)  // Duyệt các ô còn dân
        {
            for (int j = 0; j < 2; j++)  // Duyệt 2 chiều quay
            {
                BanCo boardTemp = new BanCo(board);   // Khởi tạo bàn cờ tạm đế đi thử
                int reward = boardTemp.Move(i, j);  // Di chuyển quân ở ô i theo chiều j, điểm số sau lượt đi lưu vào biến reward
                if (!map.ContainsKey(reward))  // Nếu chưa có key reward trong map
                {
                    map[reward] = new List<Choice>(); // Khởi tạo list mới
                }
                map[reward].Add(new Choice(i, j)); // Thêm vào list
            }
        }



        List<Choice> firstList = map.FirstOrDefault().Value; // Lấy list dự đoán có điêm số cao nhất
        System.Random rd = new System.Random();
        Choice choice = firstList[rd.Next(0, firstList.Count)];

        return new int[] { choice.getIndex(), choice.getClockwise() }; // Random 1 trong các dự đoán
    }

    public static int[] BanCoToArr(BanCo banCo)
    {
        int[] arr = new int[14];
        arr[0] = banCo.goal_1;
        arr[1] = banCo.goal_2;
        for (int i = 2; i < 14; i++)
        {
            arr[i] = banCo.getBoard()[i - 2];
        }
        return arr;
    }

    public static int[] HardAi(int[] arr, int player)
    {
        BanCo Board = new BanCo(arr, 14);
        Board.goal_1 = arr[1];
        Board.goal_2 = arr[0];

        int[] board = (int[])Board.getBoard().Clone();
        List<int> lsBox = new List<int>(); // list box còn dân

        // Xét các ô còn dân
        if (player == 1)
        {
            for (int i = 6; i < 11; i++) // Xét các ô của người từ 6 đến 10
            {
                if (Board.getBoard()[i] > 0)  // neu o con dan
                {
                    lsBox.Add(i);
                }
            }
        }
        else if (player == 2)
        {
            for (int i = 0; i < 5; i++) // Xét các ô của máy từ 0 đến 4
            {
                if (Board.getBoard()[i] > 0)  // neu o con dan
                {
                    lsBox.Add(i);
                }
            }
        }

        // khởi tạo SortedDictionary với key là điểm số sau mỗi lượt và được sắp xếp giảm dần
        SortedDictionary<int, List<Choice>> map = new SortedDictionary<int, List<Choice>>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        foreach (int i in lsBox)  // Duyệt các ô còn dân
        {
            for (int j = 0; j < 2; j++)  // Duyệt 2 chiều quay
            {
                BanCo boardTemp = new BanCo(board);   // Khởi tạo bàn cờ tạm đế đi thử
                int reward = boardTemp.Move(i, j);  // Di chuyển quân ở ô i theo chiều j, điểm số sau lượt đi lưu vào biến reward
                if (!map.ContainsKey(reward))  // Nếu chưa có key reward trong map
                {
                    map[reward] = new List<Choice>(); // Khởi tạo list mới
                }
                map[reward].Add(new Choice(i, j)); // Thêm vào list
            }
        }



        List<Choice> firstList = map.FirstOrDefault().Value; // Lấy list dự đoán có điêm số cao nhất
        if (firstList.Count > 1)
        {
            SortedDictionary<int, List<Choice>> map2 = new SortedDictionary<int, List<Choice>>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
            foreach (Choice x in firstList)
            {
                Choice choice = new Choice(x.getIndex(), x.getClockwise());
                BanCo boardTemp = new BanCo(board); // Khởi tạo bàn cờ tạm
                int reward = boardTemp.Move(choice.getIndex(), choice.getIndex()); // Di chuyển quân ở ô i theo chiều j, điểm số sau lượt đi lưu vào biến reward
                UpdateEmptyBoard(boardTemp);

                int[] choiceArr = MediumAi(BanCoToArr(boardTemp), 1);
                choice = new Choice(choiceArr[0], choiceArr[1]);//  Dùng hàm MediumAi để dự đoán nước đi của người chơi
                reward -= boardTemp.Move(choice.getIndex(), choice.getClockwise()); // Điểm số sau khi người chơi đi, ta muốn tối thiểu hóa điểm số này nên coi giá trị là âm
                UpdateEmptyBoard(boardTemp);

                choiceArr = MediumAi(BanCoToArr(boardTemp), 2);
                choice = new Choice(choiceArr[0], choiceArr[1]); // Dự đoán nước đi của máy sau khi người chơi đi
                reward += boardTemp.Move(choice.getIndex(), choice.getClockwise());   // Điểm số sau khi máy đi
                UpdateEmptyBoard(boardTemp);

                if (!map2.ContainsKey(reward))
                {
                    map2[reward] = new List<Choice>();
                }
                map2[reward].Add(new Choice(x.getIndex(), x.getClockwise()));
            }

            List<Choice> firstList2 = map2.FirstOrDefault().Value; // Lấy list dự đoán có điêm số cao nhất
            System.Random rd = new System.Random();
            Choice result = firstList2[rd.Next(0, firstList2.Count)];
            return new int[] { result.getIndex(), result.getClockwise() }; // Random 1 trong các dự đoán
        }
        else
        {
            return new int[] { firstList[0].getIndex(), firstList[0].getClockwise() }; // Random 1 trong các dự đoán
        }

    }
}
