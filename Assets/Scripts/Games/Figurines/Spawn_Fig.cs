using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Fig : MonoBehaviour
{
    public Drop_Area DA;
    public Fall_Area FA;

    public Figur[] Figurs; //шаблоны

    public GameObject[] Pos;
    public GameObject[] Drop_Pos;

    public int Number_Of_Figs;

    /*
     * Как вибирается фигура и её контур?
     * Необходимо взять две цифры: fig[0-4] и col[0-6]
     * По ним выбриается фигура и её цвет
     * После этого они передают свои данные в Pos и Drop_Pos
     * И уже эти два объекта уходят в Drop_Area и Fall_Area
     */
     void Awake()
    {
        //Pos_Sp_Ren = Pos.GetComponent<SpriteRenderer>();
        //Drop_Pos_Sp_Ren = Drop_Pos.GetComponent<SpriteRenderer>();
        Create_Fig_List();
    }

    public void Create_Fig_List()
    {
        int fig = 0, col = 0;

        for (int i = 0; i < Number_Of_Figs; i++)
        {
            fig = Random.Range(0, 5);
            col = Random.Range(0, 5); //у многоугольника нет одного цвета, пока без синего

            //передаем данные
            Fill_Pos(fig, col, i);
            Fill_Drop_Pos(fig, col, i);
        }
    }

    void Fill_Pos(int fig, int col, int i)
    {
        Pos[i].GetComponent<SpriteRenderer>().sprite = Figurs[fig].Figurs_Col[col];
        Pos[i].GetComponent<Falling_Object>().Tag = fig.ToString() + col.ToString();
        FA.Falling_Objects.Add(Pos[i]);
    }

    void Fill_Drop_Pos(int fig, int col, int i)
    {
        Drop_Pos[i].GetComponent<SpriteRenderer>().sprite = Figurs[fig].Figurs_Circl_Col[col];
        Drop_Pos[i].GetComponent<Drop_Object>().Tag = fig.ToString() + col.ToString();
        DA.Drop_Objects.Add(Drop_Pos[i]);
    }
}
