using UnityEngine;
using UnityEngine.UI;

public class UserDetailsPanel : IPanel
{
    [Header("UI")]
    public Text bmiText;
    public Text bmrText;
    public Text calorieBFText;
    public Text calorieSText;
    public Text calorieLText;
    public Text calorieDText;

    void Update()
    {
        bmiText.text = PersonController.person.BMI.ToString("f2");
        bmrText.text = PersonController.person.BMR.ToString("f1") + " kcal";

        calorieBFText.text = PersonController.person.caloriesForBreakfast.ToString("f1") + " kcal";
        calorieSText.text = PersonController.person.caloriesForSnacks.ToString("f1") + " kcal";
        calorieLText.text = PersonController.person.caloriesForLunch.ToString("f1") + " kcal";
        calorieDText.text = PersonController.person.caloriesForDinner.ToString("f1") + " kcal";
    }

    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }
}