using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Professions_Controller : MonoBehaviour
{
    public GameOver go;
    public SoundManager sm;

    // 1-Cook 2-Doc 3-Engineer 4-Hairdresser
    [Header("Список профессий")]
    public List<Profession> Professions;

    [Header("Ссылка на спрайт Кнопы")]
    public SpriteRenderer Knopa_Pos;

    [Header("Профессия данной сессии")]
    public Profession Main_Prof;

    [Header("Позиции для предметов")]
    public Profession_Pos[] Professions_Pos;

    [Header("Звезды за прав. ответ")]
    public Animation[] Stars;

    public Text Down_Bar_Text;
    public Text End_Text;

    Dictionary<int, string> Sounds = new Dictionary<int, string>
        {{1, "DoctorStaff"},
         {2, "MasterStaff" },
         {3, "HairstyleStaff" },
         {0, "CookStaff" }};


    //-------------------------------------------------------//
    [Header("Кол-во предметов")]
    public int Tools_Count;
    public int Win_Count;

    public bool End_Game;
    private bool end;

    private int ans_1, ans_2;

    void Awake()
    {
        go = GameObject.FindGameObjectWithTag("GameOver").GetComponent<GameOver>();
        sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();
        Win_Count = 0;
        End_Game = false;
    }

    void Start()
    {
        end = false;
        Shuffle_Lists(); // помешали списки
        Profession_Choice(); // выбрали профу
        Choose_Answer(); // выбрали ответы

        int tip;                                                                    /* 0 - первый раз, 1 - уже было */
        /* Подсказка если игра открыта в первый раз */
        if (!PlayerPrefs.HasKey(Sounds[Singleton.Instance.Extra_Index]))
        { PlayerPrefs.SetInt(Sounds[Singleton.Instance.Extra_Index], 0); }          /* Создали если не было */
        tip = PlayerPrefs.GetInt(Sounds[Singleton.Instance.Extra_Index]);           /* Получили значение */

        if (tip == 0 && Setting_Menu.Sound)
        {
            sm.Play(Sounds[Singleton.Instance.Extra_Index]);
            PlayerPrefs.SetInt(Sounds[Singleton.Instance.Extra_Index], 1);
        }
    }

    void Update()
    {
        if(Win_Count >= 6 && !end)
        {
            end = true;
            End_Game = true;
            Game_Over("Победа!");
        }
    }

    // возможно потом заменю на скрипт, пока велосипед (Присмотреться к Lua)
    public IEnumerator Remove_Answers()
    {
        // блокирование передвижения
        Block_and_Unblock_Col(false);
        // исчезновение
        Play_Animation_For_Group("Disappearance");
        yield return new WaitForSeconds (1f);
        // возвращение на позиции
        foreach (Profession_Pos pos in Professions_Pos)
            pos.Return_To_Start_Pos();
        //выбор новых ответов и спрайтов
        Choose_Answer();
        //появление
        Play_Animation_For_Group("Appearance");
        yield return new WaitForSeconds(1f);
        Block_and_Unblock_Col(true); ;
    }

    void Choose_Answer()
    {
        ans_1 = 0;
        ans_2 = 0;
        while (ans_1 == ans_2)
        {
            ans_1 = Random.Range(0, 4);
            ans_2 = Random.Range(0, 4);
        }

        Professions_Pos[ans_1].is_Answer = true;
        Professions_Pos[ans_2].is_Answer = true;

        Sprite_Insert();
    }

    public void SoundTip()
    {
       //Debug.Log(Singleton.Instance.Extra_Index);
       sm.Play(Sounds[Singleton.Instance.Extra_Index]);
    }

    void Sprite_Insert()
    {
        foreach (Profession_Pos pos in Professions_Pos)
        {
            Profession prof = null;
            if (pos.is_Answer)
            {
                prof = Main_Prof;
                pos.Sprite_Ren.sprite = prof.Prof_Tools[prof.Counter++];
            }
            else
            {
                prof = Professions[Random.Range(0, 3)];
                pos.Sprite_Ren.sprite = prof.Prof_Tools[prof.Counter++];
            }
            
        }
    }

    void Block_and_Unblock_Col(bool is_block)
    {
        foreach (Profession_Pos pos in Professions_Pos)
        {
            pos.Box_Col.enabled = is_block;
        }
    }

    //при надобности перенести потом в Anim_Contrl foreach(list<T>?, string anim)
    void Play_Animation_For_Group(string anim)
    {
        foreach (var obj in Professions_Pos) //Professions_Pos -> list<T>
        {
            obj.Anim.Play(anim);
        }
    }

    //public void Play_Animation_For_Star()
    //{
    //    Stars[Win_Count].Play();
    //}

    void Profession_Choice()
    {
        var prof = Singleton.Instance.Extra_Index; // индекс
        Main_Prof = Professions[prof];
        Professions.RemoveAt(prof);

        Down_Bar_Text.text = "НАЙДИ ВСЕ ПРЕДМЕТЫ ДЛЯ " + Main_Prof.Prof_Name.ToUpper() + "А";
        Knopa_Pos.sprite = Main_Prof.Knopa_Sprite; // присвоение спрайта
    }

    void Game_Over(string text)
    {
        //End_Game = false;

        Block_and_Unblock_Col(false);
        go.EndGame();
    }

    void Shuffle_Lists()
    {
        foreach(Profession prof in Professions)
        {
            var len = prof.Prof_Tools.Length;

            for (int i = len - 1; i > 0; i--)
            {
                var rand = Random.Range(0, len);
                var val = prof.Prof_Tools[rand];
                prof.Prof_Tools[rand] = prof.Prof_Tools[i];
                prof.Prof_Tools[i] = val;
            }
        }
    }
}
