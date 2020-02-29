using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice_Button : MonoBehaviour
{
    public Animals_Controller AC;
    //public GameObject Answer_Pos;

    public Sprite[] Sprites;

    private BoxCollider2D bc;
    private SpriteRenderer sr;

    void Awake()
    {
        bc = gameObject.GetComponent<BoxCollider2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        if (!AC.End_Game)
        {
            AC.Choose_Answer();
            StartCoroutine(Enabled());
        }
    }

    // прекрасно работает
    IEnumerator Enabled()
    {
        sr.sprite = Sprites[1];
        bc.enabled = false;
        yield return new WaitForSeconds(3.5f);
        sr.sprite = Sprites[0];
        bc.enabled = true;
    }
}
