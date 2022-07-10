using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Powerup Card", menuName = "Card/Powerup Card")]
public class PowerupCard : ScriptableObject
{
    public Sprite icon;

    public string powerupName;
    public int powerupNumber;
    public enum Units { points, percent};
    public Units powerupUnits;
    public int powerupCap;
    public string powerupDescription;
}

