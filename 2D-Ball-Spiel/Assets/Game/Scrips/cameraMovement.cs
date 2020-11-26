using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class cameraMovement : MonoBehaviour
{
    //public GameObject roomPickHander;
    //public roomPicker picker;


    Vector3 touchStart;
    public Vector3 direction;
    public Vector3 newCameraPos;
    public Vector3 oldCameraPos;

    public float minZoom = 1;
    public float maxZoom = 8;
    public float zoomSmooth;

    public float minY;
    public float maxY;

    public float minX;
    public float maxX;

    public float cameraZPosition;

    Touch touchZero;
    Touch touchOne;

    public bool touched = false;
    public bool cameraMoves = false;

    // Start is called before the first frame update
    void Start()
    {
        //roomPickHander = GameObject.Find("ChooseRoom");
        //picker = roomPickHander.GetComponent<roomPicker>();

        //keeps screen in porttait mode
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void LateUpdate()
    {
        //keeps camera inside the boundarys
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            Mathf.Clamp(transform.position.z, cameraZPosition, cameraZPosition)
        );

        if (newCameraPos == oldCameraPos)
        {
            cameraMoves = false;
        }
        else if (newCameraPos != oldCameraPos)
        {
            cameraMoves = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        oldCameraPos = Camera.main.transform.position;

        //checks where the finger ist placed first
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            newCameraPos = oldCameraPos;
        }

        // checks if 2 fingers are placed and enables zoom
        if (Input.touchCount == 2)// && picker.roomSelected == false)
        {
            touchZero = Input.GetTouch(0);
            touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPosition = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPosition = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPosition - touchOnePrevPosition).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float differenceMag = currentMagnitude - prevMagnitude;

            Zoom(differenceMag * zoomSmooth);
            touched = true;
        }
        else if (Input.touchCount == 0)
        {
            //if 2 fingers where placed, both finger need to come off the screen to enable paning
            touched = false;
        }

        //callculates the pan distance
        if (Input.GetMouseButton(0) && touched == false)// && picker.roomSelected == false)
        {

            direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }

        Zoom(Input.GetAxis("Mouse ScrollWheel"));

    }

    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, minZoom, maxZoom);
    }

}
