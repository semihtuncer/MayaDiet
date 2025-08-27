using UnityEngine;

public class SelectionMenu : IMenu
{
    [Header("UI")]
    public GameObject returnButton;

    void OnEnable()
    {
        if (PlayerPrefs.GetInt("ENTERANCE", 0) == 0 || PlayerPrefs.GetInt("SETUPCOMPLETE", 0) == 0)
        {
            returnButton.SetActive(false);
        }
        else
        {
            returnButton.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentPanel == allPanels[1])
            {
                OpenPanelButton(allPanels[0]);
            }
            else if (currentPanel == allPanels[2])
            {
                OpenPanelButton(allPanels[1]);
            }
        }
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