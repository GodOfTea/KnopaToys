using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bubble : MonoBehaviour
{
    //---ПЕРЕМЕННЫЕ---
    public SoundManager              SM;
    [SerializeField] private Vector2 Direction;
    [SerializeField] private float   Speed;
    //---ПЕРЕМЕННЫЕ---

    void Start()
    {
        SM = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();
        Direction.x = Random.Range(-0.5f, 0.51f);
        StartCoroutine(Delete_Timer());
    }

    void FixedUpdate()
    {
        transform.Translate(Speed * Direction, Space.World);
    }

    void OnMouseDown()
    {
        SM.Play("Destroy");
        Destroy(gameObject);
    }

    IEnumerator Delete_Timer()
    {
        yield return new WaitForSeconds(7f);
        if(gameObject != null)
            Destroy(gameObject);
    }

    //СТАРЫЙ КОД (новый лучше и меньше)
    /*
    public float Push_Force;

    //public Bubbles_Controller BC;
    public Spawn_Size_controller SC;
    public SoundManager SM;
    public Rigidbody2D RB2D;

    //public Sprite[] Pictures;

    void Awake()
    {
        SM = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();
    }

    public void Spawn()
    {
        //SC = GameObject.FindGameObjectWithTag("GameController").GetComponent<Spawn_Size_controller>();
        Vector3 pos = SC.Center + new Vector3(Random.Range(-SC.x, SC.x), Random.Range(-SC.y, SC.y), SC.Center.z);
        Instantiate(gameObject, pos, Quaternion.identity);
        //Push();
    }

    //public void Push()
    //{
    //    //RB2D.AddForce(Vector2.down * Push_Force);
    //    RB2D.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Push_Force;
    //}

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        //BC.Bubbles_Count -= 1;
        SM.Play("Destroy");
        Destroy(gameObject);
        //StartCoroutine(Destroy());
    }
    */
}
