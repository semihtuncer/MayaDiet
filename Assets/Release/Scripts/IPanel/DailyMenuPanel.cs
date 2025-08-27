using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class DailyMenuPanel : IPanel
{
    [Header("UI")]
    public ScrollRect pageHolder;
    public MealBanner mealBanner;
    public Button saveMenuButton;
    public Toggle savedToggle;
    public Text pagingText;
    bool lerping;
    int currentPage;

    [Header("BREAKFAST")]
    public Transform bHolder;
    List<MealBanner> breakfastBanners = new List<MealBanner>();

    [Header("SNACKS")]
    public Transform sHolder;
    List<MealBanner> snacksBanner = new List<MealBanner>();

    [Header("LUNCH")]
    public Transform lHolder;
    List<MealBanner> lunchBanners = new List<MealBanner>();

    [Header("DINNER")]
    public Transform dHolder;
    List<MealBanner> dinnerBanners = new List<MealBanner>();

    public void ChangePage(int pID)
    {
        if (currentPage == pID)
            return;

        if (lerping)
            return;

        pagingText.text = "Sayfa " + (pID + 1).ToString() + "/2";
        currentPage = pID;

        if (pID == 0)
        {
            StartCoroutine(SlidePage(0, 1, 0.5f));
        }
        else if (pID == 1)
        {
            StartCoroutine(SlidePage(1, 0, 0.5f));
        }
    }
    IEnumerator SlidePage(float start, float target, float duration)
    {
        float t = 0;
        lerping = true;

        while (t < duration)
        {
            t += Time.deltaTime;

            pageHolder.verticalNormalizedPosition = Mathf.Lerp(start, target, t / duration);

            yield return null;
        }

        pageHolder.verticalNormalizedPosition = target;
        lerping = false;
        yield return null;
    }

    void Update()
    {
        if (!PersonController.person.IsMenuPresetSaved(PersonController.person.dailyMenu))
        {
            saveMenuButton.interactable = true;
        }
        else
        {
            saveMenuButton.interactable = false;
        }

        PersonController.person.selectOnlyFromSaved = savedToggle.isOn;
    }
    void OnEnable()
    {
        pagingText.text = "Sayfa " + (currentPage + 1).ToString() + "/2";
        pageHolder.verticalNormalizedPosition = currentPage == 0 ? 1 : 0;
        lerping = false;

        InitBanners();
    }

    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }

    public void InitBanners()
    {
        foreach (var item in breakfastBanners)
        {
            Destroy(item.gameObject);
        }
        breakfastBanners = new List<MealBanner>();

        foreach (var item in snacksBanner)
        {
            Destroy(item.gameObject);
        }
        snacksBanner = new List<MealBanner>();

        foreach (var item in lunchBanners)
        {
            Destroy(item.gameObject);
        }
        lunchBanners = new List<MealBanner>();

        foreach (var item in dinnerBanners)
        {
            Destroy(item.gameObject);
        }
        dinnerBanners = new List<MealBanner>();

        foreach (var item in PersonController.person.dailyMenu.breakfastMenu)
        {
            MealBanner mb = Instantiate(mealBanner, bHolder);
            breakfastBanners.Add(mb);
            mb.InitBanner(item);
        }
        foreach (var item in PersonController.person.dailyMenu.snacksMenu)
        {
            MealBanner mb = Instantiate(mealBanner, sHolder);
            snacksBanner.Add(mb);
            mb.InitBanner(item);
        }
        foreach (var item in PersonController.person.dailyMenu.lunchMenu)
        {
            MealBanner mb = Instantiate(mealBanner, lHolder);
            lunchBanners.Add(mb);
            mb.InitBanner(item);
        }
        foreach (var item in PersonController.person.dailyMenu.dinnerMenu)
        {
            MealBanner mb = Instantiate(mealBanner, dHolder);
            dinnerBanners.Add(mb);
            mb.InitBanner(item);
        }
    }

    public void OpenInspectorBreakfast()
    {
        ((MenuInspectorMenu)UIController.instance.inspectMenuMenu).menuName = "Kahvaltı";
        ((MenuInspectorMenu)UIController.instance.inspectMenuMenu).menuToInspect = PersonController.person.dailyMenu.breakfastMenu;

        UIController.instance.OpenMenuButton(UIController.instance.inspectMenuMenu);
    }
    public void OpenInspectorSnacks()
    {
        ((MenuInspectorMenu)UIController.instance.inspectMenuMenu).menuName = "Ara Ögün";
        ((MenuInspectorMenu)UIController.instance.inspectMenuMenu).menuToInspect = PersonController.person.dailyMenu.snacksMenu;

        UIController.instance.OpenMenuButton(UIController.instance.inspectMenuMenu);
    }
    public void OpenInspectorLunch()
    {
        ((MenuInspectorMenu)UIController.instance.inspectMenuMenu).menuName = "Öğle Yemeği";
        ((MenuInspectorMenu)UIController.instance.inspectMenuMenu).menuToInspect = PersonController.person.dailyMenu.lunchMenu;

        UIController.instance.OpenMenuButton(UIController.instance.inspectMenuMenu);
    }
    public void OpenInspectorDinner()
    {
        ((MenuInspectorMenu)UIController.instance.inspectMenuMenu).menuName = "Akşam Yemeği";
        ((MenuInspectorMenu)UIController.instance.inspectMenuMenu).menuToInspect = PersonController.person.dailyMenu.dinnerMenu;

        UIController.instance.OpenMenuButton(UIController.instance.inspectMenuMenu);
    }

    public void CopyAsText()
    {
        MealManager.instance.GetPrettyStringFromMenuPreset(PersonController.person.dailyMenu).CopyToClipboard();
        UIController.instance.ShowWarning("", "<size=55> Kopyalandı </size> \n\n", null);
    }
    public void SaveMenu()
    {
        if(!PersonController.person.savedMenus.Contains(PersonController.person.dailyMenu))
        {
            UIController.instance.ShowWarning("Menüyü Kaydet", "Menüyü kaydetmek istediğinize emin misiniz?", MenuSaveEvents);
        }
        else
        {
            UIController.instance.ShowWarning("Kaydetme Hatası", "Bu menü zaten kaydedilmiş.", null);
        }
    }

    void MenuSaveEvents()
    {
        saveMenuButton.interactable = false;
        UIController.instance.ShowIFWarning("Menü İsmi Giriniz", MenuNameSaveEvents);
    }
    void MenuNameSaveEvents(string s)
    {
        UIController.instance.ShowWarning("", "<size=55> Kaydedildi. </size> \n\n", null);

        PersonController.person.dailyMenu.menuName = s;
        PersonController.person.dailyMenu.id = System.Guid.NewGuid().ToString();
        PersonController.person.savedMenus.Add(PersonController.person.dailyMenu);
        saveMenuButton.interactable = false;
    }
}