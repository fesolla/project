using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     void Update () {
		
        if ( Input.GetKey("right")) {  transform.Translate( 0.1f, 0,0 );  }
        // 按住 上鍵 時，物件每個 frame 朝自身 z 軸方向移動 0.1 公尺

        if ( Input.GetKey("left")) {  transform.Translate( -0.1f , 0,0 );  }
        // 按住 下鍵 時，物件每個 frame 朝自身 z 軸方向移動 -0.1 公尺
    }
}
