using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    [Header("Level Buttons")]
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;
    public Button level5Button;

    private void Start()
    {
        UpdateLevelButtons();
    }

    public void UpdateLevelButtons()
    {
        int highestLevel = PlayerPrefs.GetInt("HighestLevel", 1);

        Debug.Log("Highest Unlocked Level = " + highestLevel);

        if (level1Button != null)
            level1Button.interactable = highestLevel >= 1;

        if (level2Button != null)
            level2Button.interactable = highestLevel >= 2;

        if (level3Button != null)
            level3Button.interactable = highestLevel >= 3;

        if (level4Button != null)
            level4Button.interactable = highestLevel >= 4;

        if (level5Button != null)
            level5Button.interactable = highestLevel >= 5;
    }

    public void OpenLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OpenLevel2()
    {
        if (PlayerPrefs.GetInt("HighestLevel", 1) >= 2)
        {
            SceneManager.LoadScene("Level 2");
        }
    }

    public void OpenLevel3()
    {
        if (PlayerPrefs.GetInt("HighestLevel", 1) >= 3)
        {
            SceneManager.LoadScene("Level 3");
        }
    }

    public void OpenLevel4()
    {
        if (PlayerPrefs.GetInt("HighestLevel", 1) >= 4)
        {
            SceneManager.LoadScene("Level 4");
        }
    }

    public void OpenLevel5()
    {
        if (PlayerPrefs.GetInt("HighestLevel", 1) >= 5)
        {
            SceneManager.LoadScene("Level 5");
        }
    }
    public void MainMenu()
    {
        
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}