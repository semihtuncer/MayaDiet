using UnityEngine;
using UnityEngine.UI;

public class SelectionPanelManager : MonoBehaviour
{
    #region Variables
    [Header("Person Stats")]
    public int gender = 0; // 0 = male, 1 = female
    public int age;
    public int weight;
    public int height;

    bool genderSelected = false;

    public Button continueButton;

    [Header("Gender UI")]
    public Color selectedImageColor;
    public Color selectedTextColor;
    public Image maleImage;
    public Image femaleImage;
    public Text maleText;
    public Text femaleText;

    [Header("Age UI")]
    public Slider ageSlider;
    public Text ageSliderText;

    [Header("Weight UI")]
    public Slider weightSlider;
    public Text weightSliderText;

    [Header("Height UI")]
    public Slider heightSlider;
    public Text heightSliderText;
    #endregion

    void OnEnable()
    {
        gender = PlayerPrefs.GetInt("GENDER", -1);
        age = PlayerPrefs.GetInt("AGE", 22);
        weight = PlayerPrefs.GetInt("WEIGHT", 60);
        height = PlayerPrefs.GetInt("HEIGHT", 170);

        OnGenderButtonClick(gender);
        ageSlider.value = age;
        weightSlider.value = weight;
        heightSlider.value = height;

        if (gender == -1)
            genderSelected = false;
        else
            genderSelected = true;
    }
    void Update()
    {
        age = (int)ageSlider.value;
        ageSliderText.text = ageSlider.value.ToString();

        weight = (int)weightSlider.value;
        weightSliderText.text = weightSlider.value.ToString();

        height = (int)heightSlider.value;
        heightSliderText.text = heightSlider.value.ToString();

        continueButton.interactable = genderSelected;
    }

    public void OnGenderButtonClick(int id)
    {
        gender = id;

        genderSelected = true;

        if (gender == 0)
        {
            maleImage.color = selectedImageColor;
            femaleImage.color = Color.white;

            maleText.color = selectedTextColor;
            femaleText.color = Color.white;
        }
        else if (gender == 1)
        {
            maleImage.color = Color.white;
            femaleImage.color = selectedImageColor;

            maleText.color = Color.white;
            femaleText.color = selectedTextColor;
        }
    }
    public void OnContinueButtonClick()
    {
        PlayerPrefs.SetInt("GENDER", gender);
        PlayerPrefs.SetInt("AGE", age);
        PlayerPrefs.SetInt("WEIGHT", weight);
        PlayerPrefs.SetInt("HEIGHT", height);

        float fHeightInMetres = (float)height / 100;
        float fWeight = weight;

        float bmi = fWeight / (fHeightInMetres * fHeightInMetres);

        PlayerPrefs.SetFloat("BMI", bmi);
    }
}