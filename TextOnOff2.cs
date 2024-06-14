using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextOnOff2 : MonoBehaviour
{
    GameMaster gamemaster;
    [SerializeField]
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("GameMaster");
        gamemaster = obj.GetComponent<GameMaster>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gamemaster.started)
        {
            if (Time.time - gamemaster.judge_time < 1.0f && gamemaster.judge == 1)
            {
                text.enabled = true;
                if (Time.time - gamemaster.judge_time < 0.2f)
                {
                    text.fontSize = 120 * ((Time.time - gamemaster.judge_time) / 0.2f);
                }
                else
                {
                    text.fontSize = 120;
                }
            }
            else text.enabled = false;
        }
    }
}
