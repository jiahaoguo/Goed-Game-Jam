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
            timeShowed -= Time.deltaTime;
        }
        else timeShowed = time;
        
    }
    private void OnEnable()
    {
        Messenger.AddListener<int>(Events.checkCard, checkTime);
        Messenger.AddListener<int>(Events.timeStart, usingCard);
        Messenger.AddListener(Events.win, wining);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener<int>(Events.checkCard, checkTime);
        Messenger.RemoveListener<int>(Events.timeStart, usingCard);
        Messenger.RemoveListener(Events.win, wining);
    }
    void checkTime(int t)
    {
        Messenger.Broadcast<bool>(Events.checkTimeRemain, time>=t);
    }
    void wining()
    {
        time = 30;
        transform.Find("Win").gameObject.SetActive(true);
    }
    void usingCard(int time1)
    {
        time -= time1;
        if(time <= 0)
        {
            time = 30;
            transform.Find("GameOver").gameObject.SetActive(true);
            //Game Over
            //Messenger Board Cast
        }
    }
}
