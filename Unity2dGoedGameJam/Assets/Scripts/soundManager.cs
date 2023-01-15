using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager Instance;

    public AudioSource sdBGM;
    public AudioSource sdHit;
    public AudioSource sdEquipSword;
    public AudioSource sdTurn;
    public AudioSource sdPlayerWalk;
    public AudioSource sdElfWalk;
    public AudioSource sdGiantWalk;
    public AudioSource sdShoot;
    public AudioSource sdThink;
    public AudioSource sdWait;
    public AudioSource sdHurt;



    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }



    // Start is called before the first frame update
    void Start()
    {
        sdBGM.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
