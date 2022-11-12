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
    public GameData data;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        if(SaveManager.ReadFromJSON<GameData>("player_data") != null)
        {
            data = SaveManager.ReadFromJSON<GameData>("player_data");
        }
        else
        {
            Save();
            print("Data file created!");
            data = SaveManager.ReadFromJSON<GameData>("player_data");
        }


        ItemManager = GetComponent<ItemManager>();
        tileManager = GetComponent<TileManager>();
        uiManager = GetComponent<UI_Manager>();
        cropManager = GetComponent<CropManager>();

    }

    public void UpdatePlayerMoney(int value)
    {
        player.money += value;
        uiManager.RefreshAll();
        data.playerData.money = player.money;
        Save();
    }

    public void Save()
    {
        SaveManager.SaveToJSON<GameData>(data, "player_data");
    }
}
