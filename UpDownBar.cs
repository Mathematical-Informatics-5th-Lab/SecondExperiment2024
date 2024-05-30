using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownBar : MonoBehaviour
{
    Example example;

    GameObject obj;

    public float barY;

    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("Capsule Hands");
        example = obj.GetComponent<Example>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 Pos = myTransform.position;
        Pos.y = example.pos_y * 15 - 4;
        myTransform.position = Pos;
        barY = Pos.y;

    }
}
