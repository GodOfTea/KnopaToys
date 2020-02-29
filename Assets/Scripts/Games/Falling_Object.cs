using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Падающие объекты
/// Оптимизация: почти все
/// </summary>
public class Falling_Object : MonoBehaviour
{
    private Fall_Area fa;
    public Draggable drag;
    public SoundManager sm;
    public BoxCollider2D Box_Col;

    [Header("Тег совпадения")]
    public string Tag;
    public string drop_tag;

    public Vector2 Direction; // направление падения

    [SerializeField]
    private float Speed; // скорость падения

    public GameObject Drop_Obj; // ?

    private bool is_delete;

    [SerializeField]
    private Vector3 start_pos; // позиция появления
    [SerializeField]
    private Vector3 check_pos;

    public Vector3 Start_Pos
    {
        get { return start_pos; }
        set { start_pos = value; }
    }
    public Vector3 End_Pos;

    Vector3 from_pos;
    float pos;
    [SerializeField]
    float lerp_speed;
    
    void Awake()
    {
        Box_Col = GetComponent<BoxCollider2D>();
        sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();
        fa = GameObject.FindGameObjectWithTag("Fall_Area").GetComponent<Fall_Area>();
        drag = gameObject.GetComponent<Draggable>();
        is_delete = fa.delete_drop;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 13 || SceneManager.GetActiveScene().buildIndex == 7)
            Speed = Singleton.Instance.Speed_Value;

        End_Pos = Start_Pos;

        check_pos = Start_Pos + new Vector3(0, 5.5f, 0); //y=12
    }

    void FixedUpdate()
    {
        if (!drag.is_Dragging)
        {
            if (transform.position != End_Pos)
            {
                Box_Col.enabled = false;
                transform.position = Vector3.Lerp(from_pos, End_Pos, pos += lerp_speed);
            }
            else
            {
                transform.Translate(Direction.normalized * Speed);
                Normalization();
            }
        }
        
    }

    void Update()
    {
        //если не тянется
        if (!drag.is_Dragging)
        {
            if (drop_tag == Tag && Drop_Obj != null)
            {
                sm.Play("Success");
                fa.obj_remined--;
                if(is_delete)
                    Drop_Obj.GetComponent<Drop_Object>().is_correct = true;
                Remove();
            }
            else if(drag.is_Up)
            {
                sm.Play("Error");
                drag.is_Up = false;
                End_Pos = Start_Pos;
                from_pos = transform.position;
                //Return_To_Start_Pos();
            }
        }
    }

    void Normalization()
    {
        Box_Col.enabled = true;
        pos = 0;
        //Start_Pos = gameObject.transform.position;
        End_Pos = gameObject.transform.position;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Fall_Zone" && !drag.is_Dragging)
        {
            Return_To_Start_Pos();
        }

        if (col.tag == "Drop_Object")
        {
            Drop_Obj = null;
            drop_tag = null;
        }
    }

    //в идеале изменить
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Drop_Object")
        {
            Drop_Obj = col.gameObject;
            drop_tag = Drop_Obj.GetComponent<Drop_Object>().Tag;
        }
    }

    public void Return_To_Start_Pos()
    {
        gameObject.transform.position = Check_Pos();
        Normalization();
    }

    public Vector3 Check_Pos()
    {
        // ахуенный способ!
        RaycastHit2D hit = Physics2D.Raycast(check_pos, -gameObject.transform.up, 8);
        
        if (hit && hit.transform.tag == "Falling_Object")
        {
            //Debug.Log(hit.transform.name);
            return hit.transform.position + new Vector3(0, 2.5f);
        }
        else
        {
            return Start_Pos;
        }
    }

    public void Remove()
    {
        fa.obj_on_screen -= 1;
        Destroy(gameObject);
    }
}
