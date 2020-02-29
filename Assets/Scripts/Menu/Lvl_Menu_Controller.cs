using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lvl_Menu_Controller : MonoBehaviour
{
    public Game[] Games;

    void Awake()
    {
        int game_i = Singleton.Instance.Game_Index;

        if(game_i != 0)
        {
            Show_Buttons(game_i-1);
            Debug.Log("Найдена игра " + game_i);
        }
        else
        {
            Debug.LogError("Игра не найдена!");
        }
    }

    void Show_Buttons(int i)
    {
        var game_buttons = Games[i].Lvl_Buttons;
        var game_texts = Games[i].Lvl_Texts;
        var game_imgs = Games[i].Lvl_Images;
        int val = 0;

        foreach (Button button in game_buttons)
        {
            //button.transform.GetChild(0).GetComponent<Text>().text = game_texts[val];
            //button.transform.GetChild(1).GetComponent<Image>().sprite = game_imgs[val];

            button.gameObject.SetActive(true);
            //Debug.Log("Я появилась!");
            val += 1;
        }
    }
}
