using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Refences")]

    public Rigidbody rb;
    public Transform head;
    public Camera camera;


    [Header("Configurations")]

    public float walkSpeed;
    public float runSpeed;

    [Header("Item Pickup")]
    public float interactRange = 2f;
    public LayerMask interactLayer;
    private PickableItem currentItem;

    private float baseWalkSpeed;
    private float baseRunSpeed;
    private bool isBoosting = false;
    private float boostTimer = 0f;



    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        baseWalkSpeed = walkSpeed;
        baseRunSpeed = runSpeed;
    }


    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);

        CheckForInteractables();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickUp();
        }

        if (isBoosting)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0f)
            {
                walkSpeed = baseWalkSpeed;
                runSpeed = baseRunSpeed;
                isBoosting = false;
                Debug.Log("Speed Boost Ended.");
                Debug.Log("Resetting to base speeds");
                Debug.Log($"Reset to Walk: {baseWalkSpeed}, Run: {baseRunSpeed}");

            }
        }
    }



    void FixedUpdate()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 moveDirection = transform.TransformDirection(input.normalized) * speed;

        Vector3 velocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);
        rb.linearVelocity = velocity;
    }


    void LateUpdate()
    {
        Vector3 e = head.eulerAngles;
        e.x -= Input.GetAxis("Mouse Y") * 2f;
        e.x = RestrictAngle(e.x, -85, 85);
        head.eulerAngles = e;


    }

    public static float  RestrictAngle(float angle, float angleMin, float angleMax)
    {
        if (angle > 180)
            angle -= 360;
        else if (angle < -180)
            angle += 360;

        if(angle > angleMax)
            angle = angleMax;
        if(angle < angleMin)
            angle = angleMin;

        return angle;
    }


    void CheckForInteractables()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, interactLayer))
        {
            currentItem = hit.collider.GetComponent<PickableItem>();
            if (currentItem != null)
            {
                Debug.Log("Looking at: " + currentItem.itemName);
            }
        }
        else
        {
            currentItem = null;
        }
    }

    void TryPickUp()
    {
        if (currentItem != null)
        {
            currentItem.Use(); 
            Destroy(currentItem.gameObject); 
            currentItem = null;
        }
    }

    public void ApplySpeedBoost(float boostAmount, float duration)
    {
        if (!isBoosting)
        {
            Debug.Log($"Before Boost - Walk: {walkSpeed}, Run: {runSpeed}");
            walkSpeed += boostAmount;
            runSpeed += boostAmount;
            Debug.Log($"After Boost - Walk: {walkSpeed}, Run: {runSpeed}");

            isBoosting = true;
            boostTimer = duration;
            Debug.Log("Speed boost applied!");
        }
    }

        


}
