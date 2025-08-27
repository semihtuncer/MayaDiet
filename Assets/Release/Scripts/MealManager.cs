using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MealManager : MonoBehaviour
{
    public List<Meal> allMeals = new List<Meal>();
    Dictionary<int, Sprite> allMealImages = new Dictionary<int, Sprite>();

    public static MealManager instance;

    void Awake()
    {
        if (instance != null)
            return;

        allMeals = Meal.GetAllMeals();
        allMeals.Sort((x, y) => string.Compare(x.Name, y.Name));

        instance = this;
    }
    public void Initialize()
    {
        if (instance != null)
            return;

        allMeals = Meal.GetAllMeals();
        allMeals.Sort((x, y) => string.Compare(x.Name, y.Name));

        instance = this;
    }

    public Meal GetMealById(int id)
    {
        foreach (var item in allMeals)
        {
            if (item.Id == id)
                return item;
        }

        return null;
    }
    public Meal GetMealByName(string s)
    {
        foreach (var item in allMeals)
        {
            if (item.Name == s)
                return item;
        }

        return null;
    }

    public List<Meal> GetMealsByMealType(MealType mt)
    {
        return allMeals.Where(a => a.MealType == mt).ToList();
    }
    public List<Meal> GetMealsByMealTime(MealTime mt)
    {
        return allMeals.Where(a => a.MealTime == mt).ToList();
    }

    public float GetMealListCalories(List<Meal> ml)
    {
        float r = 0;
        foreach (var item in ml)
        {
            r += (float)item.Calories;
        }
        return r;
    }
    public float GetMealListCarbs(List<Meal> ml)
    {
        float r = 0;
        foreach (var item in ml)
        {
            r += (float)item.Carbs;
        }
        return r;
    }
    public float GetMealListLipid(List<Meal> ml)
    {
        float r = 0;
        foreach (var item in ml)
        {
            r += (float)item.Fat;
        }
        return r;
    }
    public float GetMealListProtein(List<Meal> ml)
    {
        float r = 0;
        foreach (var item in ml)
        {
            r += (float)item.Protein;
        }
        return r;
    }

    public string GetPrettyStringFromMealList(List<Meal> ml, string optS = "")
    {
        string r = "";
        if (string.IsNullOrEmpty(optS))
            r = "MENÜ: \n";
        else
            r = optS + "\n";

        foreach (var item in ml)
        {
            r += "- " + item.Name;

            if (!string.IsNullOrEmpty(item.Portion))
            {
                string s = item.Portion;

                s = s.Replace("(", "");
                s = s.Replace(")", "");

                r += " (" + s + ")";
            }

            r += "\n";
        }

        return r;
    }
    public string GetPrettyStringFromMenuPreset(MenuPreset mp)
    {
        string t = GetPrettyStringFromMealList(mp.breakfastMenu, "GÜNÜN MENÜSÜ: \nKAHVALTI:");
        t += "\n" + GetPrettyStringFromMealList(mp.snacksMenu, "ARA ÖGÜN:");
        t += "\n" + GetPrettyStringFromMealList(mp.lunchMenu, "ÖĞLE YEMEĞİ:");
        t += "\n" + GetPrettyStringFromMealList(mp.dinnerMenu, "AKŞAM YEMEĞİ:");

        return t;
    }
}

public static class MealManagerExtension
{
    public static List<Meal> GetListByType(this List<Meal> m, MealType t)
    {
        return m.Where(a => a.MealType == t).ToList();
    }
    public static List<Meal> GetListByTime(this List<Meal> m, MealTime t)
    {
        return m.Where(a => a.MealTime == t).ToList();
    }
    public static List<Meal> GetMealsExcept(this List<Meal> mList, List<int> idA)
    {
        return mList.Where(m => !idA.Contains(m.Id)).ToList();
    }
}
public static class ClipboardExtension
{
    public static void CopyToClipboard(this string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }
}