using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour
{

    private float countDown;

    // Start is called before the first frame update
    void Start()
    {
        countDown = 0f;
    }

    void Update()
    {
        countDown += 1f*Time.deltaTime;
        if(countDown >= 5 * 60 * Time.deltaTime)
        {

            Destroy(this.gameObject);
        }

    }

}
