using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

public class TipBoxController : MonoBehaviour
{
    [Header("TIPS")]
    public Text tipText;
    public List<string> tipToSelect;
    public List<string> celiacTip;
    public List<string> pcosTip;
    public List<string> diabetesTip;

    string selectedString;

    void OnEnable()
    {
        SelectTip();
    }

    void SelectTip()
    {
        List<string> select = new List<string>();

        if(Random.Range(0, 2) == 0)
        {
            if (PersonController.person.hasCeliac)
                select = celiacTip;
            else if (PersonController.person.hasDiabetes)
                select = diabetesTip;
            else if (PersonController.person.hasPCOS)
                select = pcosTip;
            else
                select = tipToSelect;
        }
        else
        {
            select = tipToSelect;
        }

        string s = select[Random.Range(0, select.Count)];

        
        if(s != selectedString)
        {
            selectedString = s;
            tipText.text = selectedString;
        }
        else
        {
            SelectTip();
        }
    }
}