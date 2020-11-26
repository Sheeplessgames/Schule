using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text _MoneyColText;
    [SerializeField] private float _moneyCollected;

    #region Getter / Setter
    public float MoneyCollected
    {
        get { return _moneyCollected; }
        set { _moneyCollected = value; }
    }
    #endregion

     void Awake()
    {
        Instance = this;
        StartCoroutine(Load());
    }

    void Update()
    {
        _MoneyColText.text ="Money Collected: "+ _moneyCollected.ToString("F2") + "$";
    }

    IEnumerator Load()
    {
        MoneyCollected = ValueHandler.SaveInstance.MoneySave;
        yield return new WaitForSeconds(0.5f);
    }
}
