using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingHook : MonoBehaviour
{
    [Header("Kanca Ayarlari")]
    public LayerMask grappleableLayer;
    public float maxGrappleDistance = 40f;
    public float reelSpeed = 12f; 

    [Header("Firlama Ayarlari")]
    [Range(0f, 1f)]
    public float verticalLaunchScale = 0.8f; 
    
    private Vector3 grapplePoint;
    private SpringJoint joint;
    private bool isGrappling = false;
    private float initialGrappleDistance; 

    [Header("Bilesenler")]
    public Transform cameraTransform;
    public Transform gunTip;
    public LineRenderer lineRenderer;

    [Header("Ses Ayarlari")]
    public AudioSource grappleAudioSource;
    
    private PlayerController playerController;
    private Rigidbody rb;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartGrapple();
        }
        
        if (Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            StopGrapple();
        }

        if (isGrappling && joint != null && Keyboard.current != null)
        {
            if (Keyboard.current.spaceKey.isPressed && !playerController.isGrounded)
            {
                joint.maxDistance -= reelSpeed * Time.deltaTime;
            }
            
            if (Keyboard.current.leftCtrlKey.isPressed)
            {
                joint.maxDistance += reelSpeed * Time.deltaTime;
                if (joint.maxDistance > initialGrappleDistance)
                {
                    joint.maxDistance = initialGrappleDistance;
                }
            }
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxGrappleDistance, grappleableLayer))
        {
            grapplePoint = hit.point;
            isGrappling = true;
            
            playerController.isGrappling = true;

            joint = gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            initialGrappleDistance = Vector3.Distance(transform.position, grapplePoint);

            joint.maxDistance = initialGrappleDistance;
            joint.minDistance = 0f;
            joint.spring = 400f; 
            joint.damper = 10f;  
            joint.massScale = 4.5f;

            lineRenderer.positionCount = 2;

            if (grappleAudioSource != null)
            {
                // Sese rastgele hafif bir incelik/kalınlık katarak tekrara düşmesini engelliyoruz
                grappleAudioSource.pitch = Random.Range(0.9f, 1.1f);
                grappleAudioSource.Play();
            }
        }
    }

    public void StopGrapple()
    {
        if (!isGrappling) return;

        isGrappling = false;
        
        Vector3 velocity = rb.linearVelocity;
        velocity.y *= verticalLaunchScale;
        rb.linearVelocity = velocity;

        if (playerController != null)
        {
            playerController.isGrappling = false;
            playerController.StartMomentum(); 
        }
        
        lineRenderer.positionCount = 0;
        Destroy(joint); 
    }

    void DrawRope()
    {
        if (!isGrappling) return;
        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }
}