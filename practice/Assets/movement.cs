using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update	
private Rigidbody2D m_Rigidbody2D;
    void Start(){
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
		if (Input.GetKey(KeyCode.RightArrow)){
			m_Rigidbody2D.velocity = new Vector2(3,0);
		}
		if (Input.GetKeyUp(KeyCode.RightArrow)){
			m_Rigidbody2D.velocity =Vector2.zero;
		}
		if (Input.GetKey(KeyCode.LeftArrow)){
			m_Rigidbody2D.velocity =new Vector2(-3,0);
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow)){
			m_Rigidbody2D.velocity =Vector2.zero;
		}
    }
}
