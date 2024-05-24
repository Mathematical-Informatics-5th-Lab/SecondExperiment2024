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
    private float xv = 0;
    private float yv = 0;

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
        if(Input.GetKeyDown(KeyCode.Z))ampa=1.0F;
        else ampa*=0.9F;
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
    }

    float sigmoid(float x,float a){
        double p=System.Math.PI;
        return (float)(System.Math.Tanh((double)(a*x))/p+0.5);
    }
    float[] disc_gauss(float sg,float sum){
        float[] v={0,0,0,0,0};
        float sm=0;
        if(sum<0.01F)return v;
        for(int i=0;i<5;i++){
            v[i]=Mathf.Exp(-sg*i*i);
            sm+=v[i];
        }
        for(int i=0;i<5;i++)v[i]*=sum/sm;
        return v;
    }

    float[] weight(float x,float y){
        //zは鳴らすかどうかの判定
        //yで重み

        //-0.3で(1,0),で(0,1)
        float w_sin = sigmoid(x,30.0F);
        float w_rect = 1.0F - w_sin;

        float[] ret={0,0,0,0,0,0,0,0,0,0};
        if(y>0){
            //sinの周波数帯を高くする
            float[] v_sin=disc_gauss(0.04F/(0.04F+y),w_sin);
            float[] v_rect=disc_gauss(1.0F,w_rect);
            for(int i=0;i<5;i++)ret[i]=v_sin[i];
            for(int i=0;i<5;i++)ret[5+i]=v_rect[i];
        }
        else{
            //rectの音量を低くする
            float[] v_sin=disc_gauss(1.0F/(1.0F-y),w_sin);
            float[] v_rect=disc_gauss(0.04F/(0.04F-y),w_rect*(1.0F+y/10.0F));
            for(int i=0;i<5;i++)ret[i]=v_sin[i];
            for(int i=0;i<5;i++)ret[5+i]=v_rect[i];
        }
        return ret;
    }

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
            if(i>4)vs+=3.0F*var[i];
            else vs+=var[i];
        }
        return v/vs;

        // 4 4 1 0 0 / 4 0 0 0 0
        // 8 4 2 1 1 / 0 0 0 0 0
        // 20 0 3 1 1 / 0 6 0 3 0
        // 0 0 3 1 1 / 6 3 0 3 0
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        int sampleCount = data.Length / channels;
        for (int i = 0; i < sampleCount; i++) // 必要なサンプル数だけ
        {
            float f=130.8F/24343.3F;
            if(ampa == 1.0F){
                xv=px/50000.0F-0.5F; xv=xv*xv*xv;
                yv=py/50000.0F-0.5F; yv=yv*yv*yv;
            }
            float[] wt=weight(xv,yv);
            //Debug.Log(xv);
            //Debug.Log(yv);
            float v=neiro0(gain,ampa,Mathf.PI * 2.0F * (float)(phase + i),f,wt);
            
            for (int c = 0; c < channels; c++) // チャネル分コピー
            {
                data[(i * channels) + c] = v;
            }
        }
        //Debug.Log(sampleCount); 50*1024/100 20480
        phase += sampleCount;
    }
}