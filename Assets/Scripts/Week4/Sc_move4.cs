using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_move4 : MonoBehaviour
{
    private Vector2 moveInput;
    float rotY;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

    }

    void Start()
    {

    }

    void Update()
    {
        float v = moveInput.y;
        float h = moveInput.x;
        this.transform.Translate(new Vector3(h,0,v) * Time.deltaTime * 3f);

        float rotX = this.transform.localEulerAngles.y + Mouse.current.delta.x.ReadValue() *3f;
        this.transform.localEulerAngles = new Vector3(0, rotX, 0);

        rotY += GameObject.Find("Main Camera").transform.localEulerAngles.y + Mouse.current.delta.y.ReadValue() * 2f;
        rotY = Mathf.Clamp(rotY, -60, 60);
        GameObject.Find("Main Camera").transform.localEulerAngles = new Vector3(rotY, 0 , 0);
        //Camera.main.transform.localEulerAngles = new Vector3(rotY, 0 , 0);
    }
}
