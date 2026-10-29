using UnityEngine;
using UnityEngine.InputSystem;
public class Sc_Sphere : MonoBehaviour
{
    float speed = 100f;
    GameObject hero;
    Vector3 offset;
    void Start()
    {
        hero = GameObject.Find("Hero");
        offset = new Vector3(hero.transform.position.x, hero.transform.position.y+1f, hero.transform.position.z-2f);
        
    }

    void Update()
    {
        offset = Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.up) * offset;
        // offset = Quaternion.AngleAxis(Mouse.current.delta.x.ReadValue() * speed * Time.deltaTime, Vector3.up) * offset;
        this.transform.position = hero.transform.position + offset;
        
    }
}
