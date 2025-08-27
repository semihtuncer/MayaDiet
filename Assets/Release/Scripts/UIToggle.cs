using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class UIToggle : MonoBehaviour
{
    public bool isOn;

    public float toggleLerpSpeed;

    public Image backgroundImage;
    public Slider toggleSlider;

    public Color backgroundColorOn;
    public Color backgroundColorOff;

    void Awake()
    {

        if (isOn)
        {
            backgroundImage.color = backgroundColorOn;
            toggleSlider.value = 1;
        }
        else
        {
            backgroundImage.color = backgroundColorOff;
            toggleSlider.value = 0;
        }
    }
    public void Toggle()
    {
        isOn = !isOn;

        if (isOn)
        {
            StartCoroutine(LerpValue(0, 1, toggleLerpSpeed));
            StartCoroutine(LerpColor(backgroundColorOff, backgroundColorOn, toggleLerpSpeed));
        }
        else
        {
            StartCoroutine(LerpValue(1, 0, toggleLerpSpeed));
            StartCoroutine(LerpColor(backgroundColorOn, backgroundColorOff, toggleLerpSpeed));
        }
    }

    IEnumerator LerpValue(float startValue, float endValue, float lerpDuration)
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            toggleSlider.value = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        toggleSlider.value = endValue;
    }
    IEnumerator LerpColor(Color startValue, Color endValue, float lerpDuration)
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            backgroundImage.color = Color.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        backgroundImage.color = endValue;
    }
}
