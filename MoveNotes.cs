using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNotes : MonoBehaviour
{
    public float time = 0;
    public int place = 0;
    GameMaster gamemaster;
    UpDownBar updownbar;
    Example example;
    float speed;
    bool exist = true;
    bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("GameMaster");
        gamemaster = obj.GetComponent<GameMaster>();
        GameObject udb = GameObject.Find("UpDownBar");
        updownbar = udb.GetComponent<UpDownBar>();
        GameObject ch = GameObject.Find("Capsule Hands");
        example = ch.GetComponent<Example>();
        speed = gamemaster.speed;
        Transform myTransform = this.transform;
        Vector3 Pos = myTransform.position;
        Pos.y = place * 1.6f - 4.0f;
        myTransform.position = Pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (exist)
        {
            Transform myTransform = this.transform;
            Vector3 Pos = myTransform.position;
            Pos.x = (time - Time.time) * speed - 10;
            myTransform.position = Pos;
            if (updownbar.barY >= 0) flag = false;
            if (!flag && updownbar.barY < 0) {
                flag = true;
                if (example.extendedCount == place)
                {
                    if (Mathf.Abs(time - Time.time) < gamemaster.bad)
                    {
                        exist = false;
                        GetComponent<Renderer>().material.color = new Color32(167, 238, 255, 0);
                        if (Mathf.Abs(time - Time.time) < gamemaster.perfect)
                        {
                            gamemaster.perfect_count++;
                            gamemaster.judge = 0;
                            gamemaster.judge_time = Time.time;
                        }
                        else if (Mathf.Abs(time - Time.time) < gamemaster.good)
                        {
                            gamemaster.good_count++;
                            gamemaster.judge = 1;
                            gamemaster.judge_time = Time.time;
                        }
                        else
                        {
                            gamemaster.bad_count++;
                            gamemaster.judge = 2;
                            gamemaster.judge_time = Time.time;
                        }
                    }
                }
            }
        }
    }
}
