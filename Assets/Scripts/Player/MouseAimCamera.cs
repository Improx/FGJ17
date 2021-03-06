using UnityEngine;
using System.Collections;

public class MouseAimCamera : MonoBehaviour
{
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;

    public float viewRange = 60.0f;
    float rotationY = 0F;

    public GameObject Head;

    private static MouseAimCamera instance;
    public static MouseAimCamera Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<MouseAimCamera>();
            }

            return instance;
        }
    }

    //private Transform cameraTransform;
    void Start()
    {
        //cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update() {
        float rotationX = Head.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
        if (rotationX < 180 && rotationX > 0) {
            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
        }
        //Change from hard coded values
        if (rotationX > 180) {
            rotationX = Mathf.Clamp(rotationX, 270, 360);
        }
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        Head.transform.localEulerAngles = new Vector3(0, rotationX, rotationY);
        
    }
}