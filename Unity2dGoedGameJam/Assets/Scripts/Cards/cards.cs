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

public class MoveRight : ICardEffect
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
public class Equip : ICardEffect
{
    public void PerformEffect(int effectAmount)
    {
        Messenger.Broadcast<int>(Events.equip, effectAmount);
    }
}
public class Think : ICardEffect
{
    public void PerformEffect(int effectAmount)
    {
        Messenger.Broadcast<int>(Events.drawCard, effectAmount);
    }
}
public class Wait : ICardEffect
{
    public void PerformEffect(int effectAmount)
    {

    }
}
public class Turn : ICardEffect
{
    public void PerformEffect(int effectAmount)
    {
        Messenger.Broadcast(Events.turn);
    }
}
public class Up: ICardEffect
{
    public void PerformEffect(int effectAmount)
    {
        Messenger.Broadcast(Events.up);
    }
}
