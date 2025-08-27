using UnityEngine;
using UnityEngine.UI;

public class ThreeLineMenuController : MonoBehaviour
{
    public Text bmiText;

    void OnEnable()
    {
        bmiText.text = PlayerPrefs.GetFloat("BMI", 20.2f).ToString("00.00");    
    }
}