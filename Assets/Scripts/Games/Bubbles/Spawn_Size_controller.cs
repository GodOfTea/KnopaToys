using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Size_controller : MonoBehaviour
{
    // не используется
    public Vector3 Center;
    public Vector3 Size;

    public float x;
    public float y;

    void Awake()
    {
        x = Size.x / 2;
        y = Size.y / 2;
    }
}
