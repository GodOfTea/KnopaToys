using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class figur : MonoBehaviour
{
    public SoundManager sm;
    public Col_Pic_Controller CPC;
    public Animation Anim;

    public int Color;

    void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();
        Anim = gameObject.GetComponent<Animation>();
        CPC = GameObject.FindGameObjectWithTag("GameController").GetComponent<Col_Pic_Controller>();
    }

    void OnMouseDown()
    {
        if (Color == CPC.Color_Now)
        {
            sm.Play("Success");
            CPC.Colors_List[Color] -= 1;
            Anim.Play("Disappearance");
            StartCoroutine(Destroy());
        }
        else
        {
            sm.Play("Error");
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
