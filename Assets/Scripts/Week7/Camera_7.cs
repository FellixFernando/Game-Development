using UnityEngine;

public class Camera_7 : MonoBehaviour
{
    [SerializeField] private Transform target;
    Vector3 offset;

    void Start()
    {
        if (target == null)
        {
            GameObject hero = GameObject.Find("Hero");
            if (hero == null)
            {
                Debug.LogError("Camera_7 could not find a GameObject named 'Hero'.", this);
                enabled = false;
                return;
            }

            target = hero.transform.Find("Selfpoint");
            if (target == null)
            {
                target = hero.transform;
                Debug.LogWarning("Camera_7 could not find 'Selfpoint' under 'Hero'; using the Hero transform.", this);
            }
        }

        offset = new Vector3(target.localPosition.x,target.localPosition.y-1f,target.localPosition.z-2f);
    }

    void Update()
    {
        if (target == null)
        {
            Debug.LogError("Camera_7 target was destroyed or unassigned.", this);
            enabled = false;
            return;
        }

        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4f, Vector3.up) * offset;
        this.transform.position = target.position + offset;
        transform.LookAt(target.position);
    }
}
