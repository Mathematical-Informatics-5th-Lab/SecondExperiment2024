using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 Pos = myTransform.position;
        //if (Input.GetKeyDown(KeyCode.DownArrow)){Pos.y -= 1;myTransform.position = Pos;}
        //if (Input.GetKeyDown(KeyCode.UpArrow)){Pos.y += 1;myTransform.position = Pos;}
        
    }
}
