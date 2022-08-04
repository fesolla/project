using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class position : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Transform>().localPosition=new Vector3(0.18f,0.013f,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
