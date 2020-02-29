using System.Collections;
using System;
using UnityEngine;

public class Animal_Voice_Controller : MonoBehaviour
{
    public static Animal_Voice_Controller i;

    public Sound[] Voices;

    void Awake()
    { 
        foreach (Sound s in Voices)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(Voices, Sound => Sound.Name == name);

        if (s == null)
        {
            Debug.LogError("Sound " + name + " not found");
            return;
        }

        s.source.Play();
    }

}
