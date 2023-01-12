using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    private void OnEnable()
    {
        Messenger.AddListener<int>(Events.move, moving);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener<int>(Events.move, moving);
    }

    private void moving(int distance)
    {
        transform.position+=new Vector3(distance, 0, 0);
    }
}
