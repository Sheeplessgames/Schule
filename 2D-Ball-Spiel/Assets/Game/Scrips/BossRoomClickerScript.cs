using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossRoomClickerScript : MonoBehaviour
{
    #region Money
    public float MoneyStored;
    public float MoneyGenerated;
    public float MoneyGenStart;
    public float MoneyCollected;
    #endregion

    #region Time
    public float timer;
    public float startTimer;
    [SerializeField]
    float time;
    #endregion

    #region Room
    public int RoomLevel;
    public float RoomLevelMultipiler;
    #endregion

    #region Upgrade
    public float UpgradeCost;
    float upgradeMultipiler;
    #endregion

    public Text RoomLevelText;
    public Text MoneyStoredText;
    // Start is called before the first frame update
    void Start()
    {
        timer = startTimer;
        MoneyGenerated = MoneyGenStart;
        RoomLevel = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;

        if (time >= timer)
        {
            MoneyStored += MoneyGenerated; 
            time = 0;
        }
    }

    private void Update()
    {
        RoomLevelText.text = "LVL:" + RoomLevel.ToString();
        MoneyStoredText.text = MoneyStored.ToString("F2") + "$";

        RaycastHit hit;

        var ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

        Debug.DrawLine(ray.origin, Camera.main.transform.forward * 200, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Boss") && MoneyStored >= UpgradeCost)
                {
                    MoneyCollected += MoneyStored;
                    MoneyStored = 0;
                }
            }
        }

    }

    public void UpgradeRoom()
    {
        if (MoneyCollected >= UpgradeCost)
        {
            RoomLevel += 1;
            MoneyGenerated = MoneyGenerated + 2;
            MoneyCollected = MoneyCollected - UpgradeCost;
            UpgradeCost = UpgradeCost + 4;
        }
    }
}

