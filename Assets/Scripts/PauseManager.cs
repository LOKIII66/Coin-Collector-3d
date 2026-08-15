using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    [Header("Button Sound")]
    public AudioSource buttonAudio;
    public AudioClip buttonClickSound;

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        if (isPaused && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;

        pausePanel.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        PlayButtonSound();

        isPaused = false;

        pausePanel.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartGame()
    {
        StartCoroutine(RestartAfterSound());
    }

    private IEnumerator RestartAfterSound()
    {
        PlayButtonSound();

        // Wait for sound to finish
        if (buttonClickSound != null)
        {
            yield return new WaitForSecondsRealtime(buttonClickSound.length);
        }

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        StartCoroutine(QuitAfterSound());
    }

    private IEnumerator QuitAfterSound()
    {
        PlayButtonSound();

        // Wait for sound to finish
        if (buttonClickSound != null)
        {
            yield return new WaitForSecondsRealtime(buttonClickSound.length);
        }

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu");
    }

    private void PlayButtonSound()
    {
        if (buttonAudio != null && buttonClickSound != null)
        {
            buttonAudio.PlayOneShot(buttonClickSound);
        }
    }
}