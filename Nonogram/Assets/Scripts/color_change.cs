using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color_change : MonoBehaviour
{
    private Color change_color = Color.white;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            //按下才發♂射
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, -1);
            if (hit.collider)
            {
                Debug.Log(hit.collider.name);
                //執行功能
                //hint方塊
                if (hit.collider.tag == "hint")
                {
                    if(hit.collider.gameObject.GetComponent<Renderer>().material.color == Color.white)
                    {
                        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.gray;
                    }
                    else if (hit.collider.gameObject.GetComponent<Renderer>().material.color == Color.gray)
                    {
                        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;
                    }
                }
                //board方塊
                else if(hit.collider.tag == "board")
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = change_color;
                }
            }
        }
    }

    public void change_to_Red()
    {
        change_color = Color.red;
        Debug.Log("red");
    }
    public void change_to_green()
    {
        change_color = Color.green;
        Debug.Log("green");
    }
    public void change_to_blue()
    {
        change_color = Color.blue;
        Debug.Log("blue");
    }
    public void change_to_white()
    {
        change_color = Color.white;
        Debug.Log("white");
    }

}
