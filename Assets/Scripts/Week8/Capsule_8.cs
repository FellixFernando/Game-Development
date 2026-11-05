
using UnityEngine;

public class Capsule_8 : MonoBehaviour
{
    GameObject objCamera;

    void Start()
    {
        objCamera = this.transform.Find("Main Camera").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "areaMusuh")
        {
            objCamera.SendMessage("rubahMusik1");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "areaMusuh")
        {
            objCamera.SendMessage("rubahMusik2");
        }
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        bool isJalan = v != 0;

        if (isJalan)
        {
            this.transform.Translate(
                new Vector3(v, 0, 0) * Time.deltaTime * 4f
            );

            objCamera.SendMessage("suaraBerjalan");
        }
        else
        {
            objCamera.SendMessage("berhentiJalan");
        }

        this.transform.Rotate(new Vector3(0, h, 0) * 2f);
    }
}
