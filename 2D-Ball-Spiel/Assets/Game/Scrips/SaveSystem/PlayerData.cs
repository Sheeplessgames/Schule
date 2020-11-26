using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData 
{
    public float moneySaveData;
    public PlayerData(ValueHandler value)
    {
        moneySaveData = value.MoneySave;
    }
}
