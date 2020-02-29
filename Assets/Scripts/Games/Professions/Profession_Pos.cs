using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profession_Pos : Pos
{
    private SoundManager sm;
    public  Professions_Controller PC;


    void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();

        is_Answer = false;
        Sprite_Ren = gameObject.GetComponent<SpriteRenderer>();
        Box_Col = gameObject.GetComponent<BoxCollider2D>();
        Drag = gameObject.GetComponent<Draggable>();
        Anim = gameObject.GetComponent<Animation>();

        _pos = gameObject.transform.position;
        Answer_Pos = new Vector3[2] { new Vector3(-3.4f, 1.4f), new Vector3(3.4f, 1.4f) };
    }

    void Update()
    {
        if (Drag.is_Up)
        {
            Drag.is_Up = false;
            Check_Answer();
        }
    }

    void Check_Answer()
    {
        if (Answer_Box != null && !PC.End_Game)
        {
            if(is_Answer)
            {
                //PC.Play_Animation_For_Star(); //изменить.
                sm.Play("Success");
                PC.Win_Count += 1;
                bool odd = (PC.Win_Count % 2 == 0);
                is_Answer = false;
                Box_Col.enabled = false;
                gameObject.transform.position = (odd ? Answer_Pos[1] : Answer_Pos[0]);

                if (odd)
                    StartCoroutine(PC.Remove_Answers());
            }
            else
            {
                sm.Play("Error");
                //lose_counter +=1
                Return_To_Start_Pos();
            }
        }
        else
        {
            Return_To_Start_Pos();
        }
    }

    public void Return_To_Start_Pos()
    {
        gameObject.transform.position = _pos;
    }
}
