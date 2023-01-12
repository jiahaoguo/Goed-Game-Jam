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

public class moveRight4 : ICardEffect
{
    public void PerformEffect()
    {
        Messenger.Broadcast<int>(Events.move, 4);
    }
}

public class normalAttack : ICardEffect
{
    public void PerformEffect()
    {
        Messenger.Broadcast<int>(Events.attack, 2);
    }
}
