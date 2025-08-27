using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MealInspectorMenu : IMenu
{
    public Meal mealToInspect;

    [Header("UI")]
    public Image mealImage;
    public Text mealName;
    public Text mealPortion;

    public Image favButton;

    public Text mealCalories;
    public RadialSlider carbsText;
    public RadialSlider proteinText;
    public RadialSlider lipidsText;

    public Toggle celiacToggle;
    public Toggle diabetesToggle;
    public Toggle pcosToggle;

    public Text mealTimeText;
    public Text mealTypeText;

    public Text favText;
    public Sprite favPressed;
    public Sprite favDefault;

    public IMenu lastMenu;

    public override void Show()
    {
        base.Show();

        if (mealToInspect != null)
        {
            mealImage.sprite = mealToInspect.GetMealImage();
            mealName.text = mealToInspect.Name;
            mealPortion.text = mealToInspect.Portion;
            mealCalories.text = mealToInspect.Calories.ToString();

            carbsText.value = (int)mealToInspect.Carbs;
            proteinText.value = (int)mealToInspect.Protein;
            lipidsText.value = (int)mealToInspect.Fat;

            mealTimeText.text = mealToInspect.MealTimeText;
            mealTypeText.text = mealToInspect.MealTypeText;

            diabetesToggle.isOn = mealToInspect.Diabetes == 0 ? false : true;
            celiacToggle.isOn = mealToInspect.Celiac == 0 ? false : true;
            pcosToggle.isOn = mealToInspect.PCOS == 0 ? false : true;

            if (PersonController.person.IsMealFavved(mealToInspect))
            {
                favButton.sprite = favPressed;
                favText.text = "Favorilerden çıkar";
            }
            else
            {
                favButton.sprite = favDefault;
                favText.text = "Favorilere ekle";
            }
        }
        else
        {
            UIController.instance.ShowWarning("Uygulama Hatası!", "İstenilen yemek açılırken bir sorun oluştu.", () => UIController.instance.OpenMenuButton(UIController.instance.mainMenu));
        }
    }
    public override void Hide()
    {
        base.Hide();
        mealToInspect = null;
    }

    public void MealFavButton()
    {
        if (PersonController.person.IsMealFavved(mealToInspect))
        {
            PersonController.person.favMeals.Remove(mealToInspect);
            favButton.sprite = favDefault;
        }
        else
        {
            PersonController.person.favMeals.Add(mealToInspect);
            favButton.sprite = favPressed;
        }
    }
    public void ReturnButton()
    {
        UIController.instance.OpenMenuButton(lastMenu);
    }
}