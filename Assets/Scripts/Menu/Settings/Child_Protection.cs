using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Child_Protection : MonoBehaviour
{
    public Text Question;
    public int Answer;

    public void Math_Test()
    {
        int a = Random.Range(3, 10);
        int b = Random.Range(3, 10);
        Answer = a * b;

        Question.text = a.ToString() + "*" + b.ToString() + "=?";
    }
}
