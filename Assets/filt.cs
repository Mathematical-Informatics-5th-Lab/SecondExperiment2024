using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The code example shows how to implement a metronome that procedurally
// generates the click sounds via the OnAudioFilterRead callback.
// While the game is paused or suspended, this time will not be updated and sounds
// playing will be paused. Therefore developers of music scheduling routines do not have
// to do any rescheduling after the app is unpaused

[RequireComponent(typeof(AudioSource))]
public class filt : MonoBehaviour
{
    public double bpm = 140.0F;
    public float gain = 0.5F;
    public int signatureHi = 4;
    public int signatureLo = 4;
    public float px = 0;
    public float py = 0;

    private double nextTick = 0.0F;
    private float amp = 1.0F;
    private float phase = 0.0F;
    private double sampleRate = 0.0F;
    private int accent;
    private bool running = false;

    void Start()
    {
        accent = signatureHi;
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick * sampleRate;
        running = true;
    }

    void Update(){
        Vector3 mp = Input.mousePosition;
        px = mp.x*70;
        py = mp.y*200;
        //Debug.Log(plp);
    }

    float triwave(float G,float A,float p){
        float x=(p)/(2.0F*Mathf.PI);
        p -= (int)(x)*(2.0F*Mathf.PI);

        if(0 < Mathf.Sin(p))return -G * A * (-1.0F + p / Mathf.PI * 2.0F);
        else return G * A * (3.0F - p / Mathf.PI * 2.0F);
    }

    float sinwave(float G,float A,float p){
        float x=G * A * Mathf.Sin(p);
        return x;
    }

    float rectwave(float G,float A,float p){
        if(0 < Mathf.Sin(p))return G * A;
        else return -G * A;
    }

    float sawwave(float G,float A,float p){
        float x=(p)/(2.0F*Mathf.PI);
        p -= (int)(x)*(2.0F*Mathf.PI);
        return -G * A * (-1.0F + p / Mathf.PI);
    }

    float mixture(float G,float A,float p){
        return (px*rectwave(G,A,p) + py*sinwave(G,A,p))/(px+py);
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;
        int sampleCount = data.Length / channels;
        for (int i = 0; i < sampleCount; i++) // 必要なサンプル数だけ
        {
            float v = mixture(gain,amp,Mathf.PI * 2.0F * (float)(phase + i) / 100.0F);
            
            for (int c = 0; c < channels; c++) // チャネル分コピー
            {
                data[(i * channels) + c] = v;
            }
        }
        phase += sampleCount;
    }
}