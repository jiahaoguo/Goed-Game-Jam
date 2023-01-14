using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRemain : MonoBehaviour
{
    public int time;
    private float timeShowed;
    private void Start()
    {
        timeShowed=time;
    }
    private void Update()
    { 
        if (timeShowed > time)
        {
            GetComponent<Text>().text = ((int)timeShowed).ToString();
            timeShowed -=Time.deltaTime;
        }
        
    }
    private void OnEnable()
    {
        Messenger.AddListener<int>(Events.checkCard, checkTime);
        Messenger.AddListener<int>(Events.timeStart, usingCard);
    }
    private void OnDisable()
    {
        Messenger.AddListener<int>(Events.checkCard, checkTime);
        Messenger.AddListener<int>(Events.timeStart, usingCard);
    }
    void checkTime(int t)
    {
        Messenger.Broadcast<bool>(Events.checkTimeRemain, time>t);
    }
    void usingCard(int time1)
    {
        time -= time1;
        if(time < 0)
        {
            //Game Over
            //Messenger Board Cast
        }
    }
}
