using UnityEngine;

public class Sc_move3 : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            Vector3 target = new Vector3(h, 0, v);
            target = GameObject.Find("Main Camera").transform.TransformDirection(target);
            target.y = 0f;

            Quaternion rot = Quaternion.LookRotation(target, Vector3.up);
            this.transform.rotation = rot;
        }

        Vector3 forward = Vector3.ProjectOnPlane(GameObject.Find("Main Camera").transform.forward, Vector3.up).normalized * v * Time.deltaTime * 5f;
        Vector3 right = Vector3.ProjectOnPlane(this.transform.right, Vector3.up).normalized * h * Time.deltaTime * 5f;
        this.transform.position += forward + right;
    }
}
