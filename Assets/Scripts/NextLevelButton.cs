using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    [Header("Button Sound")]
    public AudioSource buttonAudio;
    public AudioClip buttonClickSound;

    public void NextLevel()
    {
        StartCoroutine(NextLevelAfterSound());
    }

    private IEnumerator NextLevelAfterSound()
    {
        Debug.Log("NEXT BUTTON PRESSED");

        // Play button sound
        if (buttonAudio != null && buttonClickSound != null)
        {
            buttonAudio.PlayOneShot(buttonClickSound);

            // Wait for the complete sound
            yield return new WaitForSecondsRealtime(
                buttonClickSound.length
            );
        }

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Loading LevelSelect...");

        SceneManager.LoadScene("LevelSelect");
    }
}