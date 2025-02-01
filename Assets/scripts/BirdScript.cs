using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D birdRigidBody;
    public float flapStrength;
    public float boundary = 10;
    public bool birdIsAlive = true;
    public InputAction jump;
    public LogicScript logic;

    private void OnEnable()
    {
        jump.Enable();
    }

    private void OnDisable()
    {
        jump.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime));

        if (jump.WasPressedThisFrame() && birdIsAlive)
        {
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

    public void disableJump()
    {
        jump.Disable();
    }
}
