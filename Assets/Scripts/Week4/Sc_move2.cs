using UnityEngine;

public class Sc_move2 : MonoBehaviour
{
    private float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float j = Input.GetAxis("Jump");
        if (h != 0 || v != 0 || j != 0)
        {
            this.transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
            if (j != 0)
            {
                this.transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            Vector3 maju = new Vector3(-h, 0, v);
            maju = this.transform.TransformDirection(maju);
            this.transform.Translate(maju * speed * Time.deltaTime);
        }
    }
}
