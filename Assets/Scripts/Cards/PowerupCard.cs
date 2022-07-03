using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Powerup Card", menuName = "Card/Powerup Card")]
public class PowerupCard : ScriptableObject
{
    public Sprite icon;

    public string powerupName;
    public string powerupDescription;
}

