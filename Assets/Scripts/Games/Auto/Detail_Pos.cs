using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detail_Pos : Pos
{
    public SoundManager sm;
    public Auto_Cnotroller AC;

    public int Tag;

    Vector3 Zero;
    Vector3 End_Pos;
    bool rot; //поворот - да
    bool move;
    float speed = 1f;

    void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();
        AC = GameObject.FindGameObjectWithTag("GameController").GetComponent<Auto_Cnotroller>();
        Sprite_Ren = gameObject.GetComponent<SpriteRenderer>();
        Box_Col = gameObject.GetComponent<BoxCollider2D>();
        Drag = gameObject.GetComponent<Draggable>();
        Anim = gameObject.GetComponent<Animation>();
    }

    void Start()
    {
        End_Pos = gameObject.transform.position;
        rot = false;
        Zero = Vector3.zero;
        //gameObject.transform.position = _pos;
    }

    void Update()
    {
        if (Drag.is_Up)
        {
            Drag.is_Up = false;
            Check_Answer();
        }

        if (rot)
        {
            Quaternion rot_q = Quaternion.Euler(Zero);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rot_q, Time.deltaTime * speed);
        }

        Offset();
    }

    void Offset()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, End_Pos, Time.deltaTime * speed);
    }

    void Check_Answer()
    {
        if (Answers.Count != 0) //+ endgame
        {
            foreach (var ans in Answers)
            {
                
                if (Tag == ans.GetComponent<Detail>().Tag)
                {
                    sm.Play("Success");
                    End_Pos = ans.transform.position;
                    Box_Col.enabled = false;
                    ans.GetComponent<BoxCollider2D>().enabled = false;
                    rot = true;
                    AC.Details_in_Place--;
                    break;
                }
                else
                {
                    sm.Play("Error");
                    Return_To_Start_Pos();
                }
            }
        }
        else
        {
            Return_To_Start_Pos();
        }
    }

    public void Return_To_Start_Pos()
    {
        End_Pos = _pos;
        //move = true;
        //gameObject.transform.position = _pos;
    }
}
