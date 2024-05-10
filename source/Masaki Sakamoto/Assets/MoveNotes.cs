using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNotes : MonoBehaviour
{
    public float time = 0f;
    public float place = 0f;
    GameMaster gamemaster;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("GameMaster");
        gamemaster = obj.GetComponent<GameMaster>();
        speed = gamemaster.speed;
        Transform myTransform = this.transform;
        Vector3 Pos = myTransform.position;
        Pos.y = place;
        myTransform.position = Pos;
    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 Pos = myTransform.position;
        Pos.x = (time - Time.time) * speed - 10;
        myTransform.position = Pos;
    }
}
