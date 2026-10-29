using UnityEngine;

public class Capsule_8 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        this.transform.Translate(new Vector3(v, 0, 0) * Time.deltaTime * 5f);
        this.transform.Rotate(new Vector3(0, h, 0) * 1f);
    }
}
