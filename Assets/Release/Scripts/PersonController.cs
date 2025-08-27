using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PersonController : MonoBehaviour
{
    public DailyMenuPanel dmp;
    public AllMealsMenu amm;

    [Header("PERSON")]
    public int age = 15;
    public int gender = 0; // 0 = male, 1 = female
    public int heightCM = 190;
    public float weightKG = 69;

    public float BMI;
    public float BMR;

    [Header("SICKNESS")]
    public bool hasCeliac;
    public bool hasPCOS;
    public bool hasDiabetes;

    [Header("BLOOD VALUES")]
    public float dVit;
    public float b12;
    public float insulinDesaturated;
    public float insulinSaturated;
    public float colesterol;
    public float tsh;
    public float t3;
    public float t4;
    public float iron;

    [Header("WATER")]
    public int maxGlassOfWater;
    public int currentGlassOfWater;

    [Header("FAV MEALS")]
    public List<Meal> favMeals = new List<Meal>();
    public List<MenuPreset> savedMenus = new List<MenuPreset>();

    [Header("DAILY MENU")]
    public bool selectOnlyFromSaved;
    [Range(0f, 1f)] public float breakfastCaloriePercentage;
    [Range(0f, 1f)] public float snacksCaloriePercentage;
    [Range(0f, 1f)] public float lunchCaloriePercentage;
    [Range(0f, 1f)] public float dinnerCaloriePercentage;

    public float caloriesForBreakfast;
    public float caloriesForSnacks;
    public float caloriesForLunch;
    public float caloriesForDinner;

    [Header("DIABETES MEALS")]
    public int sut;
    public int yesilelma;
    public int portakal;
    public int yogurt;
    public int tamBugdayEkmek;
    public List<int> diyabetBaklagiller;
    public List<int> baklagilsizCorbalar;

    [Header("PCOS MEALS")]
    public int meyveSuyu;
    public List<int> glisemikEndeksMeyveler;

    [Header("CELIAC MEALS")]
    public List<int> colyakCorbalar;
    public int kepekliMakarna;
    public int yumurta;

    public MenuPreset dailyMenu;
    public System.DateTime today;

    public static PersonController person;

    void Awake()
    {
        person = this;
        GetComponent<MealManager>().Initialize();
        GenerateNewMealMenu();
        LoadPerson();

        System.DateTime temp = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day);

        if (today != temp)
        {
            today = temp;
            PlayerPrefs.SetString("LASTDAY", today.ToString());

            PlayerPrefs.SetInt("WATERGLASSDRANK", 0);
            currentGlassOfWater = 0;
        }
    }
    void Update()
    {
        currentGlassOfWater = Mathf.Clamp(currentGlassOfWater, 0, maxGlassOfWater);
        favMeals = favMeals.Distinct().ToList();
    }
    void OnApplicationQuit()
    {
        SavePerson();
    }

    public void CalculateBMI()
    {
        float heightInMeters = (float)heightCM / 100f;
        BMI = (float)weightKG / Mathf.Pow(heightInMeters, 2);

        if (gender == 0)
        {
            BMR = 66.5f + (13.75f * weightKG) + (5 * heightCM) - (6.78f * age);
            BMR *= 1.2f;
        }
        else if (gender == 1)
        {
            BMR = 655.1f + (9.56f * weightKG) + (1.85f * heightCM) - (4.68f * age);
            BMR *= 1.2f;
        }

        if (BMI < 18 || BMI > 25)
        {
            UIController.instance.ShowWarning("Vücut-Kütle Endeksi Uyarısı", "Uygulama size uygun değildir, lütfen diyetisyeninize/doktorunuza danışın.", null);
        }
    }

    public bool IsMealFavved(Meal m)
    {
        foreach (var meal in favMeals)
        {
            if (meal.Name == m.Name)
                return true;
        }

        return false;
    }
    public bool IsMenuPresetSaved(MenuPreset a)
    {
        foreach (var item in savedMenus)
        {
            if (item.id == a.id)
                return true;
        }

        return false;
    }

    public void SavePerson()
    {
        PlayerPrefs.SetInt("AGE", age);
        PlayerPrefs.SetInt("GENDER", gender);
        PlayerPrefs.SetFloat("WEIGHT", weightKG);
        PlayerPrefs.SetInt("HEIGHT", heightCM);

        PlayerPrefs.SetFloat("BMI", BMI);
        PlayerPrefs.SetFloat("BMR", BMR);

        PlayerPrefs.SetInt("CELIAC", hasCeliac ? 1 : 0);
        PlayerPrefs.SetInt("PCOS", hasPCOS ? 1 : 0);
        PlayerPrefs.SetInt("DIABETES", hasDiabetes ? 1 : 0);

        PlayerPrefs.SetFloat("DVIT", dVit);
        PlayerPrefs.SetFloat("B12", b12);
        PlayerPrefs.SetFloat("DESATURATED", insulinDesaturated);
        PlayerPrefs.SetFloat("SATURATED", insulinSaturated);
        PlayerPrefs.SetFloat("COLESTEROL", colesterol);
        PlayerPrefs.SetFloat("TSH", tsh);
        PlayerPrefs.SetFloat("T3", t3);
        PlayerPrefs.SetFloat("T4", t4);
        PlayerPrefs.SetFloat("IRON", iron);

        PlayerPrefs.SetString("TODAYSMENU", JsonConvert.SerializeObject(dailyMenu));
        PlayerPrefs.SetString("SAVEDMENUS", JsonConvert.SerializeObject(savedMenus));

        caloriesForBreakfast = BMR * breakfastCaloriePercentage;
        caloriesForSnacks = BMR * snacksCaloriePercentage;
        caloriesForLunch = BMR * lunchCaloriePercentage;
        caloriesForDinner = BMR * dinnerCaloriePercentage;

        if (favMeals.Count != 0)
        {
            string saveFavMealsString = string.Empty;
            for (int i = 0; i < favMeals.Count; i++)
            {
                saveFavMealsString += favMeals[i].Id.ToString() + "|";
            }
            saveFavMealsString = saveFavMealsString.Remove(saveFavMealsString.Length - 1);
            PlayerPrefs.SetString("FAVMEALS", saveFavMealsString);
        }

        PlayerPrefs.SetString("LASTDAY", today.ToString());
        PlayerPrefs.SetInt("WATERGLASSDRANK", currentGlassOfWater);
    }
    public void LoadPerson()
    {
        age = PlayerPrefs.GetInt("AGE", 23);
        gender = PlayerPrefs.GetInt("GENDER", 0);
        weightKG = PlayerPrefs.GetFloat("WEIGHT", 65);
        heightCM = PlayerPrefs.GetInt("HEIGHT", 190);

        BMI = PlayerPrefs.GetFloat("BMI", 18f);
        BMR = PlayerPrefs.GetFloat("BMR", 1800f);

        hasCeliac = PlayerPrefs.GetInt("CELIAC", 0) == 1 ? true : false;
        hasPCOS = PlayerPrefs.GetInt("PCOS", 0) == 1 ? true : false;
        hasDiabetes = PlayerPrefs.GetInt("DIABETES", 0) == 1 ? true : false;

        dVit = PlayerPrefs.GetFloat("DVIT");
        b12 = PlayerPrefs.GetFloat("B12");
        insulinDesaturated = PlayerPrefs.GetFloat("DESATURATED");
        insulinSaturated = PlayerPrefs.GetFloat("SATURATED");
        colesterol = PlayerPrefs.GetFloat("COLESTEROL");
        tsh = PlayerPrefs.GetFloat("TSH");
        t3 = PlayerPrefs.GetFloat("T3");
        t4 = PlayerPrefs.GetFloat("T4");
        iron = PlayerPrefs.GetFloat("IRON");

        dailyMenu = JsonConvert.DeserializeObject<MenuPreset>(PlayerPrefs.GetString("TODAYSMENU", ""));

        savedMenus = JsonConvert.DeserializeObject<List<MenuPreset>>(PlayerPrefs.GetString("SAVEDMENUS", ""));

        if (savedMenus == null)
            savedMenus = new List<MenuPreset>();

        caloriesForBreakfast = BMR * breakfastCaloriePercentage;
        caloriesForSnacks = BMR * snacksCaloriePercentage;
        caloriesForLunch = BMR * lunchCaloriePercentage;
        caloriesForDinner = BMR * dinnerCaloriePercentage;

        string[] favMealsLoad = PlayerPrefs.GetString("FAVMEALS", "").Split('|');
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("FAVMEALS", "")) && !string.IsNullOrWhiteSpace(PlayerPrefs.GetString("FAVMEALS", "")))
        {
            for (int i = 0; i < favMealsLoad.Length; i++)
            {
                favMeals.Add(MealManager.instance.GetMealById(int.Parse(favMealsLoad[i])));
            }
        }

        today = System.DateTime.Parse(PlayerPrefs.GetString("LASTDAY", System.DateTime.MinValue.ToString()));
        currentGlassOfWater = PlayerPrefs.GetInt("WATERGLASSDRANK", 0);
    }

    public void GenerateNewMealMenu()
    {
        if (selectOnlyFromSaved && (hasCeliac || hasDiabetes || hasPCOS))
        {
            List<MenuPreset> diabetesMP = new List<MenuPreset>();
            List<MenuPreset> celiacMP = new List<MenuPreset>();
            List<MenuPreset> pcosMP = new List<MenuPreset>();

            foreach (var sm in savedMenus)
            {
                if (sm.menuName.ToLower().Contains("diyabet"))
                    diabetesMP.Add(sm);
                if (sm.menuName.ToLower().Contains("çölyak"))
                    celiacMP.Add(sm);
                if (sm.menuName.ToLower().Contains("pcos"))
                    pcosMP.Add(sm);
            }
    

            if (hasDiabetes)
                dailyMenu = diabetesMP[Random.Range(0, diabetesMP.Count)];
            if (hasCeliac)
                dailyMenu = celiacMP[Random.Range(0, celiacMP.Count)];
            if (hasPCOS)
                dailyMenu = pcosMP[Random.Range(0, pcosMP.Count)];

            dmp.InitBanners();
            return;
        }

        dailyMenu = new MenuPreset();

        List<Meal> cheeses = MealManager.instance.GetMealsByMealType(MealType.Peynir);
        List<Meal> bakeries = MealManager.instance.GetMealsByMealType(MealType.UnluMamül);
        List<Meal> vegies = MealManager.instance.GetMealsByMealType(MealType.Sebze);
        List<Meal> fruits = MealManager.instance.GetMealsByMealType(MealType.Meyve);
        List<Meal> nuts = MealManager.instance.GetMealsByMealType(MealType.Kuruyemiş);
        List<Meal> sweets = MealManager.instance.GetMealsByMealType(MealType.Tatlı);
        List<Meal> drinks = MealManager.instance.GetMealsByMealType(MealType.İçecek).GetListByTime(MealTime.Sabah);
        List<Meal> proteins = MealManager.instance.GetMealsByMealType(MealType.EtYemeği);
        List<Meal> salads = MealManager.instance.GetMealsByMealType(MealType.Salata);

        if (hasDiabetes || hasPCOS)
        {
            fruits = fruits.GetMealsExcept(glisemikEndeksMeyveler);
        }

        caloriesForBreakfast = BMR * breakfastCaloriePercentage;
        caloriesForSnacks = BMR * snacksCaloriePercentage;
        caloriesForLunch = BMR * lunchCaloriePercentage;
        caloriesForDinner = BMR * dinnerCaloriePercentage;

        #region breakfast
        // BREAKFAST
        float breakfastCalorie = 0;
        int tries1 = 0;
        int putCheese = 0;
        int putVegies = 0;
        bool hasBakery = false;
        bool hasProteinForBreakfast = false;
        bool hasMilkForBreakfast = false;
        bool dontPutProteinToBreakfast = Random.Range(0, 4) == 0;

        // put drinks
        if (hasPCOS)
        {
            breakfastCalorie += (float)MealManager.instance.GetMealById(meyveSuyu).Calories;
            dailyMenu.breakfastMenu.Add(MealManager.instance.GetMealById(meyveSuyu));
            drinks.Remove(MealManager.instance.GetMealById(meyveSuyu));
            tries1++;
        }
        else if (Random.Range(0, 2) == 0)
        {
            Meal drink = new Meal();
            if (drinks != null && drinks.Count != 0)
                drink = drinks[Random.Range(0, drinks.Count)];

            if (drink != null)
            {
                if ((drink.Diabetes == 0 && !hasDiabetes) || (drink.Celiac == 0 && !hasCeliac) || (drink.PCOS == 0 && !hasPCOS))
                {
                    if (drink.Id == sut)
                        hasMilkForBreakfast = true;

                    breakfastCalorie += (float)drink.Calories;
                    dailyMenu.breakfastMenu.Add(drink);
                    drinks.Remove(drink);
                    tries1++;
                }
            }
        }

        if (Random.Range(0, 2) == 0 || hasCeliac)
        {
            dailyMenu.breakfastMenu.Add(MealManager.instance.GetMealById(yumurta));
            breakfastCalorie += (float)MealManager.instance.GetMealById(yumurta).Calories;
            hasProteinForBreakfast = true;
        }

        while (breakfastCalorie < caloriesForBreakfast)
        {
            tries1++;

            if (tries1 == 5)
                break;

            #region put protein based
            if (!dontPutProteinToBreakfast)
            {
                if (!hasProteinForBreakfast)
                {
                    List<Meal> a = proteins.GetListByTime(MealTime.Sabah);
                    Meal protein = new Meal();
                    if (a != null && a.Count != 0)
                        protein = a[Random.Range(0, a.Count)];

                    if (protein != null)
                    {
                        if ((protein.Diabetes == 0 && !hasDiabetes) || (protein.Celiac == 0 && !hasCeliac) || (protein.PCOS == 0 && !hasPCOS))
                        {
                            breakfastCalorie += (float)protein.Calories;
                            dailyMenu.breakfastMenu.Add(protein);
                            proteins.Remove(protein);

                            hasProteinForBreakfast = true;

                            if (breakfastCalorie >= caloriesForBreakfast)
                                break;
                        }
                    }
                }
            }
            #endregion
            #region put cheese
            if (putCheese < 2)
            {
                Meal cheese = new Meal();
                if (cheeses != null && cheeses.Count != 0)
                    cheese = cheeses[Random.Range(0, cheeses.Count)];

                if (cheese != null)
                {
                    if ((cheese.Diabetes == 0 && !hasDiabetes) || (cheese.Celiac == 0 && !hasCeliac) || (cheese.PCOS == 0 && !hasPCOS))
                    {
                        breakfastCalorie += (float)cheese.Calories;
                        dailyMenu.breakfastMenu.Add(cheese);
                        cheeses.Remove(cheese);

                        putCheese++;

                        if (breakfastCalorie >= caloriesForBreakfast)
                            break;
                    }
                }
            }
            #endregion
            #region put vegies
            if (putVegies < 2)
            {
                Meal veg = new Meal();
                if (vegies != null && vegies.Count != 0)
                    veg = vegies[Random.Range(0, vegies.Count)];

                if (veg != null)
                {
                    if ((veg.Diabetes == 0 && !hasDiabetes) || (veg.Celiac == 0 && !hasCeliac) || (veg.PCOS == 0 && !hasPCOS))
                    {
                        breakfastCalorie += (float)veg.Calories;
                        dailyMenu.breakfastMenu.Add(veg);
                        vegies.Remove(veg);
                        putVegies++;

                        if (breakfastCalorie >= caloriesForBreakfast)
                            break;
                    }
                }
            }
            #endregion
            #region put bakery
            if (!hasDiabetes && !hasCeliac)
            {
                if (!hasBakery)
                {
                    Meal bake = new Meal();
                    List<Meal> a = bakeries.GetListByTime(MealTime.Sabah);
                    if (bakeries != null && bakeries.Count != 0)
                        bake = a[Random.Range(0, a.Count)];

                    if (bake != null)
                    {
                        if ((bake.Diabetes == 0 && !hasDiabetes) || (bake.Celiac == 0 && !hasCeliac) || (bake.PCOS == 0 && !hasPCOS))
                        {
                            breakfastCalorie += (float)bake.Calories;
                            dailyMenu.breakfastMenu.Add(bake);
                            bakeries.Remove(bake);

                            hasBakery = true;

                            if (breakfastCalorie >= caloriesForBreakfast)
                                break;
                        }
                    }
                }
            }
            #endregion
        }
        #endregion

        #region snacks
        // SNACKS
        float snacksCalorie = 0;
        int tries2 = 0;
        int putSweet = Random.Range(0, 2);

        // put necessity meals
        if (hasDiabetes || hasPCOS)
        {
            if (Random.Range(0, 2) == 0)
            {
                Meal fruit = new Meal();
                if (fruits != null && fruits.Count != 0)
                    fruit = fruits[Random.Range(0, fruits.Count)]; ;

                if (hasDiabetes)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        dailyMenu.snacksMenu.Add(MealManager.instance.GetMealById(yesilelma));
                        snacksCalorie += (float)MealManager.instance.GetMealById(yesilelma).Calories;
                    }
                    else
                    {
                        dailyMenu.snacksMenu.Add(MealManager.instance.GetMealById(portakal));
                        snacksCalorie += (float)MealManager.instance.GetMealById(portakal).Calories;
                    }
                    tries2++;
                }
                else
                {
                    if (fruit != null)
                    {
                        if ((fruit.Diabetes == 0 && !hasDiabetes) || (fruit.Celiac == 0 && !hasCeliac) || (fruit.PCOS == 0 && !hasPCOS))
                        {
                            snacksCalorie += (float)fruit.Calories;
                            dailyMenu.snacksMenu.Add(fruit);
                            fruits.Remove(fruit);
                            tries2++;
                        }
                    }
                }

                if (!hasMilkForBreakfast)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        dailyMenu.snacksMenu.Add(MealManager.instance.GetMealById(sut));
                        snacksCalorie += (float)MealManager.instance.GetMealById(sut).Calories;
                    }
                    else
                    {
                        dailyMenu.snacksMenu.Add(MealManager.instance.GetMealById(yogurt));
                        snacksCalorie += (float)MealManager.instance.GetMealById(yogurt).Calories;
                    }
                    tries2++;
                }
                else if (Random.Range(0, 2) == 0)
                {
                    dailyMenu.snacksMenu.Add(MealManager.instance.GetMealById(yogurt));
                    snacksCalorie += (float)MealManager.instance.GetMealById(yogurt).Calories;
                    tries2++;
                }
            }
        }
        else if (putSweet == 0) // put sweet
        {
            if (!hasCeliac && !hasPCOS)
            {
                Meal sweet = new Meal();
                if (sweets != null && sweets.Count != 0)
                    sweet = sweets[Random.Range(0, sweets.Count)];

                if (sweet != null)
                {
                    if ((sweet.Diabetes == 0 && !hasDiabetes) || (sweet.Celiac == 0 && !hasCeliac) || (sweet.PCOS == 0 && !hasPCOS))
                    {
                        snacksCalorie += (float)sweet.Calories;
                        dailyMenu.snacksMenu.Add(sweet);
                        sweets.Remove(sweet);
                    }
                }
            }
        }
        while (snacksCalorie < caloriesForSnacks)
        {
            if (putSweet == 0)
            {
                if (!hasPCOS && !hasCeliac && !hasDiabetes)
                {
                    break;
                }
            }

            tries2++;

            if (tries2 == 3)
                break;

            #region put nuts
            Meal nut = new Meal();
            if (nuts != null && nuts.Count != 0)
                nut = nuts[Random.Range(0, nuts.Count)];

            if (nut != null)
            {
                if ((nut.Diabetes == 0 && !hasDiabetes) || (nut.Celiac == 0 && !hasCeliac) || (nut.PCOS == 0 && !hasPCOS))
                {
                    snacksCalorie += (float)nut.Calories;
                    dailyMenu.snacksMenu.Add(nut);
                    nuts.Remove(nut);

                    if (snacksCalorie >= caloriesForSnacks)
                        break;
                }
            }
            #endregion
            #region put fruits
            Meal fruit = new Meal();
            if (fruits != null && fruits.Count != 0)
                fruit = fruits[Random.Range(0, fruits.Count)];

            if (fruit != null)
            {
                if ((fruit.Diabetes == 0 && hasDiabetes) || (fruit.Celiac == 0 && hasCeliac) || (fruit.PCOS == 0 && hasPCOS))
                {
                    snacksCalorie += (float)fruit.Calories;
                    dailyMenu.snacksMenu.Add(fruit);
                    fruits.Remove(fruit);

                    if (snacksCalorie >= caloriesForSnacks)
                        break;
                }
            }
            #endregion
        }
        #endregion

        #region lunch
        // LUNCH
        int tries3 = 0;
        float list1Calorie = 0;
        bool list1Lunch = Random.Range(0, 2) == 0;
        bool list1AddedPasta = false;
        List<Meal> list1 = new List<Meal>();

        #region add soup
        Meal soup = new Meal();
        soup = MealManager.instance.GetMealById(baklagilsizCorbalar[Random.Range(0, baklagilsizCorbalar.Count)]);

        if (soup != null)
        {
            if ((soup.Diabetes == 0 && !hasDiabetes) || (soup.Celiac == 0 && !hasCeliac) || (soup.PCOS == 0 && !hasPCOS))
            {
                list1Calorie += (float)soup.Calories;
                list1.Add(soup);
                tries3++;
            }
        }
        #endregion
        #region add main
        if (hasCeliac)
        {
            if (Random.Range(0, 4) == 0)
            {
                Meal mak = MealManager.instance.GetMealById(kepekliMakarna);
                list1Calorie += (float)mak.Calories;
                list1.Add(mak);
                list1AddedPasta = true;
            }
        }

        if (!list1AddedPasta)
        {
            Meal bean = new Meal();
            bean = MealManager.instance.GetMealById(diyabetBaklagiller[Random.Range(0, diyabetBaklagiller.Count)]);

            if (bean != null)
            {
                if ((bean.Diabetes == 0 && !hasDiabetes) || (bean.Celiac == 0 && !hasCeliac) || (bean.PCOS == 0 && !hasPCOS))
                {
                    list1Calorie += (float)bean.Calories;
                    list1.Add(bean);
                    tries3++;
                }
            }
        }
        #endregion
        #region add bread

        if (list1Calorie < (list1Lunch ? caloriesForLunch : caloriesForDinner) && Random.Range(0, 2) == 0 && !list1AddedPasta)
        {
            list1Calorie += (float)MealManager.instance.GetMealById(tamBugdayEkmek).Calories;
            list1.Add(MealManager.instance.GetMealById(tamBugdayEkmek));
            tries3++;
        }
        #endregion
        #region add salad
        Meal salad = new Meal();
        salad = salads[Random.Range(0, salads.Count)];

        if (salad != null)
        {
            if ((salad.Diabetes == 0 && !hasDiabetes) || (salad.Celiac == 0 && !hasCeliac) || (salad.PCOS == 0 && !hasPCOS))
            {
                list1Calorie += (float)salad.Calories;
                list1.Add(salad);
                tries3++;
            }
        }
        #endregion
        #endregion

        #region dinner
        List<Meal> list2 = new List<Meal>();

        Meal pro = new Meal();
        List<Meal> lunchProteins = proteins.GetListByTime(MealTime.ÖğleAkşam);
        pro = lunchProteins[Random.Range(0, lunchProteins.Count)];

        if (pro != null)
        {
            if ((pro.Diabetes == 0 && !hasDiabetes) || (pro.Celiac == 0 && !hasCeliac) || (pro.PCOS == 0 && !hasPCOS))
            {
                list1Calorie += (float)pro.Calories;
                list2.Add(pro);
                tries3++;
            }
        }

        if (list1Calorie < (list1Lunch ? caloriesForLunch : caloriesForDinner) && Random.Range(0, 2) == 0)
        {
            list1Calorie += (float)MealManager.instance.GetMealById(tamBugdayEkmek).Calories;
            list2.Add(MealManager.instance.GetMealById(tamBugdayEkmek));
            tries3++;
        }
        #endregion

        if (list1Lunch)
        {
            dailyMenu.lunchMenu = list1;
            dailyMenu.dinnerMenu = list2;
        }
        else
        {
            dailyMenu.dinnerMenu = list1;
            dailyMenu.lunchMenu = list2;
        }

        dmp.InitBanners();
    }
}