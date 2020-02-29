using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormFigController : MonoBehaviour
{
    public SoundManager sm;

    void Start()
    {
        sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();

        int tip;                                                                      /* 0 - первый раз, 1 - уже было */
        /* Подсказка если игра открыта в первый раз */
        if (!PlayerPrefs.HasKey("FormFigGame")) { PlayerPrefs.SetInt("FormFigGame", 0); } /* Создали если не было */
        tip = PlayerPrefs.GetInt("FormFigGame");                                        /* Получили значение */

        if (tip == 0 && Setting_Menu.Sound)
        {
            sm.Play("FormFigGame");
            PlayerPrefs.SetInt("FormFigGame", 1);
        }
    }
}
