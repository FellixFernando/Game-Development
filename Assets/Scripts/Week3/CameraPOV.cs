using UnityEngine;

public class CameraPOV : MonoBehaviour
{
    GameObject target;
    void Start()
    {
        target = GameObject.Find("hero");
    }

    void Update()
    {
        float speed = 0f;
        float zoomRate = 0f;


        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 200;
        }
        if (Input.GetKey(KeyCode.RightShift))
        {
            speed = -200;
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            zoomRate = 50;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            zoomRate = -50;
        }


        //TODO make camera able to zoom in (1) and out (2)
        this.transform.LookAt(target.transform);
        this.transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
        this.transform.Translate(new Vector3(0,0,zoomRate) * Time.deltaTime);
    }
}
