using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoWindow : MonoBehaviour {
    //the tower info panel
    public Tower tower;
    //info text
    public Text infoText;
    public Text upgradeCostText;

    //cost of upgrade
    private int upgradePrice;
    private GameObject upgradeBtn;//upgrade button
    private GameObject closeBtn;//close button

    private void Awake()
    {
        upgradeBtn = upgradeCostText.transform.parent.gameObject;
    }

    //when window is enabled do things
    private void OnEnable()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        //get upgrade price
        upgradePrice = Mathf.CeilToInt(TowerManager.Instance.GetTowerPrice(tower.type) * 1.5f * tower.towerLevel);
        //update tower level
        infoText.text = tower.type + " Tower Lvl: " + tower.towerLevel;

        if(tower.towerLevel < 3)
        {
            upgradeBtn.SetActive(true);
            upgradeCostText.text = "Upgrade\n" + upgradePrice + " gold";
        }
        else
        {
            //tower already at max level
            upgradeBtn.SetActive(false);
        }
    }

    //upgrade the tower on click
    public void UpgradeTower()
    {
        if(GameManager.Instance.gold >= upgradePrice)
        {
            GameManager.Instance.gold -= upgradePrice;
            tower.LevelUp();//level up tower
            gameObject.SetActive(false);//close window
        }
    }
}
