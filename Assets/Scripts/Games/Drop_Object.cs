using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Объект для вставки
/// </summary>
public class Drop_Object : MonoBehaviour
{
    public Drop_Area Drop_Area;

    [Header("Тег совпадения")]
    public string Tag;

    [SerializeField]
    private Vector3 start_pos;
    [SerializeField]
    private Vector3 end_pos;

    public Vector2 Direction;

    public float Step;
    public float Speed;

    [SerializeField]
    public bool is_correct;


    private float position;

    public Vector3 Start_Pos
    {
        private get { return start_pos; }
        set { start_pos = value; }
    }
    public Vector3 End_Pos
    {
        private get { return end_pos; }
        set { end_pos = value; }
    }


    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Drop_Area") != null) //ват? Надо убрать или заменить
            Drop_Area = GameObject.FindGameObjectWithTag("Drop_Area").GetComponent<Drop_Area>();
        is_correct = false;
        Start_Pos = transform.position;
        End_Pos = transform.position;
        //Speed = Step / 2;
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Drop_Zone" && !is_correct)
        {
            Rise();
        }
    }


    void Move()
    {
        if ((transform.position != End_Pos))
        {
            Offset();
        }
        else
        {
            transform.Translate(Direction.normalized * Speed);
            Normalization();
        }
    }

    public void Offset()
    {
        transform.position = Vector3.Lerp(Start_Pos, End_Pos, position += Step);
    }

    public void Remove()
    {
        //yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    void Rise()
    {
        gameObject.transform.position = Drop_Area.Last_Object_Posititon();
        Normalization();
        Drop_Area.Permutation(gameObject);
    }

    void Normalization()
    {
        position = 0;
        Start_Pos = gameObject.transform.position;
        End_Pos = gameObject.transform.position;
    }

    //public void Modify(Sprite new_sprite)
    //{
    //    is_correct = true;
    //}

}
