using UnityEngine;

public class Camera : MonoBehaviour
{
    private float turnSpeed = 3f;
    private Transform hero;
    private Vector3 offset;

    void Start()
    {
        hero = GameObject.Find("Player").transform;
        offset = new Vector3(hero.position.x, hero.position.y, hero.position.z);
    }

    void Update()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        this.transform.position = hero.position + offset;
        this.transform.LookAt(hero);
    }
}
