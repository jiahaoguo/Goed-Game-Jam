using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRemain : MonoBehaviour
{
    public int Time;
    private void Update()
    {
        GetComponent<Text>().text = Time.ToString();
    }
    private void OnEnable()
    {
        Messenger.AddListener<int>(Events.checkCard, checkTime);
        Messenger.AddListener<cardDisplay>(Events.useCard, usingCard);
    }
    private void OnDisable()
    {
        Messenger.AddListener<int>(Events.checkCard, checkTime);
        Messenger.AddListener<cardDisplay>(Events.useCard, usingCard);
    }
    void checkTime(int t)
    {
        Messenger.Broadcast<bool>(Events.checkTimeRemain, Time>t);
    }
    void usingCard(cardDisplay card)
    {
        usingTime(card.card.timeCost);
    }
    void usingTime(int time)
    {
        Time -= time;
        if(Time < 0)
        {
            //Game Over
            //Messenger Board Cast
        }
    }
}
