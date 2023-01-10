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
        Messenger.AddListener<cardDisplay>(Events.useCard, usingTime);
    }
    private void OnDisable()
    {
        Messenger.AddListener<int>(Events.checkCard, checkTime);
        Messenger.AddListener<cardDisplay>(Events.useCard, usingTime);
    }
    void checkTime(int t)
    {
        Messenger.Broadcast<bool>(Events.checkTimeRemain, Time>t);
    }
    void usingTime(cardDisplay card)
    {
        Time -= card.card.timeCost;
    }
}
