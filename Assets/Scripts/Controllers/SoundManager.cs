using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager i;

    public Sound[] Sounds;

    void Awake()
    {
        if (i == null)
            i = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in Sounds)
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
        Sound s = Array.Find(Sounds, Sound => Sound.Name == name);

        if (s == null)
        {
            Debug.LogError("Sound " + name + " not found");
            return;
        }

        if(Setting_Menu.Sound)
            s.source.Play();
    }
}


/*
 * Sounds:
 *  Error
 *  Success
 *  Destroy
 *  EdibleGame ++
 *  DoctorStaff ++
 *  MasterStaff ++
 *  HairstyleStaff ++
 *  CookStaff ++
 *  Car ++
 *  VoiceGame ++
 *  Yellow ++
 *  Red ++
 *  Green ++
 *  Orange ++
 *  Blue ++
 *  Purple ++
 *  Win +
 *  DinosGame ++
 *  StartApp +
 *  FruVegGame ++
 *  BubblesGame ++
 *  PuzzleGame ++
 *  FigGame ++
 *  ColFigGame ++/+
 *  FormFigGame ++
 */


