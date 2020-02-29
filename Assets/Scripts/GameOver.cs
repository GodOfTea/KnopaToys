using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver i;
    public SoundManager sm;

    void Awake()
    {
        if (i == null)
            i = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();

        DontDestroyOnLoad(gameObject);
    }

    public void EndGame()
    {
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        Debug.Log("Конец игры");
        sm.Play("Win");
        yield return new WaitForSeconds(5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(14);
    }
}
