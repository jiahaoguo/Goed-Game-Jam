using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCard", menuName = "cards")]
public class cards : ScriptableObject
{
    [SerializeReference]    
    public ICardEffect Effect;

    public void PerformEffect(int effectAmount)
    {
        Effect?.PerformEffect(effectAmount);
    }
    public int EffectAmount;
    public string cardName;
    public string description;

    public int timeCost;

    public Sprite Art;

    
}
public interface ICardEffect
{
    void PerformEffect(int effectAmount);
}

public class moveRight : ICardEffect
{
    public void PerformEffect(int effectAmount)
    {
        Messenger.Broadcast<int>(Events.move, effectAmount);
    }
}

public class Attack : ICardEffect
{
    public void PerformEffect(int effectAmount)
    {
        Messenger.Broadcast<int>(Events.attack, effectAmount);
    }
}
public class equip : ICardEffect
{
    public void PerformEffect(int effectAmount)
    {

    }
}
