using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    bool started = false;
    float start_time = 0f;
    public float speed;
    int notes_num = 3;
    float[] notes_time = { 5.0f, 6.0f, 7.0f };
    float[] notes_place = { 0f, 2f, -4f };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!started && Input.GetKeyDown(KeyCode.Space))
        {
            started = true;
            start_time = Time.time;
            for (int i = 0; i < notes_num; i++)
            {
                GameObject obj = (GameObject)Resources.Load("Notes");
                GameObject note = Instantiate(obj, new Vector3(20.0f, 0.0f, 0.0f), Quaternion.identity);
                MoveNotes note_comp = note.GetComponent<MoveNotes>();
                note_comp.time = notes_time[i] + start_time;
                note_comp.place = notes_place[i];
            }
        }
        if (started)
        {

        }
    }
}
