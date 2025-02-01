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

    void Start()
    {
        birdRigidBody = GameObject.FindGameObjectWithTag("Bird").GetComponent<Rigidbody2D>();
        pipeSpawnerScript = GameObject.FindGameObjectWithTag("PipeSpawner").GetComponent<PipeSpawnerScript>();
        birdScript = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>();
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
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
