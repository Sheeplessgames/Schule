using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoomClicker : MonoBehaviour
{
    #region Money
    [Header("Money")]
    public float _MoneyStored;
    public float _MoneyGenerated;
    public float _MoneyGenStart;
    #endregion

    #region Time
    [Header("Time")]
    public float _Timer;
    public float _StartTimer;
    [SerializeField] private float _time;
    #endregion

    #region Room
    [Header("Room")]
    public int _RoomLevel;
    public float _RoomLevelMultiplier;
    #endregion

    #region Upgrade
    [Header("Upgrade")]
    public float _UpgradeCosts;
    private float _upgradeMultiplier;
    #endregion

    #region LocalUI
    [Header("Local UI")]
    public Text _RoomLevelText;
    public Text _MoneyStoredText;
    #endregion

    void Start()
    {
        if (SaveSystem.LoadData() == null)
        {
            _Timer = _StartTimer;
            _MoneyGenerated = _MoneyGenStart;
            _UpgradeCosts = 2;
            _RoomLevel = 1;
        }
        else if (SaveSystem.LoadData() != null)
        {
            StartCoroutine(LoadData());
        }

    }
    
    void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;

        if (_time >= _Timer)
        {
            _MoneyStored += _MoneyGenerated;
            _time = 0;
        }
    }

    private void Update()
    {
        _RoomLevelText.text = _RoomLevel.ToString();
        _MoneyStoredText.text = _MoneyStored.ToString("F2") + "$";

        RaycastHit hit;

        var ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

        Debug.DrawLine(ray.origin, Camera.main.transform.forward * 200, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag(this.tag))
                {
                    GameManager.Instance.MoneyCollected += _MoneyStored;
                    _MoneyStored = 0;
                }
            }
        }
    }

    public void UpgradeRoom()
    {
        if (GameManager.Instance.MoneyCollected >= _UpgradeCosts)
        {
            _MoneyGenerated += 2;
            GameManager.Instance.MoneyCollected -= _UpgradeCosts;
            _UpgradeCosts += 4;
            _RoomLevel += 1;
        }
    }

    IEnumerator LoadData()
    {
        if (true)
        {

        }
        _MoneyGenerated = ValueHandler.SaveInstance.MoneySave;
        yield return new WaitForSeconds(0.5f);
    }
}
