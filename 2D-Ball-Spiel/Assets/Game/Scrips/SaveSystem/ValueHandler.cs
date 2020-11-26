using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class ValueHandler : MonoBehaviour
{
    public float MoneySave;
    public static ValueHandler SaveInstance;

    private void Awake()
    {
        SaveInstance = this;

        if (SaveSystem.LoadData() != null)
        {
            LoadData();
        }
    }
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SaveSystem.SaveData(this);
        MoneySave = GameManager.Instance.MoneyCollected;
    }

    public void LoadData()
    {
        PlayerData data = SaveSystem.LoadData();
        MoneySave = data.moneySaveData;
    }
}
