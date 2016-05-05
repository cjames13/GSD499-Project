using UnityEngine;
using System.Collections;

public class MouseOrbit : MonoBehaviour
{
    public Transform Target;
    public float Distance = 5.0f;
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -20.0f;
    public float yMaxLimit = 80.0f;

    private float x;
    private float y;

    void Awake()
    {
		//if 3rd person: xMinLimit = 20, xMaxLimit = 20, distance = 4
		//if overhead: xminLimit = 35, xMaxLimit = 35 distance = 5.5
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;

        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (Target != null)
        {
			if (Mathf.Abs(Input.GetAxis ("Right Analog X")) > 0.02f) {
				x += (float)(Input.GetAxis ("Right Analog X") * xSpeed * 0.07f);
			} else {
				x += (float)(Input.GetAxis ("Mouse X") * xSpeed * 0.02f);
			}

            y -= (float)(Input.GetAxis("Mouse Y") * ySpeed * 0.02f);
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
			Vector3 position = rotation * (new Vector3(0.0f, 0.0f, -Distance)) + Target.position;
            transform.rotation = rotation;
            transform.position = position;
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
