using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public bool started = false;
    public float start_time = 0f;
    public float speed = 5f;
    public float bpm = 106;
    int notes_num = 29;
    int[] notes_time = { 32, 36, 40, 44, 48, 52, 56, 64, 68, 72, 76, 80, 84, 88, 96, 104, 112, 120, 128, 130, 132, 134, 136, 138, 140, 142, 144, 148, 152 };
    int[] notes_place = { 0, 1, 2, 3, 2, 1, 0, 2, 3, 4, 5, 4, 3, 2, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 2, 1, 0 };
    public float offset = 0.5f;
    public float perfect = 0.05f;
    public float good = 0.10f;
    public float bad = 0.15f;
    public int perfect_count = 0;
    public int good_count = 0;
    public int bad_count = 0;

    public AudioClip sound1;
    AudioSource audioSource;

    public int judge = -1;
    public float judge_time = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!started && Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(sound1);
            started = true;
            start_time = Time.time + offset;
            for (int i = 0; i < notes_num; i++)
            {
                GameObject obj = (GameObject)Resources.Load("Notes");
                GameObject note = Instantiate(obj, new Vector3(20.0f, 0.0f, 0.0f), Quaternion.identity);
                MoveNotes note_comp = note.GetComponent<MoveNotes>();
                note_comp.time = (float)notes_time[i] * 15f / bpm + start_time;
                note_comp.place = notes_place[i];
            }
        }
        if (started)
        {

        }
    }
}
