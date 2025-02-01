using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore = 0;
    public Text scoreText;
    public GameObject gameOverScreen;

    public GameObject[] pipes;
    public Rigidbody2D birdRigidBody;
    public BirdScript birdScript;
    public PipeSpawnerScript pipeSpawnerScript;
    public AudioSource dingSFX;
    public AudioSource bigDingSFX;
    public AudioSource gameOverSFX;
    public AudioSource backgroundMusic;

    void Awake()
    {
        birdRigidBody = GameObject.FindGameObjectWithTag("Bird").GetComponent<Rigidbody2D>();
        pipeSpawnerScript = GameObject.FindGameObjectWithTag("PipeSpawner").GetComponent<PipeSpawnerScript>();
        birdScript = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>();
    }

    void Start()
    {
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
        if (playerScore % 10 == 0)
        {
            bigDingSFX.Play();
        }
        else
        {
            dingSFX.Play();
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        backgroundMusic.Stop();
        gameOverSFX.Play();
        gameOverScreen.SetActive(true);
        pipeSpawnerScript.isSpawning = false;

        pipes = GameObject.FindGameObjectsWithTag("Pipe");
        foreach (GameObject pipe in pipes)
        {
            pipe.GetComponent<PipeMoveScript>().stopMoving();
        }

        birdScript.disableJump();
        birdRigidBody.gravityScale = 0;
        birdRigidBody.linearVelocity = Vector2.zero;
    }
}
