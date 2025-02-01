using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    public PipeSpawnerScript pipeSpawnerScript;
    public GameObject[] pipes;
    public InputAction jump;

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
        pipeSpawnerScript = GameObject.FindGameObjectWithTag("PipeSpawner").GetComponent<PipeSpawnerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime));

        if (jump.WasPressedThisFrame() && birdIsAlive)
        {
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        birdIsAlive = false;
        pipeSpawnerScript.isGameOver = true;

        pipes = GameObject.FindGameObjectsWithTag("Pipe");
        foreach (GameObject pipe in pipes)
        {
            pipe.GetComponent<PipeMoveScript>().stopMoving();
        }
    }
}
