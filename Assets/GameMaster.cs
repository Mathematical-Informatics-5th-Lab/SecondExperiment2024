using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public bool started = false;
    public float start_time = 0f;
    public float speed;
    public float bpm = 120;
    int notes_num = 3;
    int[] notes_time = { 24, 28, 32 };
    float[] notes_place = { 0f, 2f, -4f };
    int[] notes_type = { 0, 1, 2 };
    public float perfect = 0.05f;
    public float good = 0.10f;
    public float bad = 0.15f;
    public int perfect_count = 0;
    public int good_count = 0;
    public int bad_count = 0;

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
                note_comp.time = (float)notes_time[i] * 15f / bpm + start_time;
                note_comp.place = notes_place[i];
                note_comp.type = notes_type[i];
            }
        }
        if (started)
        {

        }
    }
}
