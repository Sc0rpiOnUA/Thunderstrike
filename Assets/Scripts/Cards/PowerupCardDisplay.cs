using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerupCardDisplay : MonoBehaviour
{
    public PowerupCard[] powerupCards;

    //public Image icon;
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI cardDescriptionText;

    public string cardName;
    public int cardNumber;
    public PowerupCard.Units cardUnits;
    public int cardCap;
    public string cardDescription;

    private CardInteractor interactor;
    private CardInteractor.CardType cardType;

    private void Awake()
    {
        interactor = GetComponent<CardInteractor>();
        cardType = CardInteractor.CardType.Powerup;
    }

    public void SetCard(int cardIndex, CardInteractor.CardPosition position)
    {
        Debug.Log($"Setting a new card with position = {position}");
        FillCardData(cardIndex);
        interactor.CreateCard(cardType, position);
    }

    public void SetCard(int cardIndex, int positionIndex)
    {
        FillCardData(cardIndex);

        if (positionIndex == 0)
        {
            interactor.CreateCard(cardType, CardInteractor.CardPosition.Left);
        }
        else if (positionIndex == 1)
        {
            interactor.CreateCard(cardType, CardInteractor.CardPosition.Middle);
        }
        else if (positionIndex == 2)
        {
            interactor.CreateCard(cardType, CardInteractor.CardPosition.Right);
        }
        else
        {
            Debug.LogError($"Card out of bounds! Index: {cardIndex}, Position index: {positionIndex}");
        }
    }

    public void FillCardData(int index)
    {
        Debug.Log($"Filling card with data! Index = {index}");

        cardName = powerupCards[index].powerupName;
        cardNumber = powerupCards[index].powerupNumber;
        cardUnits = powerupCards[index].powerupUnits;
        cardCap = powerupCards[index].powerupCap;
        cardDescription = powerupCards[index].powerupDescription;


        //icon.sprite = powerupCards[index].icon;
        switch (cardUnits)
        {
            case PowerupCard.Units.percent:
                {
                    cardNameText.text = $"{cardName} +{cardNumber}%";
                    cardDescriptionText.text = $"{cardDescription} {cardNumber}%";
                    break;
                }
            case PowerupCard.Units.points:
                {
                    cardNameText.text = $"{cardName} +{cardNumber}";
                    cardDescriptionText.text = $"{cardDescription} {cardNumber} points";
                    break;
                }
        }
        
    }

}