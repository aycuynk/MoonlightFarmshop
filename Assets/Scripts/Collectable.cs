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
                player.inventory.Add("Backpack", item);
                Destroy(this.gameObject);
            }
        }

    }
}