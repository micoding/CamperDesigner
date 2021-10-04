using System;
using UnityEngine;
using UnityEngine.UI;

public class CamperCreator : MonoBehaviour
{
    public GameObject canvas;
    public GameObject VanBase;

    public GameObject frontWall;
    public GameObject rightWall;
    public GameObject backWall;
    public GameObject leftWall;

    public GameObject myPrefab;

    public void SetCamper()
    {
        SetCamper("", "");
    }
    public void SetCamper(string length, string width)
    {
        if (String.IsNullOrEmpty(length) || String.IsNullOrEmpty(width))
        {
            length = GameObject.Find("VehiceLength").GetComponent<InputField>().text;
            width = GameObject.Find("VehiceWidth").GetComponent<InputField>().text;
        }

        Debug.Log(length);
        Debug.Log(width);

        VanBase.transform.localScale = new Vector3(Convert.ToInt32(length), 1, Convert.ToInt32(width));
        VanBase.SetActive(true);

        frontWall.transform.localScale = new Vector3(Convert.ToInt32(length) * 10, 2000, 1);
        frontWall.transform.position = new Vector3(0, 1000, float.Parse(width) / -2 * 10);

        backWall.transform.localScale = new Vector3(Convert.ToInt32(length) * 10, 2000, 1);
        backWall.transform.localPosition = new Vector3(0, 1000, float.Parse(width) / 2 * 10);


        rightWall.transform.localScale = new Vector3(1, 2000, Convert.ToInt32(width) * 10);
        rightWall.transform.position = new Vector3(float.Parse(length) / 2 * 10, 1000, 0);

        leftWall.transform.localScale = new Vector3(1, 2000, Convert.ToInt32(width) * 10);
        leftWall.transform.position = new Vector3(float.Parse(length) / -2 * 10, 1000, 0);

        canvas.SetActive(false);
    }
}
