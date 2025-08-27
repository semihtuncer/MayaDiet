using UnityEngine;
using UnityEngine.UI;

using System;

public class HomePanel : IPanel
{
    [Header("UI")]
    public Text greetingText;

    void OnEnable()
    {
        if (DateTime.Now.Hour >= 22 || DateTime.Now.Hour < 6)
            greetingText.text = "İyi geceler";
        else if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 12)
            greetingText.text = "Günaydın";
        else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 17)
            greetingText.text = "Tünaydın";
        else if (DateTime.Now.Hour >= 17 && DateTime.Now.Hour < 22)
            greetingText.text = "İyi akşamlar";
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