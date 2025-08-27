using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBanner : MonoBehaviour
{
    [Header("UI")]
    public Image favImage;
    public Image menuImage;
    public Text menuName;

    public Sprite favPressed;
    public Sprite favDefault;

    MenuPreset bannerPreset;

    public void InitBanner(string nm, MenuPreset mp)
    {
        bannerPreset = mp;
        menuName.text = nm;
        menuImage.sprite = mp.breakfastMenu[0].GetMealImage();
    }
    public void OnFavButtonPressed()
    {
        if (PersonController.person.savedMenus.Contains(bannerPreset))
        {
            favImage.sprite = favDefault;
            PersonController.person.savedMenus.Remove(bannerPreset);
        }
        else
        {
            favImage.sprite = favPressed;
            PersonController.person.savedMenus.Add(bannerPreset);
        }
    }
    public void OnBannerPressed()
    {
        UIController.instance.ShowWarning("Menü Değiştirme", "Günlük menünüzü " + menuName.text + " ile değiştirmek ister misiniz?", Events);
    }
    void Events()
    {
        PersonController.person.dailyMenu = bannerPreset;
    }

    public void OnCopyPressed()
    {
        MealManager.instance.GetPrettyStringFromMenuPreset(bannerPreset).CopyToClipboard();
        UIController.instance.ShowWarning("", "<size=55> Kopyalandı </size> \n\n", null);
    }
}