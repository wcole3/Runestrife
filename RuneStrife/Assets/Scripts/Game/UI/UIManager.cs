using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    //manager for the UI
    public static UIManager Instance;
    //ui elements to mamage
    public GameObject addTowerWindow;
    public GameObject towerInfoWindow;
    public GameObject winGameWindow;
    public GameObject loseGameWindow;
    public GameObject blackBackground;
    public Text goldText;
    public Text waveText;
    public Text escEnemiesText;
    //enemy health bar text
    public Transform enemyHealthBars;
    public GameObject enemyHealthBarPrefab;
    //flashing info window
    public GameObject centerWindow;
    //esc enemy indicator
    public GameObject damageScreen;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
        UpdateTopBar();
	}

    //update the text at the top of the screen
    private void UpdateTopBar()
    {
        goldText.text = GameManager.Instance.gold.ToString();//update gold
        waveText.text = "Wave: " + GameManager.Instance.waveNumber.ToString() +
             "/" + WaveManager.Instance.enemyWaves.Count.ToString();//update wave text
        //update esc enemies
        escEnemiesText.text = "Escaped Enemies: " + GameManager.Instance.escapedEnemies.ToString() + "/"
            + GameManager.Instance.maxAllowedEnemies.ToString();

    }

    //pop up the add tower window on event
    public void ShowAddTowerWindow(GameObject towerSlot)
    {
        addTowerWindow.SetActive(true);
        addTowerWindow.GetComponent<AddTowerWindow>().towerSlotToAddTower = towerSlot;
        //move the window to proper location
        UtilityMethods.MoveUiElementToWorldPosition(addTowerWindow.GetComponent<RectTransform>(), towerSlot.transform.position);
    }
    //show the tower info window
    public void ShowTowerInfoWindow(Tower tower)
    {
        towerInfoWindow.GetComponent<TowerInfoWindow>().tower = tower;//setup the tower info
        towerInfoWindow.SetActive(true);//show window
        //move ui to screen location
        UtilityMethods.MoveUiElementToWorldPosition(towerInfoWindow.GetComponent<RectTransform>(), tower.transform.position);
    }

    //show endgame windows
    public void ShowWinScreen()
    {
        winGameWindow.SetActive(true);
        blackBackground.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        loseGameWindow.SetActive(true);
        blackBackground.SetActive(true);
    }

    //make enemy health bars
    public void CreateHealthBarForEnemy(Enemy enemy)
    {
        GameObject healthBar = (GameObject)Instantiate(enemyHealthBarPrefab);
        healthBar.transform.SetParent(enemyHealthBars, false);
        healthBar.GetComponent<EnemyHealthBar>().enemy = enemy;
    }

    //show and flash the info window
    public void ShowCenterWindow(string text)
    {
        centerWindow.transform.Find("TxtWave").GetComponent<Text>().text = text;
        StartCoroutine(FlashCenterWindow());
    }
    IEnumerator FlashCenterWindow()
    {
        for(int i = 0; i < 3; ++i)
        {
            yield return new WaitForSeconds(0.4f);
            centerWindow.SetActive(true);

            yield return new WaitForSeconds(0.4f);
            centerWindow.SetActive(false);

        }
    }

    public void ShowDamageScreen()
    {
        StartCoroutine(FlashDamageScreen());
    }
    IEnumerator FlashDamageScreen()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.1f);
            damageScreen.SetActive(true);

            yield return new WaitForSeconds(0.1f);
            damageScreen.SetActive(false);
        }

    }    
}

