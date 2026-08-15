using UnityEngine;

public class LevelUnlockManager : MonoBehaviour
{
    public static LevelUnlockManager Instance;

    private const string UNLOCKED_LEVEL_KEY = "UnlockedLevel";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetUnlockedLevel()
    {
        // Level 1 is always unlocked
        return PlayerPrefs.GetInt(UNLOCKED_LEVEL_KEY, 1);
    }

    public bool IsLevelUnlocked(int level)
    {
        return level <= GetUnlockedLevel();
    }

    public void UnlockNextLevel(int completedLevel)
    {
        int unlockedLevel = GetUnlockedLevel();

        if (completedLevel >= unlockedLevel)
        {
            int nextLevel = completedLevel + 1;

            // Maximum is Level 5
            if (nextLevel <= 5)
            {
                PlayerPrefs.SetInt(UNLOCKED_LEVEL_KEY, nextLevel);
                PlayerPrefs.Save();

                Debug.Log("Unlocked Level " + nextLevel);
            }
        }
    }

    // Optional: reset all level progress
    public void ResetLevels()
    {
        PlayerPrefs.SetInt(UNLOCKED_LEVEL_KEY, 1);
        PlayerPrefs.Save();

        Debug.Log("All levels reset. Only Level 1 is unlocked.");
    }
}