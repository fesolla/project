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

    //anser_zone_�ͦ��Ϊ�
    public GameObject board_block;
    public GameObject hint_block;

    public int board_scale = 5;
    public int hint_scale = 3;

    //���ܪ��Ϥ�
    public Sprite[] image_change;
    public Sprite sprite_white;//���m��

    //�ˬd����
    private int[,] box_to_check;
    private int[,] check_up;
    private int[,] check_left;

    void Start()
    {
        board = board_scale;
        hint = hint_scale;
        box = new int[board, board];//�ͦ���
        box_to_check = new int[board, board];//�ˬd��
        //�ͦ����d
        //(1)����Ӽ�
        int safe_zone = board * board * 2 / 5 + Random.Range(0, (board * board) /3);

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
                    //Debug.Log("box[" + i + "," + j + "] = " + box[i, j]);
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
        //�������ϹL�ӤF�A���O���ץ��A�᭱�A�ϹL�Ӥ@��
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
                    is_it_start = false;
                }
            }
            //��g���ƴ���
            for(int q = 0; q <= hint - 1; q++)
            {
                hint_up[i, q] = to_record[q];
                //Debug.Log("hint_up[" + i + "," + q + " = " + hint_up[i, q]);
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
                    is_it_start = false;
                }
            }
            //��g��ƴ���
            for (int q = 0; q <= hint - 1; q++)
            {
                hint_left[q, i] = to_record[q];
                //Debug.Log("hint_left[" + q + "," + i + " = " + hint_left[q, i]);
            }
            //���U�@�椧�e���m
            is_it_start = false;
            for_looping = 0;
            for (int p = 0; p < board; p++)
            {
                to_record[p] = 0;
            }
        }
        //(3.3)�ͦ����d & ��ܴ���
        //�̤j�ݨD�q[(n-1)/2�������Ÿ�]+1 --- �o��
        //�κu���Y��A���d�����ͦ��n�N�n
        //�C�����פ��}�g�n�F�A���˥X����
        //��
        for (int i = 1; i <= board_scale + hint_scale; i++)
        {
            //�e
            for (int j = 1; j <= board_scale + hint_scale; j++)
            {
                //���W�ť�
                if (i > hint_scale || j > hint_scale)
                {
                    //���ܰ�
                    if (i <= hint_scale || j <= hint_scale)
                    {
                        //�����Ϥ��A�ͦ�
                        //������O�W���٬O����(��2��)
                        //�A��X�������Y
                        //�o��A�ϹL�ӴN�i�H�F
                        //(1)�W��
                        if (i-1 >= hint_scale && j-1 < hint_scale && hint_up[i - 1 - hint_scale, hint_scale - j] != 0)
                        {
                            //hint_block.GetComponent<SpriteRenderer>().sprite = image_change[hint_up[i - 4, j - 1]];(��l)
                            hint_block.GetComponent<SpriteRenderer>().sprite = image_change[hint_up[i - 1 - hint_scale, hint_scale - j]];//(���k�A��)
                            //Debug.Log("hint_up[" + i + "-1-hint_scale," +  j +"-1]=" + hint_up[i - 1 - hint_scale, hint_scale - j]);
                        }
                        //(2)����
                        else if (i-1 < hint_scale && j-1 >= hint_scale && hint_left[hint_scale - i, j - 1 - hint_scale] != 0)
                        {
                            //hint_block.GetComponent<SpriteRenderer>().sprite = image_change[hint_left[i - 1, j - 4]];(��l)
                            hint_block.GetComponent<SpriteRenderer>().sprite = image_change[hint_left[hint_scale - i, j - 1 - hint_scale]];
                            //Debug.Log("hint_left[" + i + "-1," + j + "-1-hint_scale]=" + hint_left[i - 1, j - 1 - hint_scale]);
                        }
                        //���Ϥ�����ͦ�
                        Instantiate(hint_block, new Vector3(-(board_scale + hint_scale) / 2 + i - 1, (board_scale + hint_scale) / 2 - j + 1, 0), transform.rotation);
                        //Debug.Log("�j�馸��"+i+j);
                        //���m�Ϥ�
                        hint_block.GetComponent<SpriteRenderer>().sprite = sprite_white;
                    }
                    //�@����
                    else
                    {
                        //�y�Ц�m(x,y)���������m(i,j) = (x+(board_scale + hint_scale)/2,-y-(board_scale + hint_scale)/2)
                        //�u�Ҽ{board�ץ��ܦ�(i',j') = (x+(board_scale - hint_scale)/2,-y+(board_scale - hint_scale)/2)
                        Instantiate(board_block, new Vector3(-(board_scale + hint_scale) / 2 + i - 1, (board_scale + hint_scale) / 2 - j + 1, 0), transform.rotation);
                    }
                }
            }
        }

    }

    //(4)���C��
    private Color change_color = Color.white;
    private int click_pose_x;
    private int click_pose_y;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            //���U�~�o��g
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, -1);
            if (hit.collider)
            {
                //Debug.Log(hit.collider.name);
                //board���
                if (hit.collider.tag == "board")
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = change_color;
                    click_pose_x = Mathf.FloorToInt(hit.collider.transform.position.x);
                    click_pose_y = Mathf.FloorToInt(hit.collider.transform.position.y);
                    //�p�G���N�O���y�Шç��box_to_check�ܦ�1
                    if (change_color == Color.green)
                    {
                        box_to_check[click_pose_x + (board_scale - hint_scale) / 2, -click_pose_y + (board_scale - hint_scale) / 2] = 1;
                        Debug.Log("box_to_check[" + (click_pose_x + (board_scale - hint_scale) / 2) + "," + (-click_pose_y + (board_scale - hint_scale) / 2) + "] = 1");
                    }
                    //�p�G���O���N��^0
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
            //���U�~�o��g
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, -1);
            //����\��
            //hint���
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
            //���U�~�o��g
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, -1);
            if (hit.collider)
            {
                //Debug.Log(hit.collider.name);
                //board���
                if (hit.collider.tag == "board")
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;
                    click_pose_x = Mathf.FloorToInt(hit.collider.transform.position.x);
                    click_pose_y = Mathf.FloorToInt(hit.collider.transform.position.y);
                    //�]���@�w�O�զ�ҥH�N�O���y�Шç��box_to_check�ܦ�0
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
    //(4)���C���
    //(5)�ˬd���ס�
    //�����ͦ��ɥ��аO�y��
    private int is_the_same = 0;//0�O�ˬd�e�A1�O���~
    public void Check_ans()
    {
        //(5.0)�ˬd�ίx�},���Y��T
        check_up = new int[board, hint];
        check_left = new int[hint, board];
        //(5.1)�����ƭ�
        int[] to_record = new int[board];
        int for_looping = 0;
        bool is_it_start = false;
        //(5.1.1)��������
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
            //��g���ƴ���
            for (int q = 0; q <= hint - 1; q++)
            {
                check_up[i, q] = to_record[q];
                Debug.Log("check_up[" + i + "," + q + " = " + check_up[i, q]);
            }
            //���U�@�椧�e���m
            is_it_start = false;
            for_looping = 0;
            for (int p = 0; p < board; p++)
            {
                to_record[p] = 0;
            }
        }
        //(5.2)�O�����
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
            //��g��ƴ���
            for (int q = 0; q <= hint - 1; q++)
            {
                check_left[q, i] = to_record[q];
                Debug.Log("check_left[" + q + "," + i + " = " + check_left[q, i]);
            }
            //���U�@�椧�e���m
            is_it_start = false;
            for_looping = 0;
            for (int p = 0; p < board; p++)
            {
                to_record[p] = 0;
            }
        }
        //(5.3)���
        //(5.3.1)���W��
        for(int i = 0; i < board_scale; i++)
        {
            for(int j = 0; j < hint_scale; j++)
            {
                //�p�G�ۦP�N�~��j��
                if(hint_up[i,j] == check_up[i, j])
                {

                }
                else
                {
                    //���@�Ӥ��P�N����j��åB�O�����A
                    is_the_same = 1;
                    i = board_scale;
                    j = hint_scale;
                }
                //���P�N����j��
            }
        }
        //(5.3.2)��索��
        for (int i = 0; i < board_scale; i++)
        {
            for (int j = 0; j < hint_scale; j++)
            {
                //�p�G�ۦP�N�~��j��
                if (hint_left[j, i] == check_left[j, i])
                {

                }
                else
                {
                    //���@�Ӥ��P�N����j��åB�O�����A
                    is_the_same = 1;
                    i = board_scale;
                    j = hint_scale;
                }
                //���P�N����j��
            }
        }
        //(5.3.3)��粒�����X���G
        if(is_the_same == 0)
        {
            Debug.Log("succese");
        }
        else
        {
            Debug.Log("false");
        }
        //(5.3.4)�ƾڪ�l��
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
