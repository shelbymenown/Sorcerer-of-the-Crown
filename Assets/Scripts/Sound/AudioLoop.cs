using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoop : MonoBehaviour
{
    public AudioSource[] musicSources;
    public int musicBPM, timeSignature, barsLength;
    public bool overrideTime = false;
    public float controlTime; // This is total time of the loopable

    private float loopPointSeconds;
    private double time;
    private int nextSource, size;
    private bool firstPlay = true;

    // Start is called before the first frame update
    void Start()
    {
        loopPointSeconds = ((float) (barsLength * timeSignature) / musicBPM) * 60;

        if (overrideTime)
        {
            loopPointSeconds = controlTime;
        }

        Debug.Log(AudioSettings.dspTime);
        Debug.Log("loopPointSeconds = " + loopPointSeconds);
        musicSources[0].Play();
        nextSource = 1;
        size = musicSources.Length;

        if (size == 2)
        {
            firstPlay = false;

            time = AudioSettings.dspTime;

            time = time + loopPointSeconds;

            Debug.Log("Time: " + time);

            musicSources[1].PlayScheduled(time);

            nextSource = 0;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (firstPlay && !musicSources[0].isPlaying)
        {
            firstPlay = false;

            time = AudioSettings.dspTime;

            time = time + loopPointSeconds;

            musicSources[1].Play();

            musicSources[2].PlayScheduled(time);
        }
        //Debug.Log(AudioSettings.dspTime);
        if (!firstPlay && !musicSources[nextSource].isPlaying)
        {
            Debug.Log(nextSource);
            Debug.Log(musicSources[nextSource].isPlaying);
            Debug.Log(AudioSettings.dspTime);
            time = time + loopPointSeconds;

            musicSources[nextSource].PlayScheduled(time);

            nextSource = (1 + 2 * (size - 2)) - nextSource; //Switches between last two AudioSources
        }

        /*
        if (!firstPlay && !musicSources[1].isPlaying)
        {
            Debug.Log(1);
            Debug.Log(musicSources[1].isPlaying);
            time = time + loopPointSeconds;

            musicSources[1].PlayScheduled(time);
        }

        if (!firstPlay && !musicSources[2].isPlaying)
        {
            Debug.Log(2);
            Debug.Log(musicSources[2].isPlaying);
            time = time + loopPointSeconds;

            musicSources[2].PlayScheduled(time);
        }
        */
    }
}
