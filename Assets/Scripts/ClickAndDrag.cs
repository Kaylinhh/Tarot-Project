using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 oldPos;
    private bool dragging = false;

    void Start()
    {
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void Update()
    {
        if (dragging)
        {
            //Move object, taking into account original offest
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        //Record the difference between the objects centre, and the clicked point on the camera plane.
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Get the position before dragging
        oldPos = transform.position;
        dragging = true;
    }

    private void OnMouseUp()
    {
        //stop dragging
        dragging = false;
        //Go back to old pos
        transform.position = oldPos;
    }

}
