using System.Collections.Generic;
using UnityEngine;

public class FavMealMenu : IMenu
{
    public Transform bannerHolder;
    public GameObject warningText;
    public MealBanner mealBannerPrefab;

    List<MealBanner> banners = new List<MealBanner>();

    public override void Show()
    {
        base.Show();

        foreach (var item in PersonController.person.favMeals)
        {
            MealBanner mb = Instantiate(mealBannerPrefab, bannerHolder);
            mb.InitBanner(item);
            banners.Add(mb);
        }

        if(PersonController.person.favMeals.Count == 0)
        {
            warningText.SetActive(true);
        }
        else
        {
            warningText.SetActive(false);
        }
    }
    public override void Hide()
    {
        base.Hide();

        foreach (var item in banners)
        {
            Destroy(item.gameObject);
        }
        banners = new List<MealBanner>();
    }
}