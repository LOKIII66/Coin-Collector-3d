using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundSlideshow : MonoBehaviour
{
    [Header("Background Images")]
    public Image backgroundImage;

    public Sprite[] backgrounds;

    [Header("Settings")]
    public float changeTime = 3f;

    [Header("Fade")]
    public bool useFade = true;
    public float fadeSpeed = 2f;

    private int currentIndex = 0;

    void Start()
    {
        if (backgroundImage == null)
        {
            Debug.LogError("Background Image is not assigned!");
            return;
        }

        if (backgrounds == null || backgrounds.Length == 0)
        {
            Debug.LogError("No background images assigned!");
            return;
        }

        // Show first image
        backgroundImage.sprite = backgrounds[0];

        StartCoroutine(ChangeBackground());
    }

    IEnumerator ChangeBackground()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeTime);

            currentIndex++;

            if (currentIndex >= backgrounds.Length)
            {
                currentIndex = 0;
            }

            if (useFade)
            {
                yield return StartCoroutine(FadeToNextImage());
            }
            else
            {
                backgroundImage.sprite = backgrounds[currentIndex];
            }
        }
    }

    IEnumerator FadeToNextImage()
    {
        Color color = backgroundImage.color;

        // Fade out
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime * fadeSpeed;
            backgroundImage.color = color;

            yield return null;
        }

        // Change image
        backgroundImage.sprite = backgrounds[currentIndex];

        // Fade in
        while (color.a < 1f)
        {
            color.a += Time.deltaTime * fadeSpeed;
            backgroundImage.color = color;

            yield return null;
        }

        color.a = 1f;
        backgroundImage.color = color;
    }
}