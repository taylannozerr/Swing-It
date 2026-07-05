using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; 
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour
{
    [Header("Hareket Ayarlari")]
    public float moveSpeed = 8f;
    public float airControlForce = 15f; 
    private Rigidbody rb;
    private Vector2 moveInput;

    [Header("Kamera Ayarlari")]
    public Transform playerCamera;
    public float mouseSensitivity = 0.08f;
    private float xRotation = 0f;

    [Header("Ziplama Ayarlari")]
    public float jumpForce = 6f;

    [Header("Momentum Ayarlari")]
    public float momentumRetainTime = 3f; 
    private float momentumTimer; 

    [Header("Surtunme Ayarlari")]
    public float groundFrictionSpeed = 4f;

    [Header("Respawn Ayarlari")]
    public Transform spawnPoint; 

    [Header("Arayüz (UI) Ayarlari")]
    public TextMeshProUGUI timerText; 
    public GameObject winPanel; // Kazanma Ekranı Paneli

    [Header("Ses Ayarlari (Player)")]
    public AudioSource deathAudioSource; // Ölüm/Yanma Sesi Çalar

    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isGrappling = false;

    // Çift Sayaç Sistemi
    private float totalLevelTimer = 0f;   // Bölüme girdiğinden beri geçen toplam süre
    private float currentRunTimer = 0f;   // Son doğduğundan beri geçen süre
    private bool isFinished = false;

    private GrapplingHook grapplingHook;
    private AudioSource audioSource; // Kutlama Sesi Çalar 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grapplingHook = GetComponent<GrapplingHook>();
        
        // Zafer ses bileşenini otomatik bul
        audioSource = GetComponent<AudioSource>();

        // Oyun başında kazanma paneli kapalı 
        if (winPanel != null) winPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb.freezeRotation = true;
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    void Update()
    {
        // ESC Tuşuna basıldığında Ana Menüye dön 
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("MainMenu"); 
            return; 
        }

        if (!isFinished)
        {
            totalLevelTimer += Time.deltaTime;
            currentRunTimer += Time.deltaTime;
            
            if (timerText != null)
            {
                timerText.text = "Toplam Süre: " + totalLevelTimer.ToString("F2") + "\n" +
                                 "Mevcut Deneme: " + currentRunTimer.ToString("F2");
            }
        }

        if (Mouse.current != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity;
            xRotation -= mouseDelta.y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseDelta.x);
        }

        float moveX = 0f;
        float moveY = 0f;

        if (Keyboard.current != null)
        {
            if (!isFinished) 
            {
                if (Keyboard.current.wKey.isPressed) moveY = 1f;
                if (Keyboard.current.sKey.isPressed) moveY = -1f;
                if (Keyboard.current.dKey.isPressed) moveX = 1f;
                if (Keyboard.current.aKey.isPressed) moveX = -1f;

                isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

                if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
                {
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                }
            }
        }

        moveInput = new Vector2(moveX, moveY).normalized;

        if (momentumTimer > 0f)
        {
            if (isGrounded && !isGrappling)
            {
                momentumTimer -= Time.deltaTime * groundFrictionSpeed;
            }
            else
            {
                momentumTimer -= Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = (transform.forward * moveInput.y + transform.right * moveInput.x).normalized;

        if (!isGrappling && momentumTimer <= 0f)
        {
            Vector3 velocity = moveDirection * moveSpeed;
            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        }
        else
        {
            rb.AddForce(moveDirection * airControlForce, ForceMode.Acceleration);
        }
    }

    public void StartMomentum()
    {
        momentumTimer = momentumRetainTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killzone"))
        {
            if (grapplingHook != null)
            {
                grapplingHook.StopGrapple();
            }

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            momentumTimer = 0f; 
            currentRunTimer = 0f;

            if (deathAudioSource != null)
            {
                deathAudioSource.pitch = Random.Range(0.95f, 1.05f);
                deathAudioSource.Play();
            }

            if (spawnPoint != null)
            {
                transform.position = spawnPoint.position;
            }
        }

        if (other.CompareTag("Finish") && !isFinished)
        {
            isFinished = true; 
            rb.linearVelocity = Vector3.zero;
            momentumTimer = 0f;
            
            if (timerText != null)
            {
                timerText.text = "PARKUR TAMAMLANDI!\n" +
                                 "Toplam Süre: " + totalLevelTimer.ToString("F2") + "\n" +
                                 "Son Deneme: " + currentRunTimer.ToString("F2");
                timerText.color = Color.green; 
            }

            if (winPanel != null)
            {
                winPanel.SetActive(true);
            }

            if (audioSource != null)
            {
                audioSource.Play();
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}