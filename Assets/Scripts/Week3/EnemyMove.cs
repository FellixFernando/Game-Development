using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    bool gerakKanan = false;
    float posAwalX;
    float jarakGerakMusuh = 4f;
    
    void Start()
    {
        posAwalX = this.transform.position.x;
    }
    void Update()
    {
        if (gerakKanan == false)
        {
            this.transform.Translate(new Vector3(3f, 0, 0) * Time.deltaTime);
            if (this.transform.position.x > posAwalX + jarakGerakMusuh)
            {
                gerakKanan = true;
            }
        }
        else
        {
            this.transform.Translate(new Vector3(-3f, 0, 0) * Time.deltaTime);
            if (this.transform.position.x < posAwalX - jarakGerakMusuh)
            {
                gerakKanan = false;
            }
        }
    }
}
