using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllMealsMenu : IMenu
{
    [Header("SETUP")]
    public Transform mealHolder;
    public MealBanner mealBannerPrefab;

    [Header("QUERY")]
    public int bannersPerPage = 10;
    public int curPage = 0;
    public int maxPage;
    public List<Meal> queryMeals = new List<Meal>();
    public ScrollRect scrollRect;
    public InputField searchIF;
    public Dropdown sortDropdown;

    public Text pagingText;

    bool init = false;
    List<MealBanner> instancedBanners = new List<MealBanner>();
    List<Meal> lastQuery = new List<Meal>();

    public void OnSearchFieldEditEnd()
    {
        sortDropdown.value = 0;

        QueryMealsWithName(searchIF.text);
    }
    public void QueryMealsWithName(string s)
    {
        List<Meal> qMeals = new List<Meal>();

        if (string.IsNullOrEmpty(s))
        {
            qMeals = MealManager.instance.allMeals;
        }
        else
        {
            qMeals = queryMeals.Where(a => a.Name.ToLower().Contains(s.ToLower())).ToList();
        }
        queryMeals = qMeals;

        if (s == "bade")
        {
            UIController.instance.ShowWarning("Hata!", "Bu tatlı veritabanında bulunamadı.", null);
        }

        scrollRect.verticalNormalizedPosition = 1;

        curPage = 0;
        InstantiateBanners(qMeals);
    }

    public void OnSortDropDownValueChange()
    {
        List<Meal> meals = new List<Meal>();
        meals.AddRange(queryMeals);

        curPage = 0;

        if (sortDropdown.value == 0)
        {
            meals.Sort((x, y) => string.Compare(x.Name, y.Name));
            InstantiateBanners(meals);
        }
        else if (sortDropdown.value == 1)
        {
            meals.Sort((x, y) => string.Compare(x.Name, y.Name));
            meals.Reverse();
            InstantiateBanners(meals);
        }
        else if (sortDropdown.value == 2)
        {
            meals = meals.OrderBy(a => a.Calories).ToList();
            InstantiateBanners(meals);
        }
        else if (sortDropdown.value == 3)
        {
            meals = meals.OrderBy(a => a.Calories).ToList();
            meals.Reverse();
            InstantiateBanners(meals);
        }
        else if (sortDropdown.value == 4)
        {
            InstantiateBanners(meals.GetListByType(MealType.Baklagil));
        }
        else if (sortDropdown.value == 5)
        {
            InstantiateBanners(meals.GetListByType(MealType.EtYemeği));
        }
        else if (sortDropdown.value == 6)
        {
            InstantiateBanners(meals.GetListByType(MealType.Meyve));
        }
        else if (sortDropdown.value == 7)
        {
            InstantiateBanners(meals.GetListByType(MealType.Sebze));
        }
        else if (sortDropdown.value == 8)
        {
            InstantiateBanners(meals.GetListByType(MealType.SütÜrünü));
        }
        else if (sortDropdown.value == 9)
        {
            InstantiateBanners(meals.GetListByType(MealType.UnluMamül));
        }
        else if (sortDropdown.value == 10)
        {
            InstantiateBanners(meals.GetListByType(MealType.Kuruyemiş));
        }
        else if (sortDropdown.value == 11)
        {
            InstantiateBanners(meals.GetListByType(MealType.Tatlı));
        }
        else if (sortDropdown.value == 12)
        {
            InstantiateBanners(meals.GetListByType(MealType.Kahvaltılık));
        }
        else if (sortDropdown.value == 13)
        {
            InstantiateBanners(meals.GetListByType(MealType.Çorba));
        }
        else if (sortDropdown.value == 14)
        {
            InstantiateBanners(meals.GetListByType(MealType.İçecek));
        }
        else if (sortDropdown.value == 15)
        {
            InstantiateBanners(meals.GetListByType(MealType.Peynir));
        }
        else if (sortDropdown.value == 16)
        {
            InstantiateBanners(meals.GetListByType(MealType.Salata));
        }

        queryMeals = meals;

        scrollRect.verticalNormalizedPosition = 1;
    }

    public void InstantiateBanners(List<Meal> list)
    {
        foreach (var item in instancedBanners)
        {
            Destroy(item.gameObject);
        }
        instancedBanners.Clear();

        maxPage = list.Count / bannersPerPage;
        curPage = Mathf.Clamp(curPage, 0, maxPage);

        int get = curPage * bannersPerPage;

        pagingText.text = "Sayfa " + (curPage + 1).ToString() + "/" + (maxPage + 1).ToString();

        for (int i = get; i < get + bannersPerPage; i++)
        {
            if (i < list.Count)
            {
                if (list[i] != null)
                {
                    MealBanner mb = Instantiate(mealBannerPrefab, mealHolder);
                    mb.InitBanner(list[i]);
                    instancedBanners.Add(mb);
                }
            }
        }

        lastQuery = list;
    }

    public void ChangePage(int inc)
    {
        curPage += inc;
        maxPage = lastQuery.Count / bannersPerPage;

        if (curPage == maxPage + 1)
            curPage = 0;
        else if (curPage == -1)
            curPage = maxPage;

        curPage = Mathf.Clamp(curPage, 0, maxPage);

        InstantiateBanners(lastQuery);
        scrollRect.verticalNormalizedPosition = 1;
    }

    public override void Show()
    {
        base.Show();

        if (!init)
        {
            init = true;
            queryMeals = new List<Meal>();
            queryMeals.AddRange(MealManager.instance.allMeals);
        }

        InstantiateBanners(queryMeals);
        scrollRect.verticalNormalizedPosition = 1;
    }
    public override void Hide()
    {
        base.Hide();
    }
}