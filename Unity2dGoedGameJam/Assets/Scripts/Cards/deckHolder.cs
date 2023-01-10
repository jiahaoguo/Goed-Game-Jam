using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deckHolder : MonoBehaviour
{
    public Text deckNum;
    public int maxiumumCard;
    public List<cardDisplay> deck;
    public List<cardDisplay> hands;
    public RectTransform[] cardSlots;
    private void Start()
    {
    }
    void Update()
    {
        deckNum.text = deck.Count.ToString();
    }
    public void drawCards()
    {
        if (deck.Count >= 1 && deck.Count<=maxiumumCard)
        {
            cardDisplay randomCard = deck[Random.Range(0,deck.Count)];
            randomCard.gameObject.SetActive(true);
            hands.Add(randomCard);
            deck.Remove(randomCard);
            for (int i = 0; i < hands.Count; i++)
            {
                hands[i].GetComponent<RectTransform>().anchoredPosition = cardSlots[i].anchoredPosition;
            }            

        }
    }
    private void OnEnable()
    {
        Messenger.AddListener<cardDisplay>(Events.useCard, usingCard);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener<cardDisplay>(Events.useCard, usingCard);
    }
    private void usingCard(cardDisplay card)
    {
        hands.Remove(card);
        deck.Add(card);
        card.gameObject.SetActive(false);
        for (int i = 0; i < hands.Count; i++)
        {
            hands[i].GetComponent<RectTransform>().anchoredPosition = cardSlots[i].anchoredPosition;
        }
    }
}
