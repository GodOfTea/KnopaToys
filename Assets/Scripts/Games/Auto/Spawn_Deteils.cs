using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Deteils : MonoBehaviour
{
    [SerializeField]
    List<Vector3> Five_Deteils;
    [SerializeField]
    List<Vector3> Six_Deteils;
    [SerializeField]
    List<Vector3> Seven_Deteils;

    [SerializeField]
    List<Detail_Pos> Details_Pos;


    public void Spawn(Sprite[] details)
    {
        int len = details.Length;
        var pos_list = (len == 6) ? Six_Deteils : ((len == 5) ? Five_Deteils : Seven_Deteils);

        if (Details_Pos.Count > len)
        {
            Details_Pos.RemoveRange(len, Details_Pos.Count - len);
        }

        for (int i = 0; i < len; i++)
        {
            Details_Pos[i].Box_Col.enabled = true;
            Details_Pos[i]._pos = pos_list[i];
            Details_Pos[i].transform.position = pos_list[i];
            Details_Pos[i].Sprite_Ren.sprite = details[i];
            Details_Pos[i].transform.rotation = Quaternion.Euler(0, 0, Random.Range(-25f, 25f));
        }
        
    }
}
