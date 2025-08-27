using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WaterManager : MonoBehaviour
{
    public GameObject glass;
    public Sprite emptyGlass;
    public Sprite fullGlass;

    List<GameObject> glasses = new List<GameObject>();
    bool init;

    void Update()
    {
        if (!init)
        {
            for (int i = 0; i < PersonController.person.maxGlassOfWater; i++)
            {
                GameObject temp = Instantiate(glass, transform);
                temp.GetComponent<Image>().sprite = emptyGlass;
                glasses.Add(temp);
            }

            init = true;
        }

        for (int i = 0; i < PersonController.person.currentGlassOfWater; i++)
        {
            glasses[i].GetComponent<Image>().sprite = fullGlass;
        }
        for (int i = 0; i < PersonController.person.maxGlassOfWater - PersonController.person.currentGlassOfWater; i++)
        {
            glasses[PersonController.person.maxGlassOfWater - i - 1].GetComponent<Image>().sprite = emptyGlass;
        }
    }
    void OnDisable()
    {
        for (int i = 0; i < glasses.Count; i++)
        {
            Destroy(glasses[i]);
        }

        init = false;
        glasses.Clear();
    }

    public void ChangeWaterLevel(int i)
    {
        PersonController.person.currentGlassOfWater += i;
        PersonController.person.SavePerson();
    }
}
