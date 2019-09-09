using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <author>Kristoffer</author>
/// </summary>
public class TaskCard : MonoBehaviour
{
    public Image headImage;
    public Image bodyImage;

    public Image OrnamentTypelot01Image;
    public Image OrnamentTypelot02Image;
    public Image OrnamentTypelot03Image;

    public Sprite[] HeadTypeprites;
    public Sprite[] bodySprites;
    public Sprite[] OrnamentTypeprites;

    [SerializeField]
    private Image taskCompletedImage;
    [SerializeField]
    private Image taskFailedImage;

    [SerializeField]
    private Image timerImage;
    private float timerWidth;
    private float timerWidthMax = 2f;
    private float timerWidthMin = 0f;
    private Color timerColor = new Color(0f, 255f, 0f);


    public void UpdateTimerBar(float timerPercent)
    {
        timerImage.color = Color.Lerp(Color.green, Color.red, timerPercent);
        timerWidth = Mathf.Lerp(timerWidthMax, timerWidthMin, timerPercent);
        timerImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, timerWidth);
    }

    public void SetTaskIngredients(int Ornament01Type, int Ornament02Type, int Ornament03Type, int BodyType, int HeadType)
    {
        SetOrnamentType(Ornament01Type, Ornament02Type, Ornament03Type);
        SetBody(BodyType);
        SetHead(HeadType);
    }

    private void SetOrnamentType(int Ornament01Type, int Ornament02Type, int Ornament03Type)
    {
        OrnamentTypelot01Image.sprite = OrnamentTypeprites[Ornament01Type];
        OrnamentTypelot02Image.sprite = OrnamentTypeprites[Ornament02Type];
        OrnamentTypelot03Image.sprite = OrnamentTypeprites[Ornament03Type];
    }

    private void SetBody(int BodyType)
    {
        bodyImage.sprite = bodySprites[BodyType];
        switch (BodyType)
        {
            case 0:
                bodyImage.color = Color.red;
                break;
            case 1:
                bodyImage.color = Color.green;
                break;
            case 2:
                bodyImage.color = Color.blue;
                break;
            default:
                throw new System.Exception("TaskCard::SetBody exception: Invalid color");
        }
    }

    private void SetHead(int HeadType)
    {
        headImage.sprite = HeadTypeprites[HeadType];
        switch (HeadType)
        {
            case 0:
                headImage.color = Color.red;
                break;
            case 1:
                headImage.color = Color.green;
                break;
            case 2:
                headImage.color = Color.blue;
                break;
            default:
                throw new System.Exception("TaskCard::SetHead exception: Invalid color");
        }
    }

    public void TaskCompleted()
    {
        //foreach (Image image in GetComponentsInChildren<Image>())
        //{
        //    taskCompletedImage.gameObject.SetActive(true);
        //}
        taskCompletedImage.gameObject.SetActive(true);
    }


    public void TaskFailed() {
        taskFailedImage.gameObject.SetActive(true);
    }

    public void Disable() {
        foreach (Image image in GetComponentsInChildren<Image>()) {
            image.gameObject.SetActive(false);
        }

        //headImage.gameObject.SetActive(false);
        //bodyImage.gameObject.SetActive(false);

        //OrnamentTypelot01Image.gameObject.SetActive(false);
        //OrnamentTypelot02Image.gameObject.SetActive(false);
        //OrnamentTypelot03Image.gameObject.SetActive(false);
    }
}
