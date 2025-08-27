using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MenuPreset
{
    public string menuName;
    public string id;

    public List<Meal> breakfastMenu = new List<Meal>();
    public List<Meal> snacksMenu = new List<Meal>();
    public List<Meal> lunchMenu = new List<Meal>();
    public List<Meal> dinnerMenu = new List<Meal>();
}