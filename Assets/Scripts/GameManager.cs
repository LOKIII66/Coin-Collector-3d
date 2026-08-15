using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text timerText;

    public CanvasGroup winPanel;
    public GameObject gameOverPanel;

    [Header("Player")]
    public PlayerMovement playerMovement;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip coinSound;

    [Header("Game Settings")]
    public int score = 0;
    public int totalCoins = 0;

    // Set this according to the level
    // Level 1 = 1
    // Level 2 = 2
    // Level 3 = 3
    // Level 4 = 4
    // Level 5 = 5
    [Header("Level")]
    public int currentLevel = 1;

    // Set this from Inspector
    public float timeLeft = 200f;

    private bool gameEnded = false;


    // =========================
    // AWAKE
    // =========================

    private void Awake()
    {
        Instance = this;
    }


    // =========================
    // START
    // =========================

    private void Start()
    {
        Time.timeScale = 1f;

        gameEnded = false;
        score = 0;

        // Cursor locked during gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        // =========================
        // SCORE
        // =========================

        if (scoreText != null)
        {
            scoreText.text = "Score : 0";
        }


        // =========================
        // TIMER
        // =========================

        if (timerText != null)
        {
            timerText.text = "Time : " + Mathf.Ceil(timeLeft);
        }


        // =========================
        // COUNT COINS
        // =========================

        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;


        // =========================
        // HIDE WIN PANEL
        // =========================

        if (winPanel != null)
        {
            winPanel.gameObject.SetActive(false);

            winPanel.alpha = 0f;

            winPanel.interactable = false;

            winPanel.blocksRaycasts = false;
        }


        // =========================
        // HIDE GAME OVER PANEL
        // =========================

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }


    // =========================
    // UPDATE
    // =========================

    private void Update()
    {
        if (gameEnded)
            return;


        // Countdown
        timeLeft -= Time.deltaTime;


        if (timeLeft < 0f)
        {
            timeLeft = 0f;
        }


        // Update timer
        if (timerText != null)
        {
            timerText.text = "Time : " + Mathf.Ceil(timeLeft);
        }


        // Time finished
        if (timeLeft <= 0f)
        {
            GameOver();
        }
    }


    // =========================
    // COIN COLLECTION
    // =========================

    public void AddScore()
    {
        if (gameEnded)
            return;


        score++;


        // Update score
        if (scoreText != null)
        {
            scoreText.text = "Score : " + score;
        }


        // Coin sound
        if (audioSource != null && coinSound != null)
        {
            audioSource.PlayOneShot(coinSound);
        }


        // All coins collected
        if (score >= totalCoins)
        {
            WinGame();
        }
    }


    // =========================
    // WIN GAME
    // =========================

    private void WinGame()
    {
        if (gameEnded)
            return;


        gameEnded = true;


        // =========================
        // UNLOCK NEXT LEVEL
        // =========================

        UnlockNextLevel();


        // Stop player
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }


        // Hide Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }


        // Show Win Panel
        StartCoroutine(ShowWinPanel());
    }


    // =========================
    // UNLOCK NEXT LEVEL
    // =========================

    private void UnlockNextLevel()
    {
        // Level 5 is the last level
        if (currentLevel >= 5)
        {
            Debug.Log("Level 5 completed! All levels completed.");
            return;
        }


        // Get currently unlocked level
        int highestUnlockedLevel =
            PlayerPrefs.GetInt("HighestLevel", 1);


        // Next level
        int nextLevel = currentLevel + 1;


        // Unlock only if necessary
        if (nextLevel > highestUnlockedLevel)
        {
            PlayerPrefs.SetInt("HighestLevel", nextLevel);

            PlayerPrefs.Save();

            Debug.Log("Level " + nextLevel + " unlocked!");
        }
    }


    // =========================
    // WIN PANEL
    // =========================

    private IEnumerator ShowWinPanel()
    {
        // Make sure Win Panel exists
        if (winPanel == null)
        {
            Debug.LogError(
                "Win Panel is NOT assigned in GameManager Inspector!"
            );

            yield break;
        }


        // Show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        // Enable panel
        winPanel.gameObject.SetActive(true);


        winPanel.alpha = 0f;

        winPanel.interactable = false;

        winPanel.blocksRaycasts = true;


        // Start small
        winPanel.transform.localScale =
            Vector3.one * 0.8f;


        // Animation
        while (winPanel.alpha < 1f)
        {
            winPanel.alpha +=
                Time.unscaledDeltaTime * 1.5f;


            winPanel.transform.localScale =
                Vector3.Lerp(
                    Vector3.one * 0.8f,
                    Vector3.one,
                    winPanel.alpha
                );


            yield return null;
        }


        // Final state
        winPanel.alpha = 1f;

        winPanel.transform.localScale =
            Vector3.one;


        // Enable buttons
        winPanel.interactable = true;

        winPanel.blocksRaycasts = true;


        // Pause game
        Time.timeScale = 0f;
    }


    // =========================
    // GAME OVER
    // =========================

    private void GameOver()
    {
        if (gameEnded)
            return;


        gameEnded = true;


        // Stop player
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }


        // Hide Win Panel
        if (winPanel != null)
        {
            winPanel.gameObject.SetActive(false);

            winPanel.interactable = false;

            winPanel.blocksRaycasts = false;
        }


        // Show Game Over Panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }


        // Show cursor
        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;


        // Pause game
        Time.timeScale = 0f;
    }
}