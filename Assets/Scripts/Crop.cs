using UnityEngine;

public class Crop : Item
{
    public int cropIndex = 0;
    [SerializeField] float growTime;
    [SerializeField] BoxCollider2D collider;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Item grownCrop;
    public bool isSeedGrown;

    public void StartToGrow()
    {
        collider.enabled = false;
        sprite.sprite = null;
        cropIndex = 0;
        isSeedGrown = false;
        InvokeRepeating("Grow", 1, growTime);
    }

    public void ContinueToGrow(int index)
    {
        collider.enabled = false;
        sprite.sprite = null;
        cropIndex = index;
        isSeedGrown = false;
        InvokeRepeating("Grow", 1, growTime);
    }

    public void Grow()
    {
        if(cropIndex < 5)
        {
            if (cropIndex == 4)
            {
                isSeedGrown = true;
                CancelInvoke();
            }
            GameManager.instance.cropManager.GrowSeed(this, cropIndex, cropIndex + 1);
            cropIndex++;
        }
    }

    public void EndGrow(Vector3Int position)
    {
        Instantiate(grownCrop, position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        Destroy(this.gameObject);
    }
}