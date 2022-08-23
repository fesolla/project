using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_create : MonoBehaviour
{
    //版面數值宣告
    private int[,] box;
    //調用數值
    private int board;
    private int hint;
    //生成提示數值
    private int[,] hint_up;
    private int[,] hint_left;

    //anser_zone_生成用的
    public GameObject board_block;
    public GameObject hint_block;

    public int board_scale = 5;
    public int hint_scale = 3;

    //提示的圖片
    public Sprite[] image_change;
    public Sprite sprite_white;//重置用

    //檢查答案
    private int[,] box_to_check;
    private int[,] check_up;
    private int[,] check_left;

    void Start()
    {
        board = board_scale;
        hint = hint_scale;
        box = new int[board, board];//生成用
        box_to_check = new int[board, board];//檢查用
        //生成關卡
        //(1)限制個數
        int safe_zone = board * board * 2 / 5 + Random.Range(0, (board * board) /3);

        //(2)填入...1是安全區...2是禁止

            for (int i = 0; i < board; i++)
            {
                for (int j = 0; j < board; j++)
                {
                    int draw_a_num = Random.Range(0, board * board);
                    if(draw_a_num < safe_zone)
                    {
                        box[i,j] = 1;
                    }
                    else
                    {
                        box[i, j] = 0;
                    }
                    //Debug.Log("box[" + i + "," + j + "] = " + box[i, j]);
                }
            }

        //(3)生成提示
        //(3.0)生成矩陣
        hint_up = new int[board, hint];
        hint_left = new int[hint, board];
        //(3.1)紀錄數值
        int[] to_record = new int[board];
        int for_looping = 0;
        bool is_it_start = false;
        //紀錄其實反過來了，但是不修正，後面再反過來一次
        //(3.1.1)紀錄直排
        for (int i = 0; i < board; i++)
        {
            for (int j = board-1; j >= 0; j--)
            {
                if (box[i, j] == 1)
                {
                    is_it_start = true;
                    to_record[for_looping] += 1 ;
                }
                else if(box[i, j] == 0 && is_it_start)
                {
                    for_looping += 1;
                    is_it_start = false;
                }
            }
            //填寫直排提示
            for(int q = 0; q <= hint - 1; q++)
            {
                hint_up[i, q] = to_record[q];
                //Debug.Log("hint_up[" + i + "," + q + " = " + hint_up[i, q]);
            }
            //換下一行之前重置
            is_it_start = false;
            for_looping = 0;
            for(int p = 0; p < board; p++)
            {
                to_record[p] = 0;
            }
        }
        //(3.2)記錄橫排
        for (int i = 0; i < board; i++)
        {
            for (int j = board - 1; j >= 0; j--)
            {
                if (box[j, i] == 1)
                {
                    is_it_start = true;
                    to_record[for_looping] += 1;
                }
                else if (box[j, i] == 0 && is_it_start)
                {
                    for_looping += 1;
                    is_it_start = false;
                }
            }
            //填寫橫排提示
            for (int q = 0; q <= hint - 1; q++)
            {
                hint_left[q, i] = to_record[q];
                //Debug.Log("hint_left[" + q + "," + i + " = " + hint_left[q, i]);
            }
            //換下一行之前重置
            is_it_start = false;
            for_looping = 0;
            for (int p = 0; p < board; p++)
            {
                to_record[p] = 0;
            }
        }
        //(3.3)生成關卡 & 顯示提示
        //最大需求量[(n-1)/2的高斯符號]+1 --- 廢棄
        //用滾輪縮放，關卡直接生成好就好
        //每種難度分開寫好了，先弄出雛形
        //長
        for (int i = 1; i <= board_scale + hint_scale; i++)
        {
            //寬
            for (int j = 1; j <= board_scale + hint_scale; j++)
            {
                //左上空白
                if (i > hint_scale || j > hint_scale)
                {
                    //提示區
                    if (i <= hint_scale || j <= hint_scale)
                    {
                        //先換圖片再生成
                        //先分辨是上方還是左邊(分2組)
                        //再找出對應關係
                        //這邊再反過來就可以了
                        //(1)上面
                        if (i-1 >= hint_scale && j-1 < hint_scale && hint_up[i - 1 - hint_scale, hint_scale - j] != 0)
                        {
                            //hint_block.GetComponent<SpriteRenderer>().sprite = image_change[hint_up[i - 4, j - 1]];(原始)
                            hint_block.GetComponent<SpriteRenderer>().sprite = image_change[hint_up[i - 1 - hint_scale, hint_scale - j]];//(左右顛倒)
                            //Debug.Log("hint_up[" + i + "-1-hint_scale," +  j +"-1]=" + hint_up[i - 1 - hint_scale, hint_scale - j]);
                        }
                        //(2)左邊
                        else if (i-1 < hint_scale && j-1 >= hint_scale && hint_left[hint_scale - i, j - 1 - hint_scale] != 0)
                        {
                            //hint_block.GetComponent<SpriteRenderer>().sprite = image_change[hint_left[i - 1, j - 4]];(原始)
                            hint_block.GetComponent<SpriteRenderer>().sprite = image_change[hint_left[hint_scale - i, j - 1 - hint_scale]];
                            //Debug.Log("hint_left[" + i + "-1," + j + "-1-hint_scale]=" + hint_left[i - 1, j - 1 - hint_scale]);
                        }
                        //換圖片之後生成
                        Instantiate(hint_block, new Vector3(-(board_scale + hint_scale) / 2 + i - 1, (board_scale + hint_scale) / 2 - j + 1, 0), transform.rotation);
                        //Debug.Log("迴圈次數"+i+j);
                        //重置圖片
                        hint_block.GetComponent<SpriteRenderer>().sprite = sprite_white;
                    }
                    //作答區
                    else
                    {
                        //座標位置(x,y)對應到方格位置(i,j) = (x+(board_scale + hint_scale)/2,-y-(board_scale + hint_scale)/2)
                        //只考慮board修正變成(i',j') = (x+(board_scale - hint_scale)/2,-y+(board_scale - hint_scale)/2)
                        Instantiate(board_block, new Vector3(-(board_scale + hint_scale) / 2 + i - 1, (board_scale + hint_scale) / 2 - j + 1, 0), transform.rotation);
                    }
                }
            }
        }

    }

    //(4)換顏色
    private Color change_color = Color.white;
    private int click_pose_x;
    private int click_pose_y;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            //按下才發♂射
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, -1);
            if (hit.collider)
            {
                //Debug.Log(hit.collider.name);
                //board方塊
                if (hit.collider.tag == "board")
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = change_color;
                    click_pose_x = Mathf.FloorToInt(hit.collider.transform.position.x);
                    click_pose_y = Mathf.FloorToInt(hit.collider.transform.position.y);
                    //如果綠色就記錄座標並更改box_to_check變成1
                    if (change_color == Color.green)
                    {
                        box_to_check[click_pose_x + (board_scale - hint_scale) / 2, -click_pose_y + (board_scale - hint_scale) / 2] = 1;
                        Debug.Log("box_to_check[" + (click_pose_x + (board_scale - hint_scale) / 2) + "," + (-click_pose_y + (board_scale - hint_scale) / 2) + "] = 1");
                    }
                    //如果不是綠色就改回0
                    else
                    {
                        box_to_check[click_pose_x + (board_scale - hint_scale) / 2, -click_pose_y + (board_scale - hint_scale) / 2] = 0;
                        Debug.Log("box_to_check[" + (click_pose_x + (board_scale - hint_scale) / 2) + "," + (-click_pose_y + (board_scale - hint_scale) / 2) + "] = 0");
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            //按下才發♂射
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, -1);
            //執行功能
            //hint方塊
            if (hit.collider)
            {
                if (hit.collider.tag == "hint")
                {
                    if (hit.collider.gameObject.GetComponent<Renderer>().material.color == Color.white)
                    {
                        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.gray;
                    }
                    else if (hit.collider.gameObject.GetComponent<Renderer>().material.color == Color.gray)
                    {
                        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;
                    }
                }
            }
        }
            if (Input.GetMouseButton(1))
        {
            //按下才發♂射
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, -1);
            if (hit.collider)
            {
                //Debug.Log(hit.collider.name);
                //board方塊
                if (hit.collider.tag == "board")
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;
                    click_pose_x = Mathf.FloorToInt(hit.collider.transform.position.x);
                    click_pose_y = Mathf.FloorToInt(hit.collider.transform.position.y);
                    //因為一定是白色所以就記錄座標並更改box_to_check變成0
                    box_to_check[click_pose_x + (board_scale - hint_scale) / 2, -click_pose_y + (board_scale - hint_scale) / 2] = 0;
                    Debug.Log("box_to_check[" + (click_pose_x + (board_scale - hint_scale) / 2) + "," + (-click_pose_y + (board_scale - hint_scale) / 2) + "] = 0");
                }
            }
        }
    }

    public void change_to_Red()
    {
        change_color = Color.red;
        //Debug.Log("red");
    }
    public void change_to_green()
    {
        change_color = Color.green;
        //Debug.Log("green");
    }
    public void change_to_blue()
    {
        change_color = Color.blue;
        //Debug.Log("blue");
    }
    public void change_to_white()
    {
        change_color = Color.white;
        //Debug.Log("white");
    }
    //(4)換顏色↑
    //(5)檢查答案↓
    //首先生成時先標記座標
    private int is_the_same = 0;//0是檢查前，1是錯誤
    public void Check_ans()
    {
        //(5.0)檢查用矩陣,壓縮資訊
        check_up = new int[board, hint];
        check_left = new int[hint, board];
        //(5.1)紀錄數值
        int[] to_record = new int[board];
        int for_looping = 0;
        bool is_it_start = false;
        //(5.1.1)紀錄直排
        for (int i = 0; i < board; i++)
        {
            for (int j = board - 1; j >= 0; j--)
            {
                if (box_to_check[i, j] == 1)
                {
                    is_it_start = true;
                    to_record[for_looping] += 1;
                }
                else if (box_to_check[i, j] == 0 && is_it_start)
                {
                    for_looping += 1;
                    is_it_start = false;
                }
            }
            //填寫直排提示
            for (int q = 0; q <= hint - 1; q++)
            {
                check_up[i, q] = to_record[q];
                Debug.Log("check_up[" + i + "," + q + " = " + check_up[i, q]);
            }
            //換下一行之前重置
            is_it_start = false;
            for_looping = 0;
            for (int p = 0; p < board; p++)
            {
                to_record[p] = 0;
            }
        }
        //(5.2)記錄橫排
        for (int i = 0; i < board; i++)
        {
            for (int j = board - 1; j >= 0; j--)
            {
                if (box_to_check[j, i] == 1)
                {
                    is_it_start = true;
                    to_record[for_looping] += 1;
                }
                else if (box_to_check[j, i] == 0 && is_it_start)
                {
                    for_looping += 1;
                    is_it_start = false;
                }
            }
            //填寫橫排提示
            for (int q = 0; q <= hint - 1; q++)
            {
                check_left[q, i] = to_record[q];
                Debug.Log("check_left[" + q + "," + i + " = " + check_left[q, i]);
            }
            //換下一行之前重置
            is_it_start = false;
            for_looping = 0;
            for (int p = 0; p < board; p++)
            {
                to_record[p] = 0;
            }
        }
        //(5.3)比對
        //(5.3.1)比對上面
        for(int i = 0; i < board_scale; i++)
        {
            for(int j = 0; j < hint_scale; j++)
            {
                //如果相同就繼續迴圈
                if(hint_up[i,j] == check_up[i, j])
                {

                }
                else
                {
                    //有一個不同就停止迴圈並且記錄狀態
                    is_the_same = 1;
                    i = board_scale;
                    j = hint_scale;
                }
                //不同就停止迴圈
            }
        }
        //(5.3.2)比對左邊
        for (int i = 0; i < board_scale; i++)
        {
            for (int j = 0; j < hint_scale; j++)
            {
                //如果相同就繼續迴圈
                if (hint_left[j, i] == check_left[j, i])
                {

                }
                else
                {
                    //有一個不同就停止迴圈並且記錄狀態
                    is_the_same = 1;
                    i = board_scale;
                    j = hint_scale;
                }
                //不同就停止迴圈
            }
        }
        //(5.3.3)比對完成後輸出結果
        if(is_the_same == 0)
        {
            Debug.Log("succese");
        }
        else
        {
            Debug.Log("false");
        }
        //(5.3.4)數據初始化
        is_the_same = 0;
        for (int i = 0; i < board_scale; i++)
        {
            for (int j = 0; j < hint_scale; j++)
            {
                hint_up[i, j] = 0;
                hint_left[j, i] = 0;
            }
        }
    }

}
