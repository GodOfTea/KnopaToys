using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Зона, куда помещаются фигуры
/// Оптимизация:
/// </summary>
public class Drop_Area : MonoBehaviour
{
    public GameOver go;
    public List<GameObject> Drop_Objects; // объекты данной сессии

    [SerializeField]
    private float range;
    public float spawn_range;

    private Vector3 start_pos;
    public int length;
    private int obj_id;
    private bool shifting;
    private bool end;


    void Start()
    {
        go = GameObject.FindGameObjectWithTag("GameOver").GetComponent<GameOver>();
        start_pos = new Vector3(-4f, -2f, 50f);
        Spawn_Objects();
        range = spawn_range / 2;
        shifting = false;
        end = false;
    }

    void Update()
    {
        List_Checking();

        if(length <= 0 && !end)
        {
            end = true;
            go.EndGame();
            //StartCoroutine(GameOver());
        }
    }

    void Spawn_Objects()
    {
        foreach (var obj in Drop_Objects)
        {
            start_pos.y += spawn_range;
            Instantiate(obj, start_pos, Quaternion.identity);
        }

        length = Drop_Objects.Count;

        Drop_Objects.RemoveRange(0, length);
        Drop_Objects.AddRange(GameObject.FindGameObjectsWithTag("Drop_Object"));
    }

    void List_Checking()
    {
        for (int i = 0; i < length; i++)
        {
            if (Drop_Objects[i].GetComponent<Drop_Object>().is_correct)  // можно оптимизировать
            {
                obj_id = i;
                shifting = true;
            }
        }
        if (shifting)
        {
            Shift_Objects();
            shifting = false;
        }
    }

    void Shift_Objects()
    {
        for (int i = 0; i < length; i++)
        {
            var drop_obj = Drop_Objects[i];
            if (i < obj_id)
            {
                var pos = drop_obj.transform.position;
                drop_obj.GetComponent<Drop_Object>().Start_Pos = pos;  //можно оптимизировать
                drop_obj.GetComponent<Drop_Object>().End_Pos = new Vector3(pos.x, pos.y + range, 50f);
            }
            else if (i > obj_id)
            {
                var pos = drop_obj.transform.position;
                drop_obj.GetComponent<Drop_Object>().Start_Pos = pos; // можно оптимизировать
                drop_obj.GetComponent<Drop_Object>().End_Pos = new Vector3(pos.x, pos.y - range, 50f);
            }
        }

        Remove();
    }

    void Remove()
    {
        var drop_obj = Drop_Objects[obj_id];
        Drop_Objects.Remove(drop_obj);
        length = Drop_Objects.Count;
        Debug.Log(length);
        drop_obj.GetComponent<Drop_Object>().Remove();  // можно оптимизировать
    }

    public void Permutation(GameObject obj)
    {
        Drop_Objects.Remove(obj);
        Drop_Objects.Add(obj);
        //на всяк
        length = Drop_Objects.Count;
    }

    public Vector3 Last_Object_Posititon()
    {
        var obj = Drop_Objects[Drop_Objects.Count() - 1];
        var pos = obj.transform.position;
        pos.y += spawn_range;

        if (pos.y < 5.5f)
            pos.y = 5.5f;

        return pos;
    }

    //IEnumerator GameOver()
    //{
    //    yield return new WaitForSeconds(5f);
    //    UnityEngine.SceneManagement.SceneManager.LoadScene(14);
    //}
}
