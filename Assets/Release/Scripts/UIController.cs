using UnityEngine;
using UnityEngine.UI;

using System.Linq;
using System.Collections.Generic;

// Made by ST

public class UIController : MonoBehaviour
{
    [Header("MENU")]
    public IMenu startMenu;
    public IMenu mainMenu;
    [Space(5)]
    public IMenu currentMenu;
    public IMenu lastMenu;
    [Space(5)]
    public IMenu inspectMealMenu;
    public IMenu inspectMenuMenu;
    [Space(5)]
    public List<IMenu> allMenus = new List<IMenu>();

    [Header("WARNING")]
    public bool isWarning = false;
    public GameObject warningGO;
    public GameObject warningBanner;
    public Text warningTitle;
    public Text warningText;

    [Header("IF WARNING")]
    public GameObject ifWarningGO;
    public GameObject ifWarningBanner;
    public Text ifWarningTitle;
    public InputField ifWarningIF;
    public Button ifWarningButton;

    public static UIController instance;

    System.Action warningAction;
    System.Action<string> ifWarningAction;

    void Awake()
    {
        instance = this;

#if UNITY_ANDROID
        Screen.fullScreen = false;
#endif
    }
    void Start()
    {
        if (PlayerPrefs.GetInt("ENTERANCE", 0) == 0 || PlayerPrefs.GetInt("SETUPCOMPLETE", 0) == 0)
        {
            OpenMenu(startMenu);
        }
        else
        {
            OpenMenu(mainMenu);
        }

        PlayerPrefs.SetInt("ENTERANCE", PlayerPrefs.GetInt("ENTERANCE") + 1);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isWarning)
                ReturnMenu();
            else
                HideWarning(true);
        }

        if (string.IsNullOrEmpty(ifWarningIF.text) || string.IsNullOrWhiteSpace(ifWarningIF.text))
        {
            ifWarningButton.interactable = false;
        }
        else
        {
            ifWarningButton.interactable = true;
        }

        if (!currentMenu.gameObject.activeInHierarchy)
            currentMenu.gameObject.SetActive(true);
    }

    public void OpenMenu(IMenu menuToShow, bool saveLastMenu = true)
    {
        if (menuToShow == null)
            return;

        if (menuToShow == currentMenu)
            return;

        foreach (IMenu m in allMenus)
        {
            if (m != menuToShow)
                m.Hide();
        }

        if (currentMenu != null)
        {
            if (saveLastMenu)
                lastMenu = currentMenu;
            else
                lastMenu = null;

            currentMenu.Hide();
        }
        else
        {
            lastMenu = null;
        }

        currentMenu = menuToShow;
        menuToShow.Show();

        if (menuToShow.currentPanel != null)
        {
            menuToShow.OpenPanel(menuToShow.currentPanel, false);
        }
        else
        {
            menuToShow.OpenPanel(menuToShow.startPanel, false);
        }
    }
    public void OpenMenuButton(IMenu menuToShow)
    {
        OpenMenu(menuToShow);
    }

    public void ReturnMenu()
    {
        if (currentMenu.lastPanel != null)
        {
            if (currentMenu.lastPanel.returnable)
            {
                currentMenu.OpenPanel(currentMenu.lastPanel, false);
                lastMenu = null;
            }
            else
            {
                if (lastMenu != null)
                {
                    if (lastMenu.returnable && currentMenu.allowReturning)
                    {
                        OpenMenu(lastMenu, false);
                    }
                }
            }
        }
        else
        {
            if (lastMenu != null)
            {
                if (lastMenu.returnable && currentMenu.allowReturning)
                {
                    OpenMenu(lastMenu, false);
                }
            }
            else
            {
                if(currentMenu == mainMenu)
                {
                    if(currentMenu.currentPanel == currentMenu.startPanel)
                    {
                        Application.Quit();
                    }
                    else
                    {
                        currentMenu.OpenPanel(mainMenu.startPanel, false);
                    }
                }
                else if (currentMenu.allowReturning)
                {
                    OpenMenu(mainMenu, false);
                }
            }
        }
    }

    public void ShowIFWarning(string title, System.Action<string> action)
    {
        isWarning = true;
        ifWarningTitle.text = title;
        ifWarningGO.SetActive(true);

        LeanTween.scale(ifWarningBanner, Vector3.one, 0.1f);
        ifWarningAction = action;
    }
    public void ShowWarning(string title, string warning, System.Action action)
    {
        isWarning = true;
        warningTitle.text = title;
        warningText.text = warning;
        warningGO.SetActive(true);

        LeanTween.scale(warningBanner, Vector3.one, 0.1f);
        warningAction = action;
    }
    public void HideWarning(bool dontInvoke)
    {
        if (warningGO.activeInHierarchy)
        {
            LTDescr desc = LeanTween.scale(warningBanner, Vector3.zero, 0.1f);
            desc.setOnComplete(() => warningGO.SetActive(false));

            if (!dontInvoke)
            {
                if (warningAction != null)
                {
                    warningAction.Invoke();
                }
            }
        }
        else if (ifWarningGO.activeInHierarchy)
        {
            LTDescr desc = LeanTween.scale(ifWarningBanner, Vector3.zero, 0.1f);
            desc.setOnComplete(() => ifWarningGO.SetActive(false));

            if (!dontInvoke)
            {
                if (ifWarningAction != null)
                {
                    ifWarningAction.Invoke(ifWarningIF.text);
                }
            }

            ifWarningIF.text = "";
        }

        isWarning = false;
    }

    void OnValidate()
    {
        allMenus = allMenus.Distinct().ToList();
    }
}