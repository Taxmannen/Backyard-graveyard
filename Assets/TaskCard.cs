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

    public Image ornamentSlot01Image;
    public Image ornamentSlot02Image;
    public Image ornamentSlot03Image;

    public Sprite[] headSprites;
    public Sprite[] bodySprites;
    public Sprite[] ornamentSprites;

    //private void Start()
    //{
    //    headImage.sprite = headSprites[Random.Range(0, headSprites.Length)];
    //    bodyImage.sprite = bodySprites[Random.Range(0, bodySprites.Length)];
    //    ornamentSlot01Image.sprite = ornamentSprites[Random.Range(0, ornamentSprites.Length)];
    //    ornamentSlot02Image.sprite = ornamentSprites[Random.Range(0, ornamentSprites.Length)];
    //    ornamentSlot03Image.sprite = ornamentSprites[Random.Range(0, ornamentSprites.Length)];
    //}

    private void Start()
    {
        TaskCompleted();
    }

    public void SetTaskIngredients(int Ornament01Type, int Ornament02Type, int Ornament03Type, int BodyType, int HeadType)
    {
        SetOrnaments(Ornament01Type, Ornament02Type, Ornament03Type);
        SetBody(BodyType);
        SetHead(HeadType);
    }

    private void SetOrnaments(int Ornament01Type, int Ornament02Type, int Ornament03Type)
    {
        ornamentSlot01Image.sprite = ornamentSprites[Ornament01Type];
        ornamentSlot02Image.sprite = ornamentSprites[Ornament02Type];
        ornamentSlot03Image.sprite = ornamentSprites[Ornament03Type];
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
        headImage.sprite = headSprites[HeadType];
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
        foreach (Image image in GetComponentsInChildren<Image>())
        {
            image.gameObject.SetActive(false);
        }
    }
}
