using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInspectorMenu : IMenu
{
    [Header("SETUP")]
    public string menuName;
    public List<Meal> menuToInspect;

    [Header("UI")]
    public Text titleText;
    public Transform mealListHolder;
    public MealBanner mealBanner;
    public RadialSlider carbsSlider;
    public RadialSlider proteinSlider;
    public RadialSlider lipidsSlider;
    public Text calorieText;

    List<MealBanner> spawned = new List<MealBanner>();

    public override void Show()
    {
        base.Show();

        if (menuToInspect.Count == 0 || menuToInspect == null)
        {
            UIController.instance.ShowWarning("Hata!", "Beklenmedik bir hata oluştu.", () => UIController.instance.OpenMenuButton(UIController.instance.lastMenu));
            return;
        }

        titleText.text = menuName;

        foreach (var item in menuToInspect)
        {
            MealBanner mb = Instantiate(mealBanner, mealListHolder);
            mb.InitBanner(item);
            spawned.Add(mb);
        }

        float totalCarbs = MealManager.instance.GetMealListCarbs(menuToInspect);
        float totalProtein = MealManager.instance.GetMealListProtein(menuToInspect);
        float totalLipids = MealManager.instance.GetMealListLipid(menuToInspect);

        float totalNutrient = totalCarbs + totalLipids + totalProtein;

        calorieText.text = MealManager.instance.GetMealListCalories(menuToInspect).ToString();

        carbsSlider.value = Mathf.RoundToInt((totalCarbs / totalNutrient) * 100);
        proteinSlider.value = Mathf.RoundToInt((totalProtein / totalNutrient) * 100);
        lipidsSlider.value = Mathf.RoundToInt((totalLipids / totalNutrient) * 100);
    }
    public override void Hide()
    {
        base.Hide();

        foreach (var item in spawned)
        {
            Destroy(item.gameObject);
        }
        spawned = new List<MealBanner>();
    }

    public void CopyAsText()
    {
        MealManager.instance.GetPrettyStringFromMealList(menuToInspect, menuName.ToUpper() + ":").CopyToClipboard();
        UIController.instance.ShowWarning("", "<size=55> Kopyalandı </size> \n\n", null);
    }
}