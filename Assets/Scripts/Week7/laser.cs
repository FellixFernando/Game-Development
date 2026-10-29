using UnityEngine;
public class laser : MonoBehaviour
{
    private LineRenderer lr;

    void Start()
    {
        lr = this.GetComponent<LineRenderer>();

    }

    void Update()
    {

        lr.SetPosition(0, this.transform.position);

        RaycastHit hit;
        if(Physics.Raycast(this.transform.position, this.transform.forward, out hit))
        {
            if(hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else
        {
            lr.SetPosition(1, this.transform.position + this.transform.forward * 100f);
        }
    }
}