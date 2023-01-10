using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCard", menuName = "cards")]
public class cards : ScriptableObject
{
    public string cardName;
    public string description;

    public Sprite Art;

    public int timeCost;
    public string type;
}
