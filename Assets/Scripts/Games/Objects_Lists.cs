using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Списки объектов, слева и справа
/// Оптимизация: все впорядке
/// </summary>
public class Objects_Lists : MonoBehaviour
{
    public bool is_random;
    public bool is_col;

    public int number_of_objects;
    [SerializeField]
    private int length;

    public Drop_Area da;
    public Fall_Area fa;

    public GameObject[] Drop_List; // хранилище шаблонов drop
    public GameObject[] Fall_List; //хранилище шаблонов fall

    public void Awake()
    {
            length = Drop_List.Length;
            if (length == 0)
                length = Fall_List.Length;

            //if(is_col)

            if (is_random)
                Create_Random_Lists();
            else
                Create_Lists();
    }

    public void Create_Lists()
    {
        for (int i = length-1; i > 0; i--)
        {
            var rand_obj = Random.Range(0, length);
            GameObject val;
            if (da != null)
            {
                val = Drop_List[rand_obj];
                Drop_List[rand_obj] = Drop_List[i];
                Drop_List[i] = val;
            }

            if (fa != null)
            {
                val = Fall_List[rand_obj];
                Fall_List[rand_obj] = Fall_List[i];
                Fall_List[i] = val;
            }
        }


        for (int i = 0; i < number_of_objects; i++)
        {
            Create_Object(i);
        }
    }

    public void Create_Random_Lists()
    {
        for (int i = 0; i < number_of_objects; i++)
        {
            var rand_obj = Random.Range(0, length);
            Create_Object(rand_obj);
        }
    }

    public void Create_Object(int i)
    {
        GameObject obj;
        //drop
        if (da != null)
        {
            obj = Drop_List[i];
            da.Drop_Objects.Add(obj);
        }
        //fall
        if (fa != null)
        {
            obj = Fall_List[i];
            fa.Falling_Objects.Add(obj);
        }
    }
}
