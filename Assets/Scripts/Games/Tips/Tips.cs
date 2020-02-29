using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tips : MonoBehaviour
{
    public Dictionary<int, string[]> Tips_Text = new Dictionary<int, string[]>
    {{1, new string[3] { "Нажми на кнопку в центре, чтобы услышать голос", "Кто из животных издает такой звук?", "Выбери и помести в кружочек правильный ответ" }},
    {2, new string[3] { "Помоги Кнопе!", "Выбери предметы, которые относятся к её профессии", "Помести в кружочки правильные ответы" }}};

    public GameObject Oblako;
    public Text text;
    public GameObject Panel;


    int i;
    int tip;
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 8)
        {
            i = 1;
        }
        else
        {
            i = 2;
        }
        tip = 0;
    }

    public void Show_Tip()
    {
        if (tip >= 3)
        {
            End_Tip();
        }
        else
        {
            Oblako.SetActive(true);
            Panel.SetActive(true);
            text.text = Tips_Text[i][tip];
            tip++;
        }
        
    }

    void End_Tip()
    {
        Oblako.SetActive(false);
        Panel.SetActive(false);
        tip = 0;
    }
}
