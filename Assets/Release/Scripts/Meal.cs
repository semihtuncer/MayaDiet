using Newtonsoft.Json;

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Meal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Portion { get; set; }
    public string Description { get; set; }

    public MealTime MealTime { get; set; }
    public MealType MealType { get; set; }
    public string MealTimeText { get; set; }
    public string MealTypeText { get; set; }

    public double Calories { get; set; }
    public double Carbs { get; set; }
    public double Protein { get; set; }
    public double Fat { get; set; }

    // 0 no data, 1 should eat, 2 shouldn't eat
    public int Diabetes { get; set; }
    public int PCOS { get; set; }
    public int Celiac { get; set; }
    public int HyperThyroidism { get; set; }

    public string DiabetesText { get; set; }
    public string CeliacText { get; set; }
    public string PCOSText { get; set; }
    public string HyperThyroidismText { get; set; }

    public Sprite GetMealImage()
    {
        return Resources.Load<Sprite>("mealImages/" + this.Id.ToString());
    }
    public static Sprite GetMealImageById(int id)
    {
        return Resources.Load<Sprite>("mealImages/" + id.ToString());
    }
    public static List<Meal> GetAllMeals()
    {
        var jsonText = Resources.Load<TextAsset>("meals");

        return JsonConvert.DeserializeObject<List<Meal>>(jsonText.ToString());
    }
}

public enum MealTime
{
    Sabah,
    ÖğleAkşam, 
    Ara,
    SabahYaDaÖğleAkşam,
    HerZaman
}
public enum MealType
{
    Baklagil,
    EtYemeği,
    Meyve,
    Sebze,
    SütÜrünü,
    UnluMamül,
    Kuruyemiş,
    Tatlı,
    Kahvaltılık,
    Çorba,
    İçecek,
    Peynir,
    Salata
}