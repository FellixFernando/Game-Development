using UnityEngine;

public class BL_Hero : MonoBehaviour
{
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        this.transform.Translate(new Vector3(0, 0, v) * 5f * Time.deltaTime);
        this.transform.Rotate(new Vector3(0,h,0) * 1f);
    }
}
