using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(player.transform.position, Vector3.up, -150 * Time.deltaTime);
            offset = transform.position - player.transform.position;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(player.transform.position, Vector3.up, 150 * Time.deltaTime);
            offset = transform.position - player.transform.position;
        }
    }
}