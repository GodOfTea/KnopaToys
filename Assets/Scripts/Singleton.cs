using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    private static Singleton instance = null;

    public static Singleton Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType(typeof(Singleton)) as Singleton;
            }

            if(instance == null)
            {
                GameObject obj = new GameObject("Singleton");
                instance = obj.AddComponent(typeof(Singleton)) as Singleton;
                Debug.Log("It`s done");
            }
            return instance;
        }
    }

    void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    //Выше создание сниглтона
    //---------------------------------------------------------------------------//
    //Ниже идут переменные и функции

    //Предыдущая сцена
    public int Game_Index;

    //Значение для профессии
    public int Extra_Index;

    //Для передачи скорости
    public float Speed_Value;
}