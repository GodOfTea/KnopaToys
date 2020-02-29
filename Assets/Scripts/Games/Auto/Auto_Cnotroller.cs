using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Че как?! Я снова в деле!
 * Так, первая задача - выбрать машину, чек!
 * Дальше более скучной записью, фу...
 * 
 * 2. Заспавнить GameObject Main_Car
 * 3. Заспавнить Запчасти, под углом +-25
 * 3.1 Проверка на то, что они появляются в разных местах и не пересекаются
 * 3.2 Ограничить их в рамке спавна
 * 4. Перетащить их
 * 5. Добавить поддтягивание (Lerp)
 * 6. Маленькие box_colliders (доп. скрипт на запчастях контура)
 * 7. Победа, изи.
 * 
 */

public class Auto_Cnotroller : MonoBehaviour
{
    public GameOver go;
    public Spawn_Deteils SD;
    public SoundManager sm;


    public Car[] Cars;
    public Vector3 Main_Car_Pos;

    public int Details_in_Place;
    private bool end;

    [SerializeField]
    Car Main_Car;

    void Awake()
    {
        go = GameObject.FindGameObjectWithTag("GameOver").GetComponent<GameOver>();
        sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();
        // выбор машины
        Main_Car = Cars[Random.Range(0, Cars.Length)];
        Instantiate(Main_Car.Car_Scheme, Main_Car_Pos, Quaternion.identity);

        int tip;                                                                      /* 0 - первый раз, 1 - уже было */
        /* Подсказка если игра открыта в первый раз */
        if (!PlayerPrefs.HasKey("Car")) { PlayerPrefs.SetInt("Car", 0); } /* Создали если не было */
        tip = PlayerPrefs.GetInt("Car");                                        /* Получили значение */

        if (tip == 0 && Setting_Menu.Sound)
        {
            sm.Play("Car");
            PlayerPrefs.SetInt("Car", 1);
        }

    }

    void Start()
    {
        end = false;
        Details_in_Place = Main_Car.Details.Length;
        SD.Spawn(Main_Car.Details);
    }

    void Update()
    {
        if(Details_in_Place <= 0 && !end)
        {
            end = true;
            go.EndGame();
        }
    }

    //IEnumerator GameOver()
    //{
    //    yield return new WaitForSeconds(5f);
    //    UnityEngine.SceneManagement.SceneManager.LoadScene(14);
    //}
}
