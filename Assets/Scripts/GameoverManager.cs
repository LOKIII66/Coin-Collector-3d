using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverManager : MonoBehaviour
{
    public GameObject gameoverPanel;

    [Header("Button Sound")]
    public AudioSource buttonAudio;
    public AudioClip buttonClickSound;

    private bool isGameover = false;

    void Start()
    {
        gameoverPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }

    public void Restart()
    {
        StartCoroutine(RestartAfterSound());
    }

    private IEnumerator RestartAfterSound()
    {
        // Play button sound
        if (buttonAudio != null && buttonClickSound != null)
        {
            buttonAudio.PlayOneShot(buttonClickSound);

            // Wait until the complete sound finishes
            yield return new WaitForSecondsRealtime(buttonClickSound.length);
        }

        // Resume game time
        Time.timeScale = 1f;

        // Lock cursor again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Restart current level
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}