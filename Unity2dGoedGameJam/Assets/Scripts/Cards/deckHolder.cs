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
    public List<cardDisplay> discards;
    public RectTransform[] cardSlots;
    public GameObject cardMod;
    private void Start()
    {
        discards = new List<cardDisplay>();
        while(deck.Count >= 1 && hands.Count < maxiumumCard)
        {
            drawCards();
        }
    }
    void Update()
    {
        deckNum.text = deck.Count.ToString();
    }
    public cardDisplay addCard(cards card)
    {
        if (hands.Count < maxiumumCard)
        {
            cardMod.GetComponent<cardDisplay>().card = card;
            GameObject newcard = Instantiate(cardMod,transform.Find("parentCard"));            
            hands.Add(newcard.GetComponent<cardDisplay>());
            for (int i = 0; i < hands.Count; i++)
            {
                hands[i].GetComponent<RectTransform>().anchoredPosition = cardSlots[i].anchoredPosition;
            }
            return newcard.GetComponent<cardDisplay>();
        }
        return null;
       
    }
    public void drawCards()
    {
        if (deck.Count >= 1 && hands.Count<maxiumumCard)
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
        if (deck.Count <= 0)
        {
            deck.AddRange(discards);
            discards.Clear();
            Messenger.Broadcast<int>(Events.timeStart, 2);
        }
    }
    private void OnEnable()
    {
        Messenger.AddListener<cardDisplay>(Events.useCard, usingCard);
        Messenger.AddListener<int>(Events.drawCard, drawMCards);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener<cardDisplay>(Events.useCard, usingCard);
        Messenger.RemoveListener<int>(Events.drawCard, drawMCards);
    }
    private void usingCard(cardDisplay card)
    {
        card.card.PerformEffect(card.card.EffectAmount);
        Messenger.Broadcast<int>(Events.timeStart, card.card.timeCost);
        hands.Remove(card);
        discards.Add(card);
        card.gameObject.SetActive(false);
        for (int i = 0; i < hands.Count; i++)
        {
            hands[i].GetComponent<RectTransform>().anchoredPosition = cardSlots[i].anchoredPosition;
        }
        if(hands.Count <= 0)
        {
            while (deck.Count >= 1 && hands.Count < maxiumumCard)
            {
                drawCards();
            }
            Messenger.Broadcast<int>(Events.timeStart, 5);
        }
    }
    private void drawMCards(int n)
    {
        for (int i = 0;i < n; i++)
        {
            drawCards();
        }
    }
}
