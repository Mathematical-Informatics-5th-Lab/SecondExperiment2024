using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBar : MonoBehaviour
{
    int transparency = 256;
    GameMaster gamemaster;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("GameMaster");
        gamemaster = obj.GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamemaster.started)
        {
            transparency = (int)((Time.time - gamemaster.start_time) * gamemaster.bpm * 256f / 60f) % 256;
            //Debug.Log((Time.time - gamemaster.start_time) * gamemaster.bpm);
            //Debug.Log((int)((Time.time - gamemaster.start_time) * gamemaster.bpm / 60f));
            GetComponent<Renderer>().material.color = new Color32(201, 118, 129, (byte)transparency);
        }
    }
}