using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//info for towers
[System.Serializable]
public struct TowerCost
{
    public TowerType TowerType;
    public int Cost;
}

public class TowerManager : MonoBehaviour {
    //manager class for tower objects
    public static TowerManager Instance;
    //prefabs for towers
    public GameObject stoneTowerPrefab;
    public GameObject iceTowerPrefab;
    public GameObject fireTowerPrefab;

    public List<TowerCost> TowerCosts = new List<TowerCost>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
    //method to create a new tower
    public void CreateNewTower(GameObject slotToFill, TowerType towerType)
    {
        switch (towerType)
        {
            case TowerType.Stone:
                Instantiate(stoneTowerPrefab, slotToFill.transform.position, Quaternion.identity);
                slotToFill.gameObject.SetActive(false);
                break;
            case TowerType.Fire:
                Instantiate(fireTowerPrefab, slotToFill.transform.position, Quaternion.identity);
                slotToFill.gameObject.SetActive(false);
                break;
            case TowerType.Ice:
                Instantiate(iceTowerPrefab, slotToFill.transform.position, Quaternion.identity);
                slotToFill.gameObject.SetActive(false);
                break;
        }
    }
    //query the price of the tower
    public int GetTowerPrice(TowerType towerType)
    {
        return(from towerCost in TowerCosts //look in TowerCosts for element towerCost
               where towerCost.TowerType ==towerType//where the type matchs the passed type
               select towerCost.Cost).FirstOrDefault();//return the cost of that element
    }

}
