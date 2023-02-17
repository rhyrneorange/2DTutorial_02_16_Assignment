using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Ball,
    Bomb,
    BowlingBall,
    Coin,
    Hat,
    Magnet,
    Max
}
[System.Serializable]
public struct ItemData
{
    public ItemType type;
    public int iconIdx;
    public float value;
}
[System.Serializable]
public struct ItemInfo
{
    public ItemData itemData;
    public int count;
}