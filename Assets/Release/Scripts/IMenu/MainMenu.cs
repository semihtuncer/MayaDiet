using UnityEngine;
using UnityEngine.UI;

public class MainMenu : IMenu
{
    [Header("UI")]
    public Image homeButtonImage;
    public Sprite homeButtonSelected;
    public Sprite homeButtonDefault;

    [Space(5)]
    public Image userDetailsButtonImage;
    public Sprite userButtonSelected;
    public Sprite userButtonDefault;

    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }
    public override void OpenPanel(IPanel panelToShow, bool saveLastPanel = true)
    {
        base.OpenPanel(panelToShow, saveLastPanel);

        if(panelToShow.name == "HomePanel")
        {
            homeButtonImage.sprite = homeButtonSelected;
            userDetailsButtonImage.sprite = userButtonDefault;
        }
        else if(panelToShow.name == "UserDetailsPanel")
        {
            homeButtonImage.sprite = homeButtonDefault;
            userDetailsButtonImage.sprite = userButtonSelected;
        }
    }
}