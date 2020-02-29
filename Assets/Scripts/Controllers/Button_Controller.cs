using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Controller : MonoBehaviour
{
    public SoundManager sm;


    void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();
    }

    public void Change_Scene(int scene_index)
    {
        SceneManager.LoadScene(scene_index);
    }

    public void Set_Game_Index(int game_index)
    {
        Singleton.Instance.Game_Index = game_index;
    }

    public void Set_Extra_Index(int index)
    {
        Singleton.Instance.Extra_Index = index;
    }

    public void Set_Speed(float speed)
    {
        if (speed == 0)
            speed = 0.2f;

        Singleton.Instance.Speed_Value = speed;
    }

    public void Tip(string sound)
    {
        sm.Play(sound);
        StartCoroutine(SoundDelay());
    }

    IEnumerator SoundDelay()
    {
        yield return new WaitForSeconds(3f);
        /* добавить задержку */
    }

    public void Exit()
    {
        Application.Quit();
    }
}
