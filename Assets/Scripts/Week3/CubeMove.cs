using UnityEngine;

public class CubeMove : MonoBehaviour
{
    int arah = 1;
    float speed = 3f;

    void Update()
    {
        if (arah == 1)
        {
            this.transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (this.transform.position.x >= 5f)
            {
                arah = 2;
            }
        }
        else if (arah == 2)
        {
            this.transform.Translate(Vector3.up * speed * Time.deltaTime);

            if (this.transform.position.y >= 5f)
            {
                arah = 3;
            }
        }
        else if (arah == 3)
        {
            this.transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (this.transform.position.x <= -5f)
            {
                arah = 4;
            }
        }
        else if (arah == 4)
        {
            this.transform.Translate(Vector3.down * speed * Time.deltaTime);

            if (this.transform.position.y <= -5f)
            {
                arah = 1;
            }
        }
    }
}
