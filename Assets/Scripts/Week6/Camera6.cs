using UnityEngine;

public class Camera6 : MonoBehaviour
{
    private float turnSpeed = 3f;
    private Transform hero;
    private Vector3 offset;

    void Start()
    {
        hero = GameObject.Find("Lady Pirate").transform;
        offset = new Vector3(0f, 2f, 5f);
    }

    void Update()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        this.transform.position = hero.position + offset;
        this.transform.LookAt(hero);
    }
}
