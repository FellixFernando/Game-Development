using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections.Generic;

public class HeroMovement : MonoBehaviour
{
    List<GameObject> enemies = new List<GameObject>();
    GameObject targetMusuh;
    int idx = 0;
    void Start()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 45, 0));
        Debug.Log("Rotasi Y Hero: " + this.transform.rotation.eulerAngles.y);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter: " + other.name);
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Triggered");
            enemies.Add(other.gameObject);
            pindah_target();
            // Destroy(other.gameObject);
        }
        Debug.Log("Kill Count: " + enemies.Count);

    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    void pindah_target()
    {
        if (enemies.Count > 0)
        {
            targetMusuh = enemies[idx];
        }
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float j = Input.GetAxis("Jump");
        this.transform.Translate(new Vector3(0, 0, v) * Time.deltaTime * 3f);
        this.transform.Translate(new Vector3(h, 0, 0) * Time.deltaTime * 3f);
        // this.transform.Rotate(new Vector3(0, h, 0) * 5f);
        this.transform.Translate(new Vector3(0, j, 0) * Time.deltaTime * 3f);

        if (targetMusuh != null)
        {
            Vector3 arah = targetMusuh.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.LookRotation(arah);
        }
        if(Input.GetMouseButtonDown(0))
        {
            idx++;
            if (idx >= enemies.Count)
            {
                idx = 0;
            }
            pindah_target();
        }
    }
}
