using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    #region Variables
    [Header("Page")]
    public string currentPage;
    public GameObject introPage;
    public GameObject startPage;
    public GameObject mainPage;

    [Header("Panels")]
    public GameObject infoPanel;
    public GameObject selectionPanel;
    public GameObject sicknessPanel;

    [Header("ThreeLine Swipe")]
    public float swipeSensitivity;
    public Button threeLineButton;

    Vector2 startSwipe;
    Vector2 swipeDelta;
    bool swipe;
    #endregion

    void Start()
    {
        GoToPage("intro");

        if (PlayerPrefs.GetInt("ENTERANCE", 0) == 0)
        {
            introPage.SetActive(false);
            startPage.SetActive(false);
            mainPage.SetActive(false);

            infoPanel.SetActive(true);
            selectionPanel.SetActive(false);

            GoToPage("start");
        }
        else
        {
            introPage.SetActive(true);
            startPage.SetActive(false);
            mainPage.SetActive(false);

            infoPanel.SetActive(false);
            selectionPanel.SetActive(true);

            GoToPage("main");
        }

        PlayerPrefs.SetInt("ENTERANCE", PlayerPrefs.GetInt("ENTERANCE") + 1);
    }
    void Update()
    {
        HandleThreeLineSwipe();
    }

    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
        menu.GetComponent<Animator>().Play("GO-IN");
    }
    public void CloseMenu(GameObject menu)
    {
        StartCoroutine(CloseMenuRoutine(menu));
    }
    IEnumerator CloseMenuRoutine(GameObject menu)
    {
        menu.GetComponent<Animator>().Play("GO-OUT");
        yield return new WaitForSeconds(.25f);
        menu.SetActive(false);
    }

    public void GoToPage(string pName)
    {
        currentPage = pName;

        switch (pName)
        {
            case "intro":
                StartCoroutine(TogglePage(true, 1, introPage));
                StartCoroutine(TogglePage(false, 1, startPage));
                StartCoroutine(TogglePage(false, 1, mainPage));
                break;
            case "main":
                StartCoroutine(TogglePage(false, 1, introPage));
                StartCoroutine(TogglePage(false, 1, startPage));
                StartCoroutine(TogglePage(true, 1, mainPage));
                break;
            case "start":
                StartCoroutine(TogglePage(false, 1, introPage));
                StartCoroutine(TogglePage(true, 1, startPage));
                StartCoroutine(TogglePage(false, 1, mainPage));

                if (PlayerPrefs.GetInt("ENTERANCE", 0) == 0)
                {
                    infoPanel.SetActive(true);
                    selectionPanel.SetActive(false);
                }
                else
                {
                    infoPanel.SetActive(false);
                    selectionPanel.SetActive(true);
                }
                sicknessPanel.SetActive(false);
                break;
        }
    }
    IEnumerator TogglePage(bool toggle, float waitTime, GameObject page)
    {
        if (toggle)
        {
            page.SetActive(true);
            page.GetComponent<Animator>().Play("FADE-IN");
        }
        else
        {
            if (page.activeSelf)
            {
                page.GetComponent<Animator>().Play("FADE-OUT");

                yield return new WaitForSeconds(waitTime);

                page.SetActive(false);
            }
        }
    }

    void HandleThreeLineSwipe()
    {
        if (threeLineButton.gameObject.activeSelf && mainPage.GetComponent<CanvasGroup>().interactable && currentPage == "main")
        {
            if (Input.GetMouseButtonDown(0))
            {
                swipe = true;
                startSwipe = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swipe = false;
            }
            if (swipe)
            {
                swipeDelta = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - startSwipe;
            }
            else
            {
                swipeDelta = Vector2.zero;
                startSwipe = Vector2.zero;
            }
            if (swipeDelta.x >= swipeSensitivity)
            {
                threeLineButton.onClick.Invoke();
            }
        }
    }
}