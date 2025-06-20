using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Refences")]

    public Rigidbody rb;
    public Transform head;
    public Camera camera;
    public float jumpPower = 7f;
    public float gravity = 10f;

    [Header("Configurations")]

    public float walkSpeed;
    public float runSpeed;
    public float jumpForce = 5f; 

    [Header("Item Pickup")]
    public float interactRange = 2f;
    public LayerMask interactLayer;
    private PickableItem currentItem;

    private float baseWalkSpeed;
    private float baseRunSpeed;
    private bool isBoosting = false;
    private float boostTimer = 0f;

    public Camera birdsEyeCamera;
    public LayerMask mazeCellLayer;




    void Start()
    {
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Escape from your room(Press E on window)");
        baseWalkSpeed = walkSpeed;
        baseRunSpeed = runSpeed;
    }


    void Update()
    {
        if (GameState.Instance.IsPaused) return;

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
        if (GameState.Instance.IsPaused) return;

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 moveDirection = transform.TransformDirection(input.normalized) * speed;

        Vector3 velocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);
        rb.linearVelocity = velocity;
    }


    void LateUpdate()
    {
        if (GameState.Instance.IsPaused) return;

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
        else
        {
            TryScaleWindow();
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

    public void ApplyStunAI(float duration)
    {
        StartCoroutine(StunAIRoutine(duration));
    }

    private IEnumerator StunAIRoutine(float duration)
    {
        GameState.Instance.StopAIStun = true;
        yield return new WaitForSeconds(duration);
        GameState.Instance.StopAIStun = false;
    }

    public void ApplyBirdsEye(float duration)
    {
        StartCoroutine(BirdsEyeRoutine(duration));
    }

    private IEnumerator BirdsEyeRoutine(float duration)
    {
        GameState.Instance.StopAIBirdsEye = true;
        GameState.Instance.IsPaused = true;

        camera.gameObject.SetActive(false);
        birdsEyeCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        birdsEyeCamera.gameObject.SetActive(false);
        camera.gameObject.SetActive(true);

        GameState.Instance.StopAIBirdsEye = false;
        GameState.Instance.IsPaused = false;
    }


    public void ApplyBreakWalls(Vector3 abilityPosition, float radius)
    {
        Collider[] nearbyCells = Physics.OverlapSphere(abilityPosition, radius, mazeCellLayer);

        Debug.Log("BreakWalls called");

        foreach (Collider cellCollider in nearbyCells)
        {
            MazeCell cell = cellCollider.GetComponent<MazeCell>();
            if (cell != null)
            {
                cell.ClearAllWalls();
            }
        }
    }


    void TryScaleWindow()
    {

        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, interactLayer))
        {
            OpenWindow window = hit.collider.GetComponent<OpenWindow>();
            if (window != null)
            {
                window.Open();
            }
        }

    }






}
