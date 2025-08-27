using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SicknessSelection : MonoBehaviour
{
    #region Variables
    public int maxSickness;
    int currentSelected;

    [Header("Stats")]
    public bool hasGut;
    public bool hasTiroid;
    public bool hasDiabetes;
    public bool hasHypertension;

    [Header("UI")]
    public Toggle gutToggle;
    public Toggle tiroidToggle;
    public Toggle diabetesToggle;
    public Toggle hypertensionToggle;
    #endregion

    void OnEnable()
    {
        hasGut = PlayerPrefs.GetInt("GUT", 0) == 1 ? true : false;
        hasTiroid = PlayerPrefs.GetInt("TIROID", 0) == 1 ? true : false;
        hasDiabetes = PlayerPrefs.GetInt("DIABETES", 0) == 1 ? true : false;
        hasHypertension = PlayerPrefs.GetInt("HYPERTENSION", 0) == 1 ? true : false;

        gutToggle.isOn = hasGut;
        tiroidToggle.isOn = hasTiroid;
        diabetesToggle.isOn = hasDiabetes;
        hypertensionToggle.isOn = hasHypertension;
    }
    void Update()
    {
        List<bool> bArray = new List<bool> { gutToggle.isOn, tiroidToggle.isOn, diabetesToggle.isOn, hypertensionToggle.isOn};

        currentSelected = bArray.Where(a => a).Count();

        if(currentSelected <= maxSickness)
        {
            hasGut = gutToggle.isOn;
            hasTiroid = tiroidToggle.isOn;
            hasDiabetes = diabetesToggle.isOn;
            hasHypertension = hypertensionToggle.isOn;
        }
    }

    public void OnToggle(Toggle t)
    {
        if(currentSelected >= maxSickness)
        {
            t.isOn = false;
        }
    }
    public void OnContinueButtonClick()
    {
        PlayerPrefs.SetInt("GUT", hasGut ? 1 : 0);
        PlayerPrefs.SetInt("TIROID", hasTiroid ? 1 : 0);
        PlayerPrefs.SetInt("DIABETES", hasDiabetes ? 1 : 0);
        PlayerPrefs.SetInt("HYPERTENSION", hasHypertension ? 1 : 0);
    }
}