using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardDisplay : MonoBehaviour
{
    public cards card;
    private float timer=0;
    private void Start()
    {
        transform.Find("image").GetComponent<Image>().sprite = card.Art;
        transform.Find("Name").GetComponent<Text>().text = card.cardName;
        transform.Find("description").GetComponent<Text>().text = card.description;
        transform.Find("TimeCost").GetComponent<Text>().text=card.timeCost.ToString()+"s";
        GetComponent<Button>().onClick.AddListener(checkCard);
    }
    private void Update()
    {
        if(timer > 0) GetComponent<Button>().enabled = false;
        else GetComponent<Button>().enabled = true;
        timer-=Time.deltaTime;
    }
    private void OnEnable()
    {
        Messenger.AddListener<int>(Events.timeStart, addTimer);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener<int>(Events.timeStart, addTimer);
    }
    private void addTimer(int x)
    {
        timer = x;
    }
    void checkCard()
    {
        Messenger.AddListener<bool>(Events.checkTimeRemain, useCard);
        Messenger.Broadcast<int>(Events.checkCard, card.timeCost);        
    }
    void useCard(bool avaliability)
    {
        Messenger.RemoveListener<bool>(Events.checkTimeRemain, useCard);
        if (avaliability) Messenger.Broadcast<cardDisplay>(Events.useCard, GetComponent<cardDisplay>());
    }
}
