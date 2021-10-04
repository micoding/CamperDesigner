using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Camera rightCamera;
    public Camera backCamera;
    public Camera leftCamera;
    public Camera upCamera;

    public GameObject frontWall;
    public GameObject rightWall;
    public GameObject backWall;
    public GameObject leftWall;

    private int cameraAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
        rightCamera.enabled = false;
        backCamera.enabled = false;
        leftCamera.enabled = false;
        upCamera.enabled = false;
    }

    public void ChangeCameraAngle()
    {
        switch (cameraAngle)
        {
            case 0:
                mainCamera.enabled = false;
                rightCamera.enabled = true;

                rightWall.GetComponent<MeshRenderer>().enabled = false;
                frontWall.GetComponent<MeshRenderer>().enabled = true;
                cameraAngle++;
                break;
            case 1:
                rightCamera.enabled = false;
                backCamera.enabled = true;

                backWall.GetComponent<MeshRenderer>().enabled = false;
                rightWall.GetComponent<MeshRenderer>().enabled = true;
                cameraAngle++;
                break;
            case 2:
                backCamera.enabled = false;
                leftCamera.enabled = true;

                leftWall.GetComponent<MeshRenderer>().enabled = false;
                backWall.GetComponent<MeshRenderer>().enabled = true;
                cameraAngle++;
                break;
            case 3:
                leftCamera.enabled = false;
                upCamera.enabled = true;

                leftWall.GetComponent<MeshRenderer>().enabled = true;
                cameraAngle++;
                break;
            case 4:
                upCamera.enabled = false;
                mainCamera.enabled = true;

                frontWall.GetComponent<MeshRenderer>().enabled = false;
                cameraAngle = 0;
                break;
        }
    }
}
