using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;   
    public Text scoreText;
    public GameObject playButton;
    public GameObject exitButton;
    public GameObject gameOver;
    
    public AudioClip pointSound;
    public AudioClip deathSound;

    private int score = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        gameOver.SetActive(false);
        Pause();

    }

    public void Play()
    {
        score = 0; 
        scoreText.text = score.ToString();

        gameOver.SetActive(false);
        playButton.SetActive(false);
        exitButton.SetActive(false);
        

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        playButton.SetActive(true);
        exitButton.SetActive(true);
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 1f);

        Pause();
    }
    
    public void IncreaseScore()
    {
        score++;
        AudioSource.PlayClipAtPoint(pointSound, transform.position, 1f);
        scoreText.text = score.ToString();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
