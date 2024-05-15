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
    private float amp = 0;
    private float phase = 0.0F;
    private double sampleRate = 0.0F;
    private int accent;
    private bool running = false;

    private float ampa = 0;
    private float amps = 0;
    private float ampd = 0;
    private float ampf = 0;

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
        if(Input.GetKey(KeyCode.A))ampa=1.0F;
        else ampa*=0.9F;
        if(Input.GetKey(KeyCode.S))amps=1.0F;
        else amps*=0.9F;
        if(Input.GetKey(KeyCode.D))ampd=1.0F;
        else ampd*=0.9F;
        if(Input.GetKey(KeyCode.F))ampf=1.0F;
        else ampf*=0.9F;
    }

    float mod2pi(float p){
        float x=(p)/(2.0F*Mathf.PI);
        p -= (int)(x)*(2.0F*Mathf.PI);
        return p;
    }/*

    float triwave(float G,float A,float p,float f){
        p=mod2pi(p);

        if(0 < Mathf.Sin(p*f))return -G * A * (-1.0F + mod2pi(p*f) / Mathf.PI * 2.0F);
        else return G * A * (3.0F - mod2pi(p*f) / Mathf.PI * 2.0F);
    }*/

    float sinwave(float G,float A,float p,float f){
        float x=G * A * Mathf.Sin(p*f);
        return x;
    }

    float rectwave(float G,float A,float p,float f){
        if(0 < Mathf.Sin(p*f))return G * A;
        else return -G * A;
    }/*

    float sawwave(float G,float A,float p,float f){
        float x=(p)/(2.0F*Mathf.PI);
        p -= (int)(x)*(2.0F*Mathf.PI);
        return -G * A * (-1.0F + p / Mathf.PI);
    }*/
    /*

    float neiro1(float G,float A,float p,float f){
        float a=sinwave(G,A,p,f*1.0F);
        float b=rectwave(G,A,p,f*1.0F);

        float c=sinwave(G,A,p+0.1F,f*2.0F);

        float d=sinwave(G,A,p+0.2F,*4.0F);

        float e=sinwave(G,A,p+0.2F,f*16.0F);
        return (a*4+b*4+c*4+d+e)/13.0F;
    }

    float neiro2(float G,float A,float p,float f){
        float a=sinwave(G,A,p,f*1.0F);
        float b=sinwave(G,A,p+0.1F,f*+2.0F);
        float c=sinwave(G,A,p+0.2F,f*4.0F);
        float e=sinwave(G,A,p+0.1F,f*16.0F);
        return (a*8+b*4+c*2+e)/16.0F;
    }

    float neiro3(float G,float A,float p,float f){
        float a=sinwave(G,A,p,f*1.0F);
        float b=rectwave(G,A,p+0.1F,f*2.0F);
        float c=sinwave(G,A,p+0.2F,f*4.0F);
        float d=rectwave(G,A,p+0.1F,f*8.0F);
        float e=sinwave(G,A,p+0.1F,f*16.0F);
        return (a*20+b*6+c*3+d*3+e)/29.0F;
    }

    float neiro4(float G,float A,float p,float f){
        float a=rectwave(G,A,p,f*1.0F);
        float b=rectwave(G,A,p+0.1F,f*2.0F);
        float c=sinwave(G,A,p+0.2F,f*4.0F);
        float d=rectwave(G,A,p+0.1F,f*8.0F);
        float e=sinwave(G,A,p+0.1F,f*16.0F);
        return (a*6+b*3+c*3+d*3+e)/16.0F;
    }*/

    float neiro0(float G,float A,float p,float f,float[] var){
        float s1=sinwave(G,A,p,f*1.0F);
        float s2=sinwave(G,A,p+0.1F,f*+2.0F);
        float s4=sinwave(G,A,p+0.2F,f*4.0F);
        float s8=sinwave(G,A,p,f*8.0F);
        float sF=sinwave(G,A,p+0.1F,f*16.0F);

        float r1=rectwave(G,A,p,f*1.0F);
        float r2=rectwave(G,A,p+0.1F,f*2.0F);
        float r4=rectwave(G,A,p+0.2F,f*2.0F);
        float r8=rectwave(G,A,p+0.1F,f*8.0F);
        float rF=rectwave(G,A,p,f*16.0F);

        float[] sr = {s1,s2,s4,s8,sF,r1,r2,r4,r8,rF};

        float v=0,vs=0;
        for(int i=0;i<10;i++){
            v+=sr[i]*var[i];
            vs+=var[i];
        }
        return v/vs;

        // 4 4 1 0 0 / 4 0 0 0 0
        // 8 4 2 1 1 / 0 0 0 0 0
        // 20 0 3 1 1 / 0 6 0 3 0
        // 0 0 3 1 1 / 6 3 0 3 0
    }
    
    float neiro1(float G,float A,float p,float f){
        float[] FF = {4,4,1,0,0,4,0,0,0,0};
        return neiro0(G,A,p,f,FF);
    }
    
    float neiro2(float G,float A,float p,float f){
        float[] FF = {8,4,2,1,1,0,0,0,0,0};
        return neiro0(G,A,p,f,FF);
    }
    
    float neiro3(float G,float A,float p,float f){
        float[] FF = {20,3,3,1,1,0,6,3,3,0};
        return neiro0(G,A,p,f,FF);
    }
    
    float neiro4(float G,float A,float p,float f){
        float[] FF = {0,0,3,1,1,6,3,3,3,0};
        return neiro0(G,A,p,f,FF);
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        int sampleCount = data.Length / channels;
        for (int i = 0; i < sampleCount; i++) // 必要なサンプル数だけ
        {
            float f=130.8F/24343.3F;
            //float v = mix1(gain,amp,Mathf.PI * 2.0F * (float)(phase + i),f);
            float v=neiro1(gain,ampa,Mathf.PI * 2.0F * (float)(phase + i),f)
            +neiro2(gain,amps,Mathf.PI * 2.0F * (float)(phase + i),f)
            +neiro3(gain,ampd,Mathf.PI * 2.0F * (float)(phase + i),f)
            +neiro4(gain,ampf,Mathf.PI * 2.0F * (float)(phase + i),f);
            
            for (int c = 0; c < channels; c++) // チャネル分コピー
            {
                data[(i * channels) + c] = v;
            }
        }
        //Debug.Log(sampleCount); 50*1024/100 20480
        phase += sampleCount;
    }
}