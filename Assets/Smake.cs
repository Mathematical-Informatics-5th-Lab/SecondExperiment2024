using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smake : MonoBehaviour
{

    //public AudioClip sound1;
    AudioSource audioSource;
    bool isClicked = false;
    public float mx = 0.06F;

    void Start () {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update () {
        // 左
        if (!isClicked && Input.GetMouseButton(0)){
            isClicked = true;
            audioSource.Play();
        }
        else if(isClicked && !Input.GetMouseButton(0)){
            isClicked = false;
            audioSource.Stop();
        }
        Vector3 mp=Input.mousePosition;
        mx = 0.06F + mp.x;
        //Debug.Log(plp);
    }
}