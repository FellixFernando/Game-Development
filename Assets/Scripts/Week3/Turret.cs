using System.Runtime.Serialization;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    GameObject target;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float fireRate = 4f;
    private float nextFireTime = 0f;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Enemy") {
            target = other.gameObject;
        }
    }
    void Update()
    {
        if(target!=null) {
            Vector3 posTarget = target.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.LookRotation(posTarget);

            if(Time.time >= nextFireTime) {
                Fire();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawnPoint.forward * 1000f);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag=="Enemy" && target == other.gameObject) {
            target = null;
        }
    }
}
