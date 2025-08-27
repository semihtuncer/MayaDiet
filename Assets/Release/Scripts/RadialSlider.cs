using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialSlider : MonoBehaviour
{
    public int value;
    public int maxValue;

    public Image radial;
    public Transform endPoint;

    public Text text;
    public string prefix;
    public string suffix;

    int oldV = -1;

    void Update()
    {
        if (oldV != value)
        {
            oldV = value;
            value = Mathf.Clamp(value, 0, maxValue);
            text.text = prefix + value.ToString() + suffix;
            MoveSlider();
        }
    }

    public void MoveSlider()
    {
        radial.fillAmount = (float)value / maxValue;
        endPoint.eulerAngles = new Vector3(0, 0, -(360 * ((float)value / maxValue) - 180));
    }
}
