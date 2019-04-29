using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTowerWindow : MonoBehaviour {
    //control the window for adding towers
    public GameObject towerSlotToAddTower;

    public void AddTower(string towerTypeAsString)
    {
        //parse the string type to TowerType
        TowerType type = (TowerType)Enum.Parse(typeof(TowerType), towerTypeAsString, true);
        if(TowerManager.Instance.GetTowerPrice(type) <= GameManager.Instance.gold)//if player has enough money
        {
            GameManager.Instance.gold -= TowerManager.Instance.GetTowerPrice(type);
            //add the tower
            TowerManager.Instance.CreateNewTower(towerSlotToAddTower, type);
            gameObject.SetActive(false);//close the window
        }
    }
	
}
