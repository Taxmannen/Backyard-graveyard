using UnityEngine;

public enum OrnamentType { Candle, Flower, Statue, Heart, [System.ObsoleteAttribute] NumberOfTypes, [System.ObsoleteAttribute] None };

/* Script Made By Daniel */
public class Ornament : PlaceablePickup
{
    #region Variables
    [Header("Ornament")]
    [SerializeField] private OrnamentType ornamentType;
    #endregion

    public OrnamentType GetOrnamentType() { return ornamentType; }
}