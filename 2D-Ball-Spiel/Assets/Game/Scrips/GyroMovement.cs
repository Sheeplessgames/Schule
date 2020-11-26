using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroMovement : MonoBehaviour
{
    Rigidbody rigid;
    Gyroscope gyros;
    float horizontal;
    float vertical;


    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        rigid = GetComponent<Rigidbody>();
        gyros = Input.gyro;
        gyros.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        horizontal += -gyros.rotationRateUnbiased.x;
        vertical += gyros.rotationRateUnbiased.y;

        Vector3 movement = new Vector3(vertical, 0, horizontal);

        rigid.AddForce(movement * speed);

    }

    private void OnGUI()
    {
        //Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        GUI.Label(new Rect(500, 300, 400, 80), "Gyro rotation rate " + gyros.rotationRate);
        GUI.Label(new Rect(500, 350, 400, 80), "Gyro attitude" + gyros.attitude);
        GUI.Label(new Rect(500, 400, 400, 80), "Gyro enabled : " + gyros.enabled);
    }
}
