using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_create : MonoBehaviour
{
    //�����ƭȫŧi
    private int[,] box;
    //�եμƭ�
    private int board;
    private int hint;
    //�ͦ����ܼƭ�
    private int[,] hint_up;
    private int[,] hint_left;

    void Start()
    {
        board = GameObject.Find("board_empty").GetComponent<anser_zone_spqwn>().board_scale;
        hint = GameObject.Find("board_empty").GetComponent<anser_zone_spqwn>().hint_scale;
        box = new int[board, board];
        //�ͦ����d
        //(1)����Ӽ�
        int safe_zone = board * board / 3 + Random.Range(0, (board * board) /3);

        //(2)��J...1�O�w����...2�O�T��

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
                    Debug.Log(box[i, j]);
                }
            }

        //(3)�ͦ�����
        //(3.0)�ͦ��x�}
        hint_up = new int[board, hint];
        hint_left = new int[hint, board];
        //(3.1)�����ƭ�
        int[] to_record = new int[board];
        int for_looping = 0;
        bool is_it_start = false;
        //(3.1.1)��������
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
                }
            }
            //��g���ƴ���
            for(int q = hint - 1; q >= 0; q--)
            {
                hint_up[i, q] = to_record[q];
            }
            //���U�@�椧�e���m
            is_it_start = false;
            for_looping = 0;
            for(int p = 0; p < board; p++)
            {
                to_record[p] = 0;
            }
        }
        //(3.2)�O�����
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
                }
            }
            //��g��ƴ���
            for (int q = hint - 1; q >= 0; q--)
            {
                hint_left[q, i] = to_record[q];
            }
            //���U�@�椧�e���m
            is_it_start = false;
            for_looping = 0;
            for (int p = 0; p < board; p++)
            {
                to_record[p] = 0;
            }
        }
        //(3.3)��ܴ���

    }

    //�ˬd����
    void Check_ans()
    {

    }

}
