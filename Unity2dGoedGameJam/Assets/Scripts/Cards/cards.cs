using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCard", menuName = "cards")]
public class cards : ScriptableObject
{
    [SerializeReference]    
    public ICardEffect Effect;
    public int effectAmount;

    public void PerformEffect()
    {
        Effect?.PerformEffect();
    }
    public string cardName;
    public string description;

    public Sprite Art;

    public int timeCost;
}
public interface ICardEffect
{
    void PerformEffect();
}

public class moveRight : ICardEffect
{
    public int effectAmount = cards.effectAmount;
    public void PerformEffect()
    {
        Messenger.Broadcast<int>(Events.move, effectAmount);
    }
}

public class Attack : ICardEffect
{
    public void PerformEffect()
    {
        Messenger.Broadcast<int>(Events.attack, effectAmount);
    }
}
public class equip : ICardEffect
{
    public void PerformEffect()
    {

    }
}
