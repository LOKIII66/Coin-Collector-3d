using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public void ResetProgress()
    {
        // Lock all levels except Level 1
        PlayerPrefs.SetInt("HighestLevel", 1);
        PlayerPrefs.Save();

        Debug.Log("Game progress reset. Only Level 1 is unlocked.");

        // Go back to Level Select
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("LevelSelect");
    }
}