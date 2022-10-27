using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ItemManager ItemManager;
    public TileManager tileManager;
    public UI_Manager uiManager;
    public CropManager cropManager;

    public Player player;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        ItemManager = GetComponent<ItemManager>();
        tileManager = GetComponent<TileManager>();
        uiManager = GetComponent<UI_Manager>();
        cropManager = GetComponent<CropManager>();
    }

    public void UpdatePlayerMoney(int value)
    {
        player.money += value;
        uiManager.RefreshAll();
    }
}
