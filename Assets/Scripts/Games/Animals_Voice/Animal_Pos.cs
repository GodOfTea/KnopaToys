using UnityEngine;

public class Animal_Pos : Pos
{
    SoundManager sm;
    public Animals_Controller AC;

    public Animal animal; //все атриббуты нужного животного

    void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();

        is_Answer = false;
        Sprite_Ren = gameObject.GetComponent<SpriteRenderer>();
        Box_Col = gameObject.GetComponent<BoxCollider2D>();
        Drag = gameObject.GetComponent<Draggable>();

        _pos = gameObject.transform.position;
        Answer_Pos = new Vector3[2] { new Vector3(1.8f, 1.25f), new Vector3(1.8f, 1.25f) };
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
        if (Answer_Box != null && !AC.End_Game)
        {
            if (is_Answer)
            {
                sm.Play("Success");
                AC.lose_counter = 0;
                animal = AC.Right_Answer();
                gameObject.transform.position = Answer_Pos[0];
                Box_Col.enabled = false;
                is_Answer = false;
            }
            else
            {
                sm.Play("Error");
                AC.lose_counter += 1;
                Return_To_Start_Position();
            }
        }
        else
        {
            Return_To_Start_Position();
        }
    }

    public void Set_Sprite()
    {
        Sprite_Ren.sprite = animal.Sprite;
    }

    public void Return_To_Start_Position()
    {
        Set_Sprite();
        gameObject.transform.position = _pos;
        Box_Col.enabled = true;
    }
}
