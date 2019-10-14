using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveCreator : MonoBehaviour
{

    public float maxSize = 0.25f;
    public AnimationCurve radiusAnim;
    public AnimationCurve amplitudeAnim;
    public AnimationCurve wavesizeAnim;
    public Gradient colorAnim;
    public AnimationCurve satAnim;
    public float speed = 0.02f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < SWs.Length; i++)
            {
                if (!SWs[i].active)
                {
                    //print(i.ToString());
                    //print(Input.mousePosition.ToString());
                    CreateSW(i, Input.mousePosition);

                    break;
                }
            }
        }

    }

    void FixedUpdate()
    {
        //Shader.SetGlobalVector("negCenter00", negCenter00);
        //Shader.SetGlobalVector("negRadius00", negRadius00);
        float t = 0f;

        for (int i = 0; i < SWs.Length; i++)
        {
            if (SWs[i].active)
            {
                SWs[i].time += speed;
                t = SWs[i].time;

                SWs[i].radius = radiusAnim.Evaluate(t) * maxSize;
                SWs[i].amplitude = amplitudeAnim.Evaluate(t);
                SWs[i].wavesize = wavesizeAnim.Evaluate(t);
                SWs[i].color = colorAnim.Evaluate(t);
                SWs[i].sat = satAnim.Evaluate(t);

                Shader.SetGlobalVector("SW_Center" + i.ToString("00"), SWs[i].center);
                Shader.SetGlobalFloat("SW_Radius" + i.ToString("00"), SWs[i].radius);
                Shader.SetGlobalFloat("SW_Amplitude" + i.ToString("00"), SWs[i].amplitude);
                Shader.SetGlobalFloat("SW_WaveSize" + i.ToString("00"), SWs[i].wavesize);
                Shader.SetGlobalColor("SW_Color" + i.ToString("00"), SWs[i].color);
                Shader.SetGlobalFloat("SW_Sat" + i.ToString("00"), SWs[i].sat);

                //print(i);
                //print(SWs[i].sat);

                if (t >= 1f)
                {
                    ClearSW(i);
                }
            }
        }
    }

    void CreateSW(int index, Vector2 position)
    {
        SWs[index].active = true;
        SWs[index].time = 0f;
        SWs[index].center = new Vector2(position.x / Screen.width, position.y / Screen.height);
        SWs[index].radius = 0f;
        SWs[index].amplitude = 0f;
        SWs[index].wavesize = 0f;
        SWs[index].color = Color.white;
        SWs[index].sat = 1f;
        //SWs[index].innerRadius = 0f;

        Shader.SetGlobalVector("SW_Center" + index.ToString("00"), SWs[index].center);
        Shader.SetGlobalFloat("SW_Radius" + index.ToString("00"), SWs[index].radius);
        Shader.SetGlobalFloat("SW_Amplitude" + index.ToString("00"), SWs[index].amplitude);
        Shader.SetGlobalFloat("SW_WaveSize" + index.ToString("00"), SWs[index].wavesize);
        Shader.SetGlobalColor("SW_Color" + index.ToString("00"), SWs[index].color);
        Shader.SetGlobalFloat("SW_Sat" + index.ToString("00"), SWs[index].sat);
    }

    void ClearSW(int index)
    {
        SWs[index].active = false;
        SWs[index].time = 0f;
        SWs[index].center = Vector2.zero;
        SWs[index].radius = 0f;
        SWs[index].amplitude = 0f;
        SWs[index].wavesize = 0f;
        SWs[index].radius = 0f;
        SWs[index].color = Color.white;
        SWs[index].sat = 1f;
        //SWs[index].innerRadius = 0f;

        Shader.SetGlobalVector("SW_Center" + index.ToString("00"), SWs[index].center);
        Shader.SetGlobalFloat("SW_Radius" + index.ToString("00"), SWs[index].radius);
        Shader.SetGlobalFloat("SW_Amplitude" + index.ToString("00"), SWs[index].amplitude);
        Shader.SetGlobalFloat("SW_WaveSize" + index.ToString("00"), SWs[index].wavesize);
        Shader.SetGlobalColor("SW_Color" + index.ToString("00"), SWs[index].color);
        Shader.SetGlobalFloat("SW_Sat" + index.ToString("00"), SWs[index].sat);
    }


    [HideInInspector]
    public ShockWave[] SWs = new ShockWave[10];

    [System.Serializable]
    public struct ShockWave
    {
        public Vector2 center;
        public float radius;
        public float amplitude;
        public float wavesize;
        public Color color;
        public float sat;
        public float time;
        public bool active;
    }

}
