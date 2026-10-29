using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject preMusuh;
    [SerializeField] GameObject spawnMusuh;
    int spawnTime = 100;
    void Start()
    {

    }

    void Update()
    {
        spawnTime--;
        if (spawnTime == 0)
        {
            Vector3 offset = new Vector3(0, 1f, 0);
            Instantiate(preMusuh, spawnMusuh.transform.position + offset , spawnMusuh.transform.rotation);
            spawnTime = 100;
        }
    }
}
