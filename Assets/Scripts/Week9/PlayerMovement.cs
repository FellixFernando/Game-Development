using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public Image health_bar;

    void Start()
    {
        health_bar = GameObject.Find("health_bar").GetComponent<Image>();
        health_bar.fillAmount = 1f; 
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        this.transform.Translate(new Vector3(v, 0, 0) * 5f * Time.deltaTime);
        this.transform.Rotate(new Vector3(0, h, 0) * 0.7f);

        // Update the health bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            health_bar.fillAmount -= 0.1f; 
        }

        if(health_bar.fillAmount <= 0f)
        {
            Destroy(this.gameObject);
        }
        
    }
}
