using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_Camera : MonoBehaviour
{
    GameObject hero;
    void Start()
    {
        hero = GameObject.Find("Hero");

        this.transform.rotation = hero.transform.rotation;

        Vector3 camPosition = hero.transform.localPosition;
        camPosition -= this.transform.forward * 5f;
        this.transform.position = camPosition;
        this.transform.LookAt(hero.transform);
    }

    void Update()
    {
        MoveCameraWithMouse();
    }

    void MoveCameraWithMouse() {
        float speed = 5f;

        this.transform.RotateAround(hero.transform.position, Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * speed);

        float RotasiY = Input.GetAxis("Mouse Y");
        float currentRotasiX = this.transform.localEulerAngles.x;

        if (currentRotasiX > 180)
        {
            currentRotasiX -= 360;
        }

        float newRotasiX = currentRotasiX + RotasiY;
        newRotasiX = Mathf.Clamp(newRotasiX, 0f, 70f);
        float rotationDifference = newRotasiX - currentRotasiX;

        this.transform.RotateAround(hero.transform.position, Vector3.left, rotationDifference);
        this.transform.LookAt(hero.transform);
        // this.transform.rotation = Quaternion.LookRotation(hero.transform.position - this.transform.position);
    }

    void MoveCameraWithKeys() {
        float speed = 0f;

        if (Input.GetKey(KeyCode.LeftShift))
        {

            speed = 50;
        }
        else
        {
            speed = 0f;
        }

        if (Input.GetKey(KeyCode.RightShift))
        {
            speed = -50;
        }

        // this.transform.LookAt(hero.transform);
        this.transform.rotation = Quaternion.LookRotation(hero.transform.position - this.transform.position);

        this.transform.RotateAround(hero.transform.position, Vector3.up, speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (Vector3.Distance(this.transform.position, hero.transform.position) > 2f)
            {
                this.transform.Translate(Vector3.forward * Time.deltaTime, Space.Self);
            }
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (Vector3.Distance(this.transform.position, hero.transform.position) < 10f)
            {
                this.transform.Translate(Vector3.back * Time.deltaTime, Space.Self);
            }
        }
    }
}
