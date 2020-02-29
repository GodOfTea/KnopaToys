using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Зона падения фигур
/// </summary>
public class Fall_Area : MonoBehaviour
{
    [Header("Список данной сессии")]
    public List<GameObject> Falling_Objects;
    [Header("Хранилище зон падения")]
    public GameObject[] Fall_Zones;

    public bool delete_drop;

    [Header("Задержка")]
    public float Spawn_Time;
    [Header("Кол-во зон падения")]
    public int fall_zones_count;
    [Header("Общее кол-во объектов")]
    public int number_of_objects;
    [Header("Макс. объектов справа")]
    public int length;

    private int i;
    private int obj;
    [HideInInspector]
    public int obj_on_screen;

    private Vector3 y = new Vector3(0, 2, 0);

    public int obj_remined;


    void Start()
    {
        i = Random.Range(0, fall_zones_count);
        obj = 0;
        obj_on_screen = 0;
        obj_remined = number_of_objects;
        Debug.Log(Falling_Objects.Count);

        /*Мешаю массив, путь пока будет тут*/
        for (int i = 0; i < Falling_Objects.Count; i++)
        {
            int j = Random.Range(0, Falling_Objects.Count-1);
            if (i!=j)
            {
                var tmp = Falling_Objects[i];
                Falling_Objects[i] = Falling_Objects[j];
                Falling_Objects[j] = tmp;
            }
            
        }

        StartCoroutine(Spawn());
    }

    void Repeat()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        for (int j = obj; j < number_of_objects && obj_on_screen < length;)
        {
            yield return new WaitForSeconds(Spawn_Time);
            GameObject f_object = Falling_Objects[obj++];
            Vector3 pos = Fall_Zones[i].transform.position + y;
            f_object.GetComponent<Falling_Object>().Start_Pos = pos; // проверить на оптимизацию
            //pos = f_object.GetComponent<Falling_Object>().Check_Pos(pos); // временный костыль
            Instantiate(f_object, pos, Quaternion.identity);
            
            obj_on_screen += 1;

            if (i >= fall_zones_count-1)
                i = 0;
            else
                ++i;
        }

        yield return new WaitForFixedUpdate();
        Repeat();
    }

    
}
