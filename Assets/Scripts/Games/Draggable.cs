using UnityEngine;


/// <summary>
/// Перемещение объекта по нажатию
/// </summary>
public class Draggable : MonoBehaviour
{
    public GameObject movement_object; // объект перемещения

    public bool is_Dragging; // объект находиться в движении
    public bool is_Up;
    private float z_distance = 1f; // - брать начальное положение Vector3


    void Start()
    {
        movement_object = gameObject; // объект перемещения, этот же объект
    }
    void OnMouseDown()
    {
        is_Dragging = true;
    }

    void OnMouseUp()
    {
        is_Dragging = false;
        is_Up = true;
    }

    void Update()
    {
        if (is_Dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, z_distance));
        }
    }
}
