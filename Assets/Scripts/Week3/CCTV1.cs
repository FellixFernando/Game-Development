using UnityEngine;

public class CCTV1 : MonoBehaviour
{
    private GameObject target;

    void Start()
    {
        target = GameObject.Find("hero");
    }

    void Update()
    {
        Vector3 arah = target.transform.position - this.transform.position;
        Quaternion rot = Quaternion.LookRotation(arah);
        this.transform.rotation = Quaternion.Euler(new Vector3(0, rot.eulerAngles.y, 0));
        Debug.DrawRay(this.transform.position, arah, Color.red);

        //CCTV2s
        float angle = Mathf.Atan2(arah.x, arah.z) * Mathf.Rad2Deg;
        Quaternion rotasi = Quaternion.AngleAxis(angle, Vector3.up);
        // this.transform.rotation = rotasi;

        // this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotasi, Time.deltaTime);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotasi, Time.deltaTime);
    }
}
