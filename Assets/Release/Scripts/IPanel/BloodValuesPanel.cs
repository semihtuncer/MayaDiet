using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodValuesPanel : IPanel
{
    [Header("UI")]
    public InputField dVitIF;
    public InputField b12IF;
    public InputField desaturatedIF;
    public InputField saturatedIF;
    public InputField colesterolIF;
    public InputField tshIF;
    public InputField t3IF;
    public InputField t4IF;
    public InputField ironIF;
    public Button continueButton;

    [Header("NORMAL RANGE")]
    public Range dVitNR;
    public Range b12NR;
    public Range desaturatedNR;
    public Range saturatedNR;
    public Range colesterolNR;
    public Range tshNR;
    public Range t3NR;
    public Range t4NR;
    public Range ironNR;

    void Start()
    {
        dVitIF.text = PersonController.person.dVit.ToString();
        b12IF.text = PersonController.person.b12.ToString();
        desaturatedIF.text = PersonController.person.insulinDesaturated.ToString();
        saturatedIF.text = PersonController.person.insulinSaturated.ToString();
        colesterolIF.text = PersonController.person.colesterol.ToString();
        tshIF.text = PersonController.person.tsh.ToString();
        t3IF.text = PersonController.person.t3.ToString();
        t4IF.text = PersonController.person.t4.ToString();
        ironIF.text = PersonController.person.iron.ToString();

        dVitIF.text = ValidateFloat(dVitIF.text);
        b12IF.text = ValidateFloat(b12IF.text);
        desaturatedIF.text = ValidateFloat(desaturatedIF.text);
        saturatedIF.text = ValidateFloat(saturatedIF.text);
        colesterolIF.text = ValidateFloat(colesterolIF.text);
        tshIF.text = ValidateFloat(tshIF.text);
        t3IF.text = ValidateFloat(t3IF.text);
        t4IF.text = ValidateFloat(t4IF.text);
        ironIF.text = ValidateFloat(ironIF.text);
    }
    void Update()
    {
        if (float.Parse(ValidateFloat(dVitIF.text)) == 0 || float.Parse(ValidateFloat(b12IF.text)) == 0 || float.Parse(ValidateFloat(desaturatedIF.text)) == 0 || float.Parse(ValidateFloat(saturatedIF.text)) == 0 || float.Parse(ValidateFloat(colesterolIF.text)) == 0 || float.Parse(ValidateFloat(tshIF.text)) == 0 || float.Parse(ValidateFloat(t3IF.text)) == 0 || float.Parse(ValidateFloat(t4IF.text)) == 0 || float.Parse(ValidateFloat(ironIF.text)) == 0)
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        }
    }

    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }

    string ValidateFloat(string s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
            return "0";

        string r = "0";
        try
        {
            r = float.Parse(s).ToString("f2");
        }
        catch (System.Exception)
        {

        }
        return r;
    }

    public void OnIFChange()
    {
        try
        {
            dVitIF.text = ValidateFloat(dVitIF.text);
            b12IF.text = ValidateFloat(b12IF.text);
            desaturatedIF.text = ValidateFloat(desaturatedIF.text);
            saturatedIF.text = ValidateFloat(saturatedIF.text);
            colesterolIF.text = ValidateFloat(colesterolIF.text);
            tshIF.text = ValidateFloat(tshIF.text);
            t3IF.text = ValidateFloat(t3IF.text);
            t4IF.text = ValidateFloat(t4IF.text);
            ironIF.text = ValidateFloat(ironIF.text);

            PersonController.person.dVit = float.Parse(dVitIF.text);
            PersonController.person.b12 = float.Parse(b12IF.text);
            PersonController.person.insulinDesaturated = float.Parse(desaturatedIF.text);
            PersonController.person.insulinSaturated = float.Parse(saturatedIF.text);
            PersonController.person.colesterol = float.Parse(colesterolIF.text);
            PersonController.person.tsh = float.Parse(tshIF.text);
            PersonController.person.t3 = float.Parse(t3IF.text);
            PersonController.person.t4 = float.Parse(t4IF.text);
            PersonController.person.iron = float.Parse(ironIF.text);
        }
        catch
        {
            UIController.instance.ShowWarning("Veri Hatası!", "Girdiğiniz veri hatalı.", null);
        }

        PersonController.person.SavePerson();
    }
    public void OnContinueButton()
    {
        if (float.Parse(ValidateFloat(dVitIF.text)) < dVitNR.min || float.Parse(ValidateFloat(dVitIF.text)) > dVitNR.max)
        {
            UIController.instance.ShowWarning("Kan Değeri Hatası", $"D vitamini normal aralık ({dVitNR.min}-{dVitNR.max}), uygulamamız size uygun değildir.", null);
        }
        if (float.Parse(ValidateFloat(b12IF.text)) < b12NR.min || float.Parse(ValidateFloat(b12IF.text)) > b12NR.max)
        {
            UIController.instance.ShowWarning("Kan Değeri Hatası", $"B12 normal aralık ({b12NR.min}-{b12NR.max}), uygulamamız size uygun değildir.", null);
        }
        if (float.Parse(ValidateFloat(desaturatedIF.text)) < desaturatedNR.min || float.Parse(ValidateFloat(desaturatedIF.text)) > desaturatedNR.max)
        {
            UIController.instance.ShowWarning("Kan Değeri Hatası", $"Açlık insülin normal aralık ({desaturatedNR.min}-{desaturatedNR.max}), uygulamamız size uygun değildir.", null);
        }
        if (float.Parse(ValidateFloat(saturatedIF.text)) < saturatedNR.min || float.Parse(ValidateFloat(saturatedIF.text)) > saturatedNR.max)
        {
            UIController.instance.ShowWarning("Kan Değeri Hatası", $"Tokluk insülin normal aralık ({saturatedNR.min}-{saturatedNR.max}), uygulamamız size uygun değildir.", null);
        }
        if (float.Parse(ValidateFloat(colesterolIF.text)) < colesterolNR.min || float.Parse(ValidateFloat(colesterolIF.text)) > colesterolNR.max)
        {
            UIController.instance.ShowWarning("Kan Değeri Hatası", $"Kolesterol normal aralık ({colesterolNR.min}-{colesterolNR.max}), uygulamamız size uygun değildir.", null);
        }
        if (float.Parse(ValidateFloat(tshIF.text)) < tshNR.min || float.Parse(ValidateFloat(tshIF.text)) > tshNR.max)
        {
            UIController.instance.ShowWarning("Kan Değeri Hatası", $"TSH normal aralık ({tshNR.min}-{tshNR.max}), uygulamamız size uygun değildir.", null);
        }
        if (float.Parse(ValidateFloat(t3IF.text)) < t3NR.min || float.Parse(ValidateFloat(t3IF.text)) > t3NR.max)
        {
            UIController.instance.ShowWarning("Kan Değeri Hatası", $"T3 normal aralık ({t3NR.min}-{t3NR.max}), uygulamamız size uygun değildir.", null);
        }
        if (float.Parse(ValidateFloat(t4IF.text)) < t4NR.min || float.Parse(ValidateFloat(t4IF.text)) > t4NR.max)
        {
            UIController.instance.ShowWarning("Kan Değeri Hatası", $"T4 normal aralık ({t4NR.min}-{t4NR.max}), uygulamamız size uygun değildir.", null);
        }
        if (float.Parse(ValidateFloat(ironIF.text)) < ironNR.min || float.Parse(ValidateFloat(ironIF.text)) > ironNR.max)
        {
            UIController.instance.ShowWarning("Kan Değeri Hatası", $"Demir normal aralık ({ironNR.min}-{ironNR.max}), uygulamamız size uygun değildir.", null);
        }

        PlayerPrefs.SetInt("SETUPCOMPLETE", 1);
    }
}

[System.Serializable]
public class Range
{
    public float min;
    public float max;
}