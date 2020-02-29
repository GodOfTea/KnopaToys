using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Car
{
    public string Car_Name; // название машины

    public Sprite Circuit; // контур в левую часть

    public Sprite[] Details; // сборные детали

    // подумать, тут... нужна машина, с большим кол-вом box_cols и спрайтом
    public GameObject Car_Scheme;
}
