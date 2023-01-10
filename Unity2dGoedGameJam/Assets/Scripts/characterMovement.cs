using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    private void OnEnable()
    {
        Messenger.AddListener<cardDisplay>(Events.useCard, acting);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener<cardDisplay>(Events.useCard, acting);
    }

    private void acting(cardDisplay card)
    {
        Debug.Log("move");
    }
}
