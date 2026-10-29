using UnityEngine;
using UnityEngine.InputSystem;
public class LadyPirate : MonoBehaviour
{
    Vector2 moveInput;
    Animator anim;
    float comboAttack = 0f;
    float lastAttack = 0f;
    
    void OnMove(InputValue val)
    {
        moveInput = val.Get<Vector2>();
    }
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        float v = moveInput.y;
        float h = moveInput.x;

        if (h != 0 || v != 0)
        {
            anim.SetBool("isWalking", true);
            Vector3 target = new Vector3(h, 0, v);
            target = GameObject.Find("Main Camera").transform.TransformDirection(target);
            target.y = 0f;

            Quaternion rot = Quaternion.LookRotation(target, Vector3.up);
            this.transform.rotation = rot;
        }
        
        Vector3 forward = Vector3.ProjectOnPlane(GameObject.Find("Main Camera").transform.forward, Vector3.up).normalized * v * Time.deltaTime * 5f;
        Vector3 right = Vector3.ProjectOnPlane(this.transform.right, Vector3.up).normalized * h * Time.deltaTime * 5f;
        this.transform.position += forward + right;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            anim.SetBool("isJumping", true);
        }
        // else
        // {
        //     anim.SetBool("isJumping", false);
        // }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            lastAttack = Time.time;
            comboAttack++;
            comboAttack = Mathf.Clamp(comboAttack, 0f, 3);
            anim.SetFloat("attack", comboAttack);
        }
        if(Time.time - lastAttack >= 1){
            comboAttack = 0;
            anim.SetFloat("attack", comboAttack);
        }
    }
    public void resetJump()
    {
        anim.SetBool("isJumping", false);
    }
}
