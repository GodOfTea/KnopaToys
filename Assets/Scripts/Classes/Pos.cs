using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos : MonoBehaviour
{
    //[HideInInspector]
    public SpriteRenderer Sprite_Ren;
    [HideInInspector]
    public BoxCollider2D Box_Col;
    [HideInInspector]
    public Animation Anim;
    public Draggable Drag;

    public GameObject Answer_Box;
    public bool is_Answer;
    public Vector3 _pos;
    public Vector3[] Answer_Pos;

    public List<GameObject> Answers;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Answer")
        {
            Answers.Add(col.gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        //Debug.Log(col.name);
        if (col.tag == "Answer")
        {
            Answer_Box = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Answer")
        {
            Answer_Box = null;
            Answers.Remove(col.gameObject);
        }
    }
}
