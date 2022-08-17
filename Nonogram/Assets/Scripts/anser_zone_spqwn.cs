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
        //最大需求量[(n-1)/2的高斯符號]+1 --- 廢棄
        //用滾輪縮放，關卡直接生成好就好
        //每種難度分開寫好了，先弄出雛形
        //長
        for (int i =1; i <= board_scale + hint_scale; i++)
        {
            //寬
            for(int j = 1; j <= board_scale + hint_scale; j++)
            {
                //左上空白
                if(i > hint_scale || j > hint_scale)
                {
                    //提示區
                    if(i <= hint_scale || j <= hint_scale)
                    {
                        Instantiate(hint_block, new Vector3(-(board_scale + hint_scale) / 2 + i -1, (board_scale + hint_scale) / 2 - j +1, 0), transform.rotation);
                    }
                    //作答區
                    else
                    {
                        Instantiate(board_block, new Vector3(-(board_scale + hint_scale) / 2 + i -1, (board_scale + hint_scale) / 2 - j +1, 0), transform.rotation);
                    }
                }
            }
        }

    }

}
