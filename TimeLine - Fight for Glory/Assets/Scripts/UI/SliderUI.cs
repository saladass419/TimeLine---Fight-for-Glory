using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    private Text displayValue;
    private Slider slider;
    public static  bool isValueSet = false;
    public static int value;
    public void SetSliderValue()
    {
        value = (int)slider.value;
        ReturnValue();
    }
    public void SetValueToMax()
    {
        value = (int)slider.maxValue;
        ReturnValue();
    }
    public void SetBasics(int maxValue)
    {
        if (maxValue == 1)
        {
            value = 1;
            ReturnValue();
            return;
        }
        slider = gameObject.GetComponent<Slider>();
        displayValue = gameObject.GetComponentInChildren<Text>();
        slider.wholeNumbers = true;
        slider.maxValue = maxValue;
        slider.value = 0;
        isValueSet = false;
    }
    private void Update()
    {
        displayValue.text = slider.value.ToString();
    }
    public void ReturnValue()
    {
        isValueSet = true;
        gameObject.SetActive(false);
    }
}
