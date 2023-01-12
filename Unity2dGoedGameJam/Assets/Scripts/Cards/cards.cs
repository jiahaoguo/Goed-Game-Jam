using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCard", menuName = "cards")]
public class cards : ScriptableObject
{
    [SerializeReference]    
    public ICardEffect Effect;

    public void PerformEffect()
    {
        Effect?.PerformEffect();
    }
    public string cardName;
    public string description;

    public Sprite Art;

    public int timeCost;
    public string type;
}
public interface ICardEffect
{
    void PerformEffect();
}

public class CardEffectTypeA : ICardEffect
{
    public void PerformEffect()
    {
        Debug.Log("a");
        //your effect's logic here
    }
}

public class CardEffectTypeB : ICardEffect
{
    public void PerformEffect()
    {
        Debug.Log("b");
        //your effect's logic here
    }
}
