using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardDisplay : MonoBehaviour
{
    public cards card;
    private void Start()
    {
        transform.Find("image").GetComponent<Image>().sprite = card.Art;
        transform.Find("Name").GetComponent<Text>().text = card.cardName;
        GetComponent<Button>().onClick.AddListener(checkCard);
    }
    void checkCard()
    {
        Messenger.AddListener<bool>(Events.checkTimeRemain, useCard);
        Messenger.Broadcast<int>(Events.checkCard, card.timeCost);        
    }
    void useCard(bool avaliability)
    {
        card.PerformEffect();
        Messenger.RemoveListener<bool>(Events.checkTimeRemain, useCard);
        if (avaliability) Messenger.Broadcast<cardDisplay>(Events.useCard, GetComponent<cardDisplay>());
    }
}
