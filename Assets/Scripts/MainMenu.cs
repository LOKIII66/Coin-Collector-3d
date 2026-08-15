using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Button Sound")]
    public AudioSource buttonAudio;
    public AudioClip buttonClickSound;

    public void PlayGame()
    {
        StartCoroutine(PlayGameAfterSound());
    }

    private IEnumerator PlayGameAfterSound()
    {
        // Play button sound
        if (buttonAudio != null && buttonClickSound != null)
        {
            buttonAudio.PlayOneShot(buttonClickSound);

            // Wait for complete sound
            yield return new WaitForSecondsRealtime(
                buttonClickSound.length
            );
        }

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("LevelSelect");
    }

    public void QuitGame()
    {
        StartCoroutine(QuitAfterSound());
    }

    private IEnumerator QuitAfterSound()
    {
        Debug.Log("Quit Game");

        // Play button sound
        if (buttonAudio != null && buttonClickSound != null)
        {
            buttonAudio.PlayOneShot(buttonClickSound);

            // Wait for complete sound
            yield return new WaitForSecondsRealtime(
                buttonClickSound.length
            );
        }

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}