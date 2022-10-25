using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Item item = GetComponent<Item>();
            Player player = collision.GetComponent<Player>();
            if (item != null)
            {

                if (player.inventory.IsInventoryFull("Toolbar", item.data.name, 9))
                {
                    player.inventory.Add("Backpack", item);
                }
                else
                {
                    player.inventory.Add("Toolbar", item);
                }
                GameManager.instance.uiManager.RefreshInventoryUI("Toolbar");
                Destroy(this.gameObject);
            }
        }

    }
}