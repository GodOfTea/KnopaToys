using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tag_Generator : MonoBehaviour
{
    /*
    * Фрукты - 1
    * Овощи - 2
    * Несъедобное - 3
    * Съедобное - 4
    */

    [Header("Распределительный тег")]
    public int Start_Tag;

    private Falling_Object fa;

    void Start()
    {
        fa = gameObject.GetComponent<Falling_Object>();

        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 13:
                Set_Tag(tag1:"Fruit", tag2:"Vegetable", value:1); 
                break;
            case 7:
                Set_Tag(tag1: "Inedible", tag2: "Edible", value:3);
                break;
        }
    }

    void Set_Tag(string tag1, string tag2, int value)
    {
        fa.Tag = Start_Tag == value ? tag1 : tag2;
    }
}
