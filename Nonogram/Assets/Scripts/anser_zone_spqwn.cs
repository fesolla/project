using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class anser_zone_spqwn : MonoBehaviour
{
    public GameObject board_block;
    public GameObject hint_block;

    public int board_scale = 5;
    public int hint_scale = 3;

    void Start()
    {
        //�̤j�ݨD�q[(n-1)/2�������Ÿ�]+1 --- �o��
        //�κu���Y��A���d�����ͦ��n�N�n
        //�C�����פ��}�g�n�F�A���˥X����
        //��
        for (int i =1; i <= board_scale + hint_scale; i++)
        {
            //�e
            for(int j = 1; j <= board_scale + hint_scale; j++)
            {
                //���W�ť�
                if(i > hint_scale || j > hint_scale)
                {
                    //���ܰ�
                    if(i <= hint_scale || j <= hint_scale)
                    {
                        Instantiate(hint_block, new Vector3(-(board_scale + hint_scale) / 2 + i -1, (board_scale + hint_scale) / 2 - j +1, 0), transform.rotation);
                    }
                    //�@����
                    else
                    {
                        Instantiate(board_block, new Vector3(-(board_scale + hint_scale) / 2 + i -1, (board_scale + hint_scale) / 2 - j +1, 0), transform.rotation);
                    }
                }
            }
        }

    }

}
