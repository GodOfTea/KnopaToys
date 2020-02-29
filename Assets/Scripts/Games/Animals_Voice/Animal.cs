using UnityEngine;

[System.Serializable]
public class Animal
{
    public string Name;

    public Sprite Sprite;

    public AudioClip Voice;

    [HideInInspector]
    public AudioSource source;
}
