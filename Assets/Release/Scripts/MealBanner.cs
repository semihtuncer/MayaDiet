using UnityEngine;
using UnityEngine.UI;

public class MealBanner : MonoBehaviour
{
    public Text nameText;
    public Text caloriesText;

    public Sprite favButtonDefault;
    public Sprite favButtonPressed;

    public Image mealImage;
    public Image favButtonImage;

    public Meal bannerMeal;

    public void InitBanner(Meal m)
    {
        if (m != null)
        {
            bannerMeal = m;

            nameText.text = m.Name;
            caloriesText.text = ((int)m.Calories).ToString() + " kcal";

            mealImage.sprite = m.GetMealImage();
            gameObject.name = m.Name + " Banner";

            if (PersonController.person.IsMealFavved(bannerMeal))
            {
                favButtonImage.sprite = favButtonPressed;
            }
            else
            {
                favButtonImage.sprite = favButtonDefault;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void MealFavButton()
    {
        if (PersonController.person.IsMealFavved(bannerMeal))
        {
            PersonController.person.favMeals.Remove(bannerMeal);
            favButtonImage.sprite = favButtonDefault;
        }
        else
        {
            PersonController.person.favMeals.Add(bannerMeal);
            favButtonImage.sprite = favButtonPressed;
        }
    }
    public void MealInspectButton()
    {
        ((MealInspectorMenu)UIController.instance.inspectMealMenu).mealToInspect = bannerMeal;
        ((MealInspectorMenu)UIController.instance.inspectMealMenu).lastMenu = UIController.instance.currentMenu;
        UIController.instance.OpenMenuButton(UIController.instance.inspectMealMenu);
    }

    void OnEnable()
    {
        if (bannerMeal == null)
            return;

        if (PersonController.person.IsMealFavved(bannerMeal))
        {
            favButtonImage.sprite = favButtonPressed;
        }
        else
        {
            favButtonImage.sprite = favButtonDefault;
        }
    }
}