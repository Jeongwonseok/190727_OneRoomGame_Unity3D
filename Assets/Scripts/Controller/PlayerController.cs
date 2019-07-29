using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Transform tf_Crosshair;

    [SerializeField] Transform tf_Cam;

    [SerializeField] float sightSensivitity; // 고개의 움직임 속도
    [SerializeField] float lookLimitX;
    [SerializeField] float lookLimitY;
    float currentAngleX;
    float currentAngleY;


    // Update is called once per frame
    void Update()
    {
        CrosshairMoving();
        ViewMoving();
        
    }

    void ViewMoving()
    {
        if (tf_Crosshair.localPosition.x > (Screen.width / 2 - 100) || tf_Crosshair.localPosition.x < (-Screen.width / 2 + 100))
        {
            currentAngleY += (tf_Crosshair.localPosition.x > 0) ? sightSensivitity : -sightSensivitity;
            currentAngleY = Mathf.Clamp(currentAngleY, -lookLimitX, lookLimitX);
            
        }

        if (tf_Crosshair.localPosition.y > (Screen.height / 2 - 100) || tf_Crosshair.localPosition.y < (-Screen.height / 2 + 100))
        {
            currentAngleX += (tf_Crosshair.localPosition.y > 0) ? -sightSensivitity : sightSensivitity;
            currentAngleX = Mathf.Clamp(currentAngleX, -lookLimitY, lookLimitY);

        }

        tf_Cam.localEulerAngles = new Vector3(currentAngleX, currentAngleY, tf_Cam.localEulerAngles.z);
    }

    void CrosshairMoving()
    {
        tf_Crosshair.localPosition = new Vector2(Input.mousePosition.x - (Screen.width / 2),
                                                 Input.mousePosition.y - (Screen.height / 2));

        float t_cursorPosX = tf_Crosshair.localPosition.x;
        float t_cursorPosY = tf_Crosshair.localPosition.y;

        t_cursorPosX = Mathf.Clamp(t_cursorPosX, (-Screen.width / 2 + 50), (Screen.width / 2 - 50));
        t_cursorPosY = Mathf.Clamp(t_cursorPosY, (-Screen.height / 2 + 50), (Screen.height / 2 - 50));

        tf_Crosshair.localPosition = new Vector2(t_cursorPosX, t_cursorPosY);
    }
}
