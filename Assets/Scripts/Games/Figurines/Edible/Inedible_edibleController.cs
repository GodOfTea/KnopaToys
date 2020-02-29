using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inedible_edibleController : MonoBehaviour
{
    public SoundManager sm;

    void Start()
    {
        sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();

        int tip;                                                                      /* 0 - первый раз, 1 - уже было */
        /* Подсказка если игра открыта в первый раз */
        if (!PlayerPrefs.HasKey("EdibleGame")) { PlayerPrefs.SetInt("EdibleGame", 0); } /* Создали если не было */
        tip = PlayerPrefs.GetInt("EdibleGame");                                        /* Получили значение */

        if (tip == 0 && Setting_Menu.Sound)
        {
            sm.Play("EdibleGame");
            PlayerPrefs.SetInt("EdibleGame", 1);
        }
    }
}
