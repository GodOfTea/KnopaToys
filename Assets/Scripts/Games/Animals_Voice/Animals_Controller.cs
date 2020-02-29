using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animals_Controller : MonoBehaviour
{
    public GameOver go;
    public SoundManager sm;

    public Animal[] Animals;
    public Animal_Pos[] Animals_Pos;

    public GameObject EndCloud;
    public Text       GameOverText;

    public GameObject Star;
    public AudioSource Answer;
    public Text End_Text;
    public Vector3 Star_Pos;

    public bool End_Game;
    private bool _new_answer;
    private int _rand_animal;

    [SerializeField]
    private int _length;
    [SerializeField]
    private int counter;
    public int lose_counter;

    void Awake()
    {
        go = GameObject.FindGameObjectWithTag("GameOver").GetComponent<GameOver>();
        sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();

        End_Game = false;
        foreach(Animal a in Animals)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.Voice;
        }
    }

    void Start()
    {
        EndCloud.SetActive(false);
        lose_counter = 0;
        _new_answer = true;
        _length = Animals.Length;

        //Перемешиваем список
        for (int i = _length-1 ; i>0 ; i--)
        {
            var rand = Random.Range(0, _length);
            var val = Animals[rand];
            Animals[rand] = Animals[i];
            Animals[i] = val;
        }

        for (int i = 0; i < 4; i++)
        {
            Animals_Pos[i].animal = Spawn_Animal();
            Animals_Pos[i].Set_Sprite();
        }

        int tip;                                                                      /* 0 - первый раз, 1 - уже было */
        /* Подсказка если игра открыта в первый раз */
        if (!PlayerPrefs.HasKey("VoiceGame")) { PlayerPrefs.SetInt("VoiceGame", 0); } /* Создали если не было */
        tip = PlayerPrefs.GetInt("VoiceGame");                                        /* Получили значение */

        if (tip == 0 && Setting_Menu.Sound)
        {
            sm.Play("VoiceGame");
            PlayerPrefs.SetInt("VoiceGame", 1);
        }
    }

    void Update()
    {
        if (counter >= 14 && !End_Game)
        {
            //+ переход
            StartCoroutine(Game_Over("Победа!", true));
        }
        

        if (lose_counter >= 3 && !End_Game)
        {
            StartCoroutine(Game_Over("Проигрыш", false));
        }

    }

    Animal Spawn_Animal()
    {
        var a = counter;
        counter += 1;
        return Animals[a];
    }

    public void Choose_Answer()
    {
        if (_new_answer)
        {
            _new_answer = false;

            foreach (Animal_Pos ap in Animals_Pos)
            {
                ap.Return_To_Start_Position();
            }

            _rand_animal = Random.Range(0, 4);
            Animals_Pos[_rand_animal].is_Answer = true;
        }

        Animals_Pos[_rand_animal].animal.source.Play();

    }

    public Animal Right_Answer()
    {
        // показать звездочку на время (анимация)
        //StartCoroutine(Show_Star());
        Instantiate(Star, Star_Pos, Quaternion.identity);
        Star_Pos.x += 1;
        _new_answer = true;
        return Spawn_Animal();
    }

    IEnumerator Game_Over(string text, bool win)
    {
        End_Game = true;

        foreach (Animal_Pos ap in Animals_Pos)
        {
            ap.Box_Col.enabled = false;
        }

        End_Text.text = text;  
        
        if (win)
        {
            EndCloud.SetActive(true);
            GameOverText.text = "Поздравляю, ты победил!";
            yield return new WaitForSeconds(0.1f);
            go.EndGame();
        }
        else if (!win)
        {
            EndCloud.SetActive(true);
            GameOverText.text = "Ты проиграл. Попробуй ещё раз";
            yield return new WaitForSeconds(3f);
        }
    }
}
