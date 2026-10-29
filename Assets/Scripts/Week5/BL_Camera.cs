using UnityEngine;
using UnityEngine.InputSystem;

public class BL_Camera : MonoBehaviour
{
    GameObject hero;
    Vector3 offset;
    float pitchX; 
    void Start()
    {
        hero = GameObject.Find("Hero");
        this.transform.rotation = hero.transform.rotation;
        
        offset = hero.transform.forward * -5f;
        offset += hero.transform.up * 1f;

        pitchX = Mathf.Clamp(Mathf.DeltaAngle(0f, transform.eulerAngles.x), 0f, 70f);
    }
    
    void Update()
    {
        this.transform.position = hero.transform.position + offset;
            
        move_cam_by_mouse();    
    
        offset = transform.position - hero.transform.position;

        raycast();
    }


    void move_cam_by_mouse()
    {
        float speed = 2f;

        float mouseX = Mouse.current.delta.x.ReadValue() * Time.deltaTime * speed;
        float mouseY = Mouse.current.delta.y.ReadValue() * Time.deltaTime * speed;

        transform.RotateAround(hero.transform.position, Vector3.up, mouseX);

        float targetPitch = Mathf.Clamp(pitchX + mouseY, -20f, 70f);
        float pitchDelta = targetPitch - pitchX;
        pitchX = targetPitch;

        transform.RotateAround(hero.transform.position, transform.right, pitchDelta);

        transform.LookAt(hero.transform);
    }
    

    void raycast()
    {
        Vector3 heroPosition = hero.transform.position;
        Vector3 targetCameraPosition = heroPosition + offset;

        Vector3 direction = (targetCameraPosition - heroPosition).normalized;
        float targetDistance = Vector3.Distance(heroPosition, targetCameraPosition);

        float padding = 1f;
        float minDistance = 1f;

        RaycastHit hit;

        Ray ray = new Ray(heroPosition,direction);
        Debug.DrawRay(ray.origin, ray.direction * targetDistance, Color.red);
        //targetDistance bisa dibikin if()
        if (Physics.Raycast(ray, out hit, targetDistance))
        {
            Debug.Log(hit.transform.name);
            if (!hit.transform.IsChildOf(hero.transform))
        {
            float safeDistance = Mathf.Clamp(hit.distance - padding, minDistance, targetDistance);
            // float safeDistance = Mathf.Max(hit.distance - padding , 0.1f);
            transform.position = heroPosition + direction * safeDistance;
        }
        }
    }
}
