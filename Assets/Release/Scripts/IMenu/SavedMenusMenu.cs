using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedMenusMenu : IMenu
{
    [Header("UI")]
    public GameObject warning;
    public MenuBanner menuBanner;
    public Transform holder;

    List<MenuBanner> spawnedBanners = new List<MenuBanner>();

    public override void Show()
    {
        base.Show();

        if (PersonController.person.savedMenus.Count == 0)
            warning.SetActive(true);
        else
            warning.SetActive(false);

        for (int i = 0; i < PersonController.person.savedMenus.Count; i++)
        {
            MenuBanner m = Instantiate(menuBanner, holder);
            m.InitBanner(PersonController.person.savedMenus[i].menuName, PersonController.person.savedMenus[i]);
            spawnedBanners.Add(m);
        }
    }
    public override void Hide()
    {
        base.Hide();

        foreach (var item in spawnedBanners)
        {
            Destroy(item.gameObject);
        }

        spawnedBanners.Clear();
    }
}