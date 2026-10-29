using UnityEngine;
using UnityEngine.InputSystem;
public class hero : MonoBehaviour
{
    Vector2 moveInput;
    Animator anim;

    Transform tubuhChar;
    Transform targetLook = null;

    [SerializeField] private LineRenderer laser;

    [Range(0,1f)] public float distanceToGround;
    public LayerMask layermask;
    [SerializeField] private float maxNeckYaw = 60f;
    [SerializeField] private float maxNeckPitch = 30f;
    private void OnMove(InputValue val) {
        moveInput = val.Get<Vector2>();
    }

    void Start(){
        anim = this.GetComponent<Animator>();
        tubuhChar = this.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Neck);

        laser.enabled = false;
    }

    void Update()
    {
        float h = moveInput.x;
        float v = moveInput.y;

        if(h!=0 || v!= 0)
        {
        Vector3 arah = new Vector3(h, 0, v);
            arah = UnityEngine.Camera.main.transform.TransformDirection(arah);
            arah.y = 0;
            this.transform.rotation = Quaternion.LookRotation(arah, Vector3.up);
            
            anim.SetBool("isWalk",true);
            this.transform.position += Vector3.ProjectOnPlane(UnityEngine.Camera.main.transform.forward, Vector3.up) * v * Time.deltaTime;
            this.transform.position += Vector3.ProjectOnPlane(UnityEngine.Camera.main.transform.right, Vector3.up) * h * Time.deltaTime;
        }
        else
        {
            anim.SetBool("isWalk",false);
        }

        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            anim.SetBool("isAim", true);
            laser.enabled = true;
        }
        if(Mouse.current.rightButton.wasReleasedThisFrame)
        {
            anim.SetBool("isAim", false);
            laser.enabled = false;
        }

        if (targetLook != null)
{
    Vector3 direction = targetLook.position - tubuhChar.position;

    Vector3 localDirection = transform.InverseTransformDirection(direction);

    float yaw = Mathf.Atan2(localDirection.x, localDirection.z) * Mathf.Rad2Deg;
    float pitch = -Mathf.Atan2(
        localDirection.y,
        new Vector2(localDirection.x, localDirection.z).magnitude
    ) * Mathf.Rad2Deg;

    yaw = Mathf.Clamp(yaw, -maxNeckYaw, maxNeckYaw);
    pitch = Mathf.Clamp(pitch, -maxNeckPitch, maxNeckPitch);

    Quaternion neckRotation =
        Quaternion.Euler(pitch, yaw, 0f);

    Vector3 lookDirection =
        transform.rotation * neckRotation * Vector3.forward;

    tubuhChar.rotation = Quaternion.LookRotation(
        lookDirection,
        transform.up
    );
}
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter: " + other.gameObject.tag);
        if(other.gameObject.tag == "item")
        {
            targetLook = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        targetLook = null;
    }

    void LateUpdate()
    {
        if(targetLook != null)
        {
            tubuhChar.LookAt(targetLook.position);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        // Left foot
        SetFootIK(AvatarIKGoal.LeftFoot);

        // Right foot
        SetFootIK(AvatarIKGoal.RightFoot);
    }


    private void SetFootIK(AvatarIKGoal foot)
{
    anim.SetIKPositionWeight(foot, 1);
    anim.SetIKRotationWeight(foot, 1);

    RaycastHit hit;

    Ray ray = new Ray(
        anim.GetIKPosition(foot) + Vector3.up,
        Vector3.down
    );

    if (Physics.Raycast(ray, out hit, distanceToGround + 1f, layermask))
    {
        if (hit.transform.CompareTag("walkable"))
        {
            Vector3 footPosition = hit.point;
            footPosition.y += distanceToGround;

            anim.SetIKPosition(foot, footPosition);

            anim.SetIKRotation(
                foot,
                Quaternion.LookRotation(transform.forward, hit.normal)
            );
        }
    }
}
}
