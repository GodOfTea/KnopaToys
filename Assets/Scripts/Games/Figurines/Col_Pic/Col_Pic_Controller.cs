using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Col_Pic_Controller : MonoBehaviour
{
    public GameOver go;
    public SoundManager sm;
    public Figur_List FL;
    public GameObject Figur;
    public Text Color_Text;
    private bool end;

    public Vector3[,] Coordinates =
        {{new Vector3(-2.85f,3.3f,10), new Vector3(-0.95f,3.3f,10), new Vector3(0.95f,3.3f,10), new Vector3(2.85f,3.3f,10)},
         {new Vector3(-2.85f,1.4f,10), new Vector3(-0.95f,1.4f,10), new Vector3(0.95f,1.4f,10), new Vector3(2.85f,1.4f,10)},
         {new Vector3(-2.85f,-0.5f,10), new Vector3(-0.95f,-0.5f,10), new Vector3(0.95f,-0.5f,10), new Vector3(2.85f,-0.5f,10)},
         {new Vector3(-2.85f,-2.4f,10), new Vector3(-0.95f,-2.4f,10), new Vector3(0.95f,-2.4f,10), new Vector3(2.85f,-2.4f,10)}};

    Dictionary<int, string> Texts = new Dictionary<int, string>
        {{0, "Желтый"},
         {1, "Красный" },
         {2, "Фиолетовый" },
         {3, "Оранжевый" },
         {4, "Зеленый" },
         {5, "Синий" }};

    Dictionary<int, Color> Colors = new Dictionary<int, Color>
        {{0, new Color(1f, 0.7f, 0f, 1f)},
        {1, new Color(1f, 0f, 0f, 1f)},
        {2, new Color(0.7f, 0f, 0.9f, 1f)},
        {3, new Color(1f, 0.4f, 0.05f, 1f)},
        {4, new Color(0.06f, 0.7f, 0.1f, 1f)},
        {5, new Color(0f, 0.7f, 0.7f, 1f)}};

    Dictionary<int, string> Sounds = new Dictionary<int, string>
        {{0, "Yellow"},
         {1, "Red" },
         {2, "Purple" },
         {3, "Orange" },
         {4, "Green" },
         {5, "Blue" }};

    public int[] Colors_List = new int[6];
    public int Color_Now;

    [SerializeField]
    int i;
    bool firstStart;


    void Start()
    {
        go = GameObject.FindGameObjectWithTag("GameOver").GetComponent<GameOver>();
        sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();
        end = false;
        firstStart = true;
        Start_Spawn();
        i = 0;
        while (Colors_List[i] == 0)
            i++;
        Color_Now = i;

        int tip;                                                                      /* 0 - первый раз, 1 - уже было */
        /* Подсказка если игра открыта в первый раз */
        if (!PlayerPrefs.HasKey("ColFigGame")) { PlayerPrefs.SetInt("ColFigGame", 0); } /* Создали если не было */
        tip = PlayerPrefs.GetInt("ColFigGame");                                        /* Получили значение */

        if (tip == 0 && Setting_Menu.Sound)
        {
            sm.Play("ColFigGame");
            PlayerPrefs.SetInt("ColFigGame", 1);
            Color_Text.text = Texts[Color_Now];
            Color_Text.color = Colors[Color_Now];
            StartCoroutine(Delay());
        }
        else { Text_Settings(); }
    }

    void Update()
    {
        if(i <= 5)
        {
            if(Colors_List[i] == 0)
            {
                i++;
                Color_Now = i;
                end = true;
                if(i <= 5 && Colors_List[Color_Now] != 0)
                    Text_Settings();
            }
        }
        else if(end)
        {
            end = false;
            Color_Text.text = "ПОЗДРАВЛЯЮ!";
            Color_Text.color = Color.black;

            go.EndGame();
            //StartCoroutine(GameOver());
        }
    }

    void Text_Settings()
    {
        Color_Text.text = Texts[Color_Now];
        Color_Text.color = Colors[Color_Now];
        sm.Play(Sounds[Color_Now]);
    }

    public void SoundTip()
    {
        if (firstStart)
        {
            firstStart = false;
            sm.Play("ColFigGame");
        }
        else
        {
            sm.Play(Sounds[Color_Now]);
            //Debug.Log("Звук был");
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3.2f);
        sm.Play(Sounds[Color_Now]);
    }

    void Start_Spawn()
    {
        int fig = 0;
        int col = 0;

        SpriteRenderer SR = Figur.GetComponent<SpriteRenderer>();
        figur f = Figur.GetComponent<figur>();
        foreach (Vector3 vec in Coordinates)
        {
            fig = Random.Range(0, 5);
            col = Random.Range(0, 6);
            Colors_List[col] += 1;

            SR.sprite = FL.Figurs[fig].Figurs_Col[col];
            f.Color = col;
            Instantiate(Figur, vec, Quaternion.identity);
        }
    }
}
