using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

public AudioClip sound1;
AudioSource audioSource;

 void Start () {
   //Component‚ğ
   audioSource = GetComponent<AudioSource>();
 }

 void Update () {
   // ¶
  if (Input.GetKey (KeyCode.LeftArrow)) {
   //‰¹(sound1)‚ğ–Â‚ç‚·
    audioSource.PlayOneShot(sound1);
   }
 }
}