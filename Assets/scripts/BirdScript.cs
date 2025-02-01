using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D birdRigidBody;
    public float flapStrength;
    public float boundary = 10;
    public bool birdIsAlive = true;
    private bool isJumping = false;
    public LogicScript logic;
    private AudioSource flapSFX;
    private InputAction jumpAction;
    private PlayerInputActions playerControls;

    private void OnEnable()
    {
        jumpAction = playerControls.Player.Jump;
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        jumpAction.Disable();
    }

    void Awake()
    {
        playerControls = new PlayerInputActions();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        flapSFX = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!birdIsAlive) return;

        // Check for touch input
        isJumping = jumpAction.WasPressedThisFrame() || jumpAction.WasPerformedThisFrame() || (Touchscreen.current != null && Touchscreen.current.primaryTouch.isInProgress);

        transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime));
        if (isJumping)
        {
            flapSFX.PlayOneShot(flapSFX.clip);
            birdRigidBody.linearVelocity = Vector2.up * flapStrength;
        }

        if (transform.position.y > boundary || transform.position.y < -boundary)
        {
            logic.gameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
    }

    public void freeze()
    {
        jumpAction.Disable();
        birdIsAlive = false;
        birdRigidBody.gravityScale = 0;
        birdRigidBody.linearVelocity = Vector2.zero;
        birdRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
