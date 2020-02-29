using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubbles_Controller : MonoBehaviour
{
    public SoundManager sm;

    [SerializeField] private GameObject CloudTip; 
    [SerializeField] private Transform  Spawn_Pos;
    [SerializeField] private float      Spawn_Time;
    [SerializeField] private GameObject Bubble;
    [SerializeField] private int        count;
    

    void Start()
    {
        sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();
        CloudTip.SetActive(false);

        int tip;                                                                      /* 0 - первый раз, 1 - уже было */
        /* Подсказка если игра открыта в первый раз */
        if (!PlayerPrefs.HasKey("BubblesGame")) { PlayerPrefs.SetInt("BubblesGame", 0); } /* Создали если не было */
        tip = PlayerPrefs.GetInt("BubblesGame");                                        /* Получили значение */

        if (tip == 0 && Setting_Menu.Sound)
        {
            sm.Play("BubblesGame");
            PlayerPrefs.SetInt("BubblesGame", 1);
        }

        Repeat();
    }

    void Repeat()
    {
        if (count > 0)
        {
            StartCoroutine(Spawn_Bubble());
            count--;
        }
        else
        {
            StartCoroutine(End());
            /* Конец игры? */
        }
    }

    IEnumerator Spawn_Bubble()
    {
        yield return new WaitForSeconds(Spawn_Time);
        Instantiate(Bubble, Spawn_Pos.position, Quaternion.identity);
        Repeat();
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(2f);
        CloudTip.SetActive(true);
    }


    //СТАРЫЙ КОД
    /*
    public List<Bubble> Bubbles;
    public Image Back_Button;

    public int Bubbles_Count;
    int i;
    
    void Start()
    {
        i = 0;
        StartCoroutine(Back_To_Menu());
        StartCoroutine(Bubbles_Spawner());
    }

    IEnumerator Bubbles_Spawner()
    {
        yield return new WaitForSeconds(0.2f);
        Bubbles[i].Spawn();
        i++;
        if (i < Bubbles_Count)
            Repeat();

    }

    void Repeat()
    {
        StartCoroutine(Bubbles_Spawner());
    }

    IEnumerator Back_To_Menu()
    {
        Back_Button.enabled = false;
        yield return new WaitForSeconds(3f);
        Back_Button.enabled = true;
    }*/
}
