using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back_Button : MonoBehaviour
{
    public bool Go_back = false;

    void Update()
    {
        if (Go_back)
            SceneManager.GetSceneAt(0);
    }
}
