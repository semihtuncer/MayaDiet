using UnityEngine;
using UnityEngine.UI;

public class BMICalculatorPanel : IPanel
{
    public int minAge;
    public int maxAge;

    public Text ageText;

    public Image maleButton;
    public Image femaleButton;

    public InputField weightIF;
    public InputField heightIF;

    public Color selectedGenderButton;
    public Color deselectedGenderButton;

    public Vector2 buttonSelectedExpand;

    void OnEnable()
    {
        PersonController.person.LoadPerson();

        ageText.text = PersonController.person.age.ToString();

        weightIF.text = PersonController.person.weightKG.ToString();
        heightIF.text = PersonController.person.heightCM.ToString();

        SelectGenderButton(PersonController.person.gender);
    }

    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }

    public void AgeUpButton()
    {
        if (PersonController.person.age >= maxAge)
        {
            PersonController.person.age = maxAge;
            ageText.text = PersonController.person.age.ToString();
            return;
        }

        PersonController.person.age++;

        ageText.text = PersonController.person.age.ToString();

        PersonController.person.SavePerson();
    }
    public void AgeDownButton()
    {
        if (PersonController.person.age <= minAge)
        {
            PersonController.person.age = minAge;
            ageText.text = PersonController.person.age.ToString();
            return;
        }

        PersonController.person.age--;

        ageText.text = PersonController.person.age.ToString();

        PersonController.person.SavePerson();
    }

    public void CalculateBMIButton()
    {
        PersonController.person.heightCM = int.Parse(heightIF.text);
        PersonController.person.weightKG = float.Parse(weightIF.text);

        PersonController.person.CalculateBMI();
    }

    public void SelectGenderButton(int gender)
    {
        PersonController.person.gender = gender;

        if (gender == 0)
        {
            maleButton.color = selectedGenderButton;
            maleButton.transform.localScale = buttonSelectedExpand;

            femaleButton.color = deselectedGenderButton;
            femaleButton.transform.localScale = Vector2.one;
        }
        else if (gender == 1)
        {
            maleButton.color = deselectedGenderButton;
            maleButton.transform.localScale = Vector2.one;

            femaleButton.color = selectedGenderButton;
            femaleButton.transform.localScale = buttonSelectedExpand;
        }

        PersonController.person.SavePerson();
    }

    public void OnWeightHeightChange()
    {
        PersonController.person.heightCM = int.Parse(heightIF.text);
        PersonController.person.weightKG = float.Parse(weightIF.text);
    }
}