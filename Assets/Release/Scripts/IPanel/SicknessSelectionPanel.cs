using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SicknessSelectionPanel : IPanel
{
    [Header("UI")]
    public int maxSickness;

    public Toggle celiacToggle;
    public Toggle pcosToggle;
    public Toggle diabetesToggle;

    public IMenu selectionMenu;

    public override void Show()
    {
        base.Show();

        celiacToggle.isOn = PersonController.person.hasCeliac;
        pcosToggle.isOn = PersonController.person.hasPCOS;
        diabetesToggle.isOn = PersonController.person.hasDiabetes;
    }

    public void OnToggle(Toggle t)
    {
        if (t == celiacToggle)
            PersonController.person.hasCeliac = celiacToggle.isOn;
        else if (t == pcosToggle)
            PersonController.person.hasPCOS = pcosToggle.isOn;
        else if (t == diabetesToggle)
            PersonController.person.hasDiabetes = diabetesToggle.isOn;
    }
    public void ContinueButton()
    {
        if (!diabetesToggle.isOn && !celiacToggle.isOn && !pcosToggle.isOn)
        {
            UIController.instance.OpenMenuButton(UIController.instance.mainMenu);
            UIController.instance.mainMenu.OpenPanelButton(UIController.instance.mainMenu.allPanels[0]);

            PlayerPrefs.SetInt("SETUPCOMPLETE", 1);
        }
        else
        {
            selectionMenu.OpenPanel(selectionMenu.allPanels[2]);
        }

        PersonController.person.SavePerson();
    }
}