using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings_Button : MonoBehaviour
{
    public GameObject Back_Panel;

    public Child_Protection CP;

    public void Protection()
    {
        bool is_active = Back_Panel.activeSelf;
        
        Back_Panel.SetActive(!is_active);

        CP.Math_Test();
    }

    public Text Question;

    public void Check_Answer(int scene_index)
    {
        if (CP.Answer.ToString() == Question.text)
        {
            SceneManager.LoadScene(scene_index);
        }
        else
        {
            Question.text = null;
        }
    }
}
