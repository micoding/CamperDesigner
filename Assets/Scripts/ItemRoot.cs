using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRoot : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    public void OnMouseDown()
    {
        Camera camera = Camera.current;
        foreach (var cam in Camera.allCameras)
        {
            if (cam.isActiveAndEnabled)
                camera = cam;
        }


        screenPoint = camera.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    public void OnMouseDrag()
    {
        Camera camera = Camera.current;
        foreach (var cam in Camera.allCameras)
        {
            if (cam.isActiveAndEnabled)
                camera = cam;
        }

        float yAxleSave = transform.position.y;
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = camera.ScreenToWorldPoint(cursorPoint) + offset;
        cursorPosition.y = yAxleSave;
        transform.position = cursorPosition;
    }
}
