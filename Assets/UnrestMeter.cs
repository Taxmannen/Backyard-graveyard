using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnrestMeter : MonoBehaviour
{
    [SerializeField] private Sprite neutralSprite;
    [SerializeField] private Sprite happySprite;
    [SerializeField] private Sprite angrySprite;

    [SerializeField] private Image ghostFaceImage;
    [SerializeField] private Slider meterValue;

    private int unrestValue;


    public void UpdateUnrestMeter(int unrest)
    {
        unrestValue = unrest;
        SetMeterValue();
    }

    private void SetMeterValue()
    {
        SetCorrectSprite();
        SetSpritePosition();
    }

    private void SetCorrectSprite()
    {
        if (unrestValue < 6) ghostFaceImage.sprite = happySprite;
        else if (unrestValue > 14) ghostFaceImage.sprite = angrySprite;
        else ghostFaceImage.sprite = neutralSprite;
    }

    private void SetSpritePosition()
    {
        meterValue.value = unrestValue;
    }
}
