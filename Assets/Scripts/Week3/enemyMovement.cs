using UnityEngine;
using System.Collections.Generic;

public class enemyMovement : MonoBehaviour
{
    GameObject movepoint;
    List<GameObject> listPoint = new List<GameObject>();
    float speedMove = 5f;
    void Awake()
    {
    movepoint = GameObject.Find("movepoint");
    //save semua child point pada movepoint di array list
    foreach (Transform child in movepoint.transform) {
    listPoint.Add(child.gameObject);
    }
    //lihat isi list
    foreach (GameObject x in listPoint) {
    Debug.Log("list: " + x.ToString());
    }
    }
    void Update()
    {
      
        if (listPoint.Count > 0)
        {
            //modifikasi moveTowards, gerakan musuh ke point, hanya gerakan axis x dan z
            Vector3 targetPos = new Vector3(listPoint[0].transform.position.x, this.transform.position.y,
            listPoint[0].transform.position.z);
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speedMove *
            Time.deltaTime);

            //hadap musuh
            Vector3 posPoint = targetPos - this.transform.position;
            this.transform.rotation = Quaternion.LookRotation(posPoint);

            //modifikasi Vector3.distance hanya melihat posisi x dan z, apakah sudah sampai tujuan
            Vector3 posMusuh = this.transform.position;
            posMusuh.y = listPoint[0].transform.position.y;
            if (Vector3.Distance(listPoint[0].transform.position, posMusuh) < 0.1f)
            {
                listPoint.RemoveAt(0);
            }
        }
        else
        {
            Debug.Log("musuh sampai tujuan");
            Destroy(this.gameObject);
        }
    }
}
