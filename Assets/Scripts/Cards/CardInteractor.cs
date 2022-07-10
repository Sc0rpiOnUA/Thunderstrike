using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInteractor : MonoBehaviour
{
    public float hoverScale;
    public float centerScale;
    public float centerTime;

    public enum CardPosition { Left, Middle, Right}
    public CardPosition cardPosition;

    public enum CardType { Weapon, Powerup}
    public CardType cardType;

    private EventTrigger eventTrigger;
    private GameObject gameManagerObject;
    private GameManager gameManager;
    private bool hasMouseEntered;

    private void Awake()
    {
        eventTrigger = GetComponent<EventTrigger>();
        gameManagerObject = GameObject.FindWithTag("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        hasMouseEntered = false;
    }
    public void MouseEntersCardEvent()
    {
        if(!hasMouseEntered)
        {
            Debug.Log($"Mouse entered the card {gameObject}!");
            gameObject.transform.localScale *= hoverScale;
        }
        hasMouseEntered = true;
    }

    public void MouseExitsCardEvent()
    {
        if (hasMouseEntered)
        {
            Debug.Log($"Mouse exited the card {gameObject}!");
            gameObject.transform.localScale /= hoverScale;
        }
        hasMouseEntered = false;
    }

    public void SelectCardEvent()
    {
        Debug.Log($"Card {gameObject} selected! Passing on to GameManager");
        gameManager.SelectCard(cardType, gameObject);
    }

    public void CreateCard(CardType type, CardPosition position)
    {
        eventTrigger.enabled = false;
        cardType = type;

        switch (position)
        {
            case CardPosition.Left:
                {
                    StartCoroutine(CardPositionCreateCoroutine(new Vector2(Screen.width / 4, Screen.height / 2)));
                    break;
                }
            case CardPosition.Middle:
                {
                    StartCoroutine(CardPositionCreateCoroutine(new Vector2(Screen.width / 2, Screen.height / 2)));
                    break;
                }
            case CardPosition.Right:
                {
                    StartCoroutine(CardPositionCreateCoroutine(new Vector2((Screen.width / 4) * 3 , Screen.height / 2)));
                    break;
                }
        }

        GetComponent<RectTransform>().localScale = new Vector2(1, 1);

        //Doesn't work for some reason
        //StartCoroutine(CardScaleCreateCoroutine(centerTime));
    }

    public void SelectCard()
    {
        eventTrigger.enabled = false;
        StartCoroutine(CardPositionSelectCoroutine(centerTime));
        StartCoroutine(CardScaleSelectCoroutine(centerTime));
    }

    public void DiscardCard()
    {
        eventTrigger.enabled = false;
        StartCoroutine(CardPositionDiscardCoroutine(centerTime));
        StartCoroutine(CardScaleDiscardCoroutine(centerTime));
    }

    public void EquipCard()
    {
        eventTrigger.enabled = false;
        StartCoroutine(CardPositionEquipCoroutine(centerTime));
        StartCoroutine(CardScaleEquipCoroutine(centerTime));
    }


    //Coroutines
    private IEnumerator CardPositionCreateCoroutine(Vector2 position, float duration = 1f)
    {
        Vector2 startPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 endPosition = position;

        float time = 0f;

        while (time < duration)
        {
            gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        eventTrigger.enabled = true;
    }

    private IEnumerator CardScaleCreateCoroutine(float duration = 1f)
    {
        Vector2 startScale = gameObject.transform.localScale / 10f;
        Vector2 endScale = new Vector2(1, 1);
        

        float time = 0f;

        while (time < duration)
        {
            gameObject.transform.localScale = Vector2.Lerp(startScale, endScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator CardPositionSelectCoroutine(float duration = 1f)
    {
        Vector2 startPosition = gameObject.transform.position;
        Vector2 endPosition = new Vector2(Screen.width / 2, Screen.height / 2);

        float time = 0f;

        while (time < duration)
        {
            gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator CardScaleSelectCoroutine(float duration = 1f)
    {
        Vector2 startScale = gameObject.transform.localScale;
        Vector2 endScale = startScale * centerScale;

        float time = 0f;

        while (time < duration)
        {
            gameObject.transform.localScale = Vector2.Lerp(startScale, endScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator CardPositionDiscardCoroutine(float duration = 1f)
    {
        Vector2 startPosition = gameObject.transform.position;
        Vector2 endPosition = new Vector2(Screen.width, 0);

        float time = 0f;

        while (time < duration)
        {
            gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator CardScaleDiscardCoroutine(float duration = 1f)
    {
        Vector2 startScale = gameObject.transform.localScale;
        Vector2 endScale = startScale * 0;

        float time = 0f;

        while (time < duration)
        {
            gameObject.transform.localScale = Vector2.Lerp(startScale, endScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator CardPositionEquipCoroutine(float duration = 1f)
    {
        Vector2 startPosition = gameObject.transform.position;
        Vector2 endPosition = new Vector2(0, Screen.height);

        float time = 0f;

        while (time < duration)
        {
            gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator CardScaleEquipCoroutine(float duration = 1f)
    {
        Vector2 startScale = gameObject.transform.localScale;
        Vector2 endScale = startScale * 0;

        float time = 0f;

        while (time < duration)
        {
            gameObject.transform.localScale = Vector2.Lerp(startScale, endScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

}
