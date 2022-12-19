using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;
    [SerializeField] Rigidbody2D rb;
    Vector3 oldPos;

    public void PopUp()
    {
        if (rb != null)
        {
            oldPos = transform.position;
            Vector2 forceVector = new Vector2(0, 2);
            rb.AddForce(forceVector, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        if(transform.position.y < oldPos.y && rb != null)
        {
            rb.gravityScale = 0;
        }
    }
}

public enum ItemTypes
{
    SEED,
    TOOL,
    NONE
}