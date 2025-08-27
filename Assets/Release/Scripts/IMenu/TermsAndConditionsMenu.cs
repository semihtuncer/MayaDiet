using UnityEngine;
using UnityEngine.UI;

public class TermsAndConditionsMenu : IMenu
{
    public Button continueButton;

    void Start()
    {
        continueButton.interactable = false;
    }

    public void TermsAndConditionsAgreeButton(Toggle t)
    {
        if(t.isOn)
            PlayerPrefs.SetInt("TERMS", 1);
        else
            PlayerPrefs.SetInt("TERMS", 0);

        continueButton.interactable = t.isOn;
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