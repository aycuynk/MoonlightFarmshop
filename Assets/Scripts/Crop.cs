using UnityEngine;

public class Crop : Item
{
    private int cropIndex = 1;
    [SerializeField] float growTime;
    [SerializeField] BoxCollider2D collider;
    [SerializeField] SpriteRenderer sprite;


    public void StartToGrow()
    {
        collider.enabled = false;
        sprite.sprite = null;
        cropIndex = 1;
        InvokeRepeating("Grow", 1, growTime);
    }

    public void Grow()
    {
        if(cropIndex <= 5)
        {
            GameManager.instance.cropManager.GrowSeed(this, cropIndex - 1, cropIndex);
            cropIndex++;
        }
        else
        {
            CancelInvoke();
        }
    }
}