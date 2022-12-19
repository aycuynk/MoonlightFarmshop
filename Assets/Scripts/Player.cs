using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryManager inventory;
    [SerializeField] Inventory toolBarInventory;
    [SerializeField] Animator anim;
    [SerializeField] Vector2Int positionOffset;

    private bool isInteractiveItem;
    public int money;

    private void Start()
    {
        toolBarInventory = inventory.GetInventoryByName("Toolbar");
        money = GameManager.instance.data.playerData.money;
    }

    private void Update()
    {
        if (isInteractiveItem)
        {
            Vector3Int position = new Vector3Int((int)transform.position.x + positionOffset.x,
                (int)transform.position.y + positionOffset.y, 0);

            if (!GameManager.instance.tileManager.IsHighlighted(position))
            {
                GameManager.instance.tileManager.SetHighlight(position, true);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {

                if (IsHoeSelected())
                {
                    anim.SetTrigger("isHit 0");
                }
                else
                {
                    GameManager.instance.cropManager.PlantSeed(position);
                }
                    
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.instance.cropManager.Harvest(position);
            }
        }
        else
        {
            GameManager.instance.tileManager.SetHighlight(Vector3Int.zero, false);
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.instance.counterManager.PutItemToSell(transform.position);
            }
        }

        
    }

    public void DropItem(Item item)
    {
        Vector3 spawnLocation = transform.position;

        float randX = Random.Range(-2f, 2f);
        float randY = Random.Range(-2f, 2f);

        Vector3 spawnOffset = new Vector3(randX, randY, 0).normalized;

        Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);
    }

    public void DropItem(Item item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }

    public bool IsHoeSelected()
    {
        string selectedSlot = toolBarInventory.slots[UI_Manager.selectedSlot.slotID].itemName;

        if (selectedSlot == "Hoe")
        {
            return true;
        }

        return false;
    }

    public void Dig()
    {
        Vector3Int position = new Vector3Int((int)transform.position.x + positionOffset.x,
                (int)transform.position.y + positionOffset.y, 0);

        if(GameManager.instance.tileManager.IsInteractable(position))
        {
            GameManager.instance.tileManager.SetInteracted(position);
        }
        else
        {
            GameManager.instance.tileManager.ResetInteractedTile(position);
        }
    }

    public void CheckSelectedItem()
    {
        Item selectedItem = GameManager.instance.ItemManager.GetItemByName(toolBarInventory.slots[UI_Manager.selectedSlot.slotID].itemName);

        if (selectedItem != null && selectedItem.data.itemType != ItemTypes.NONE)
        {
            isInteractiveItem = true;
        }
        else
        {
            isInteractiveItem = false;
        }

    }
}
