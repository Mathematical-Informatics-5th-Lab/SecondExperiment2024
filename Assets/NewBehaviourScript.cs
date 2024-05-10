using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string key = "Q";
    }

    // Update is called once per frame
    void Update()
    {
        string key = Console.ReadLine();
        if (key != "Q") {
            System.Media.SoundPlayer("do.mp3").Play();
        } 
    }
}
