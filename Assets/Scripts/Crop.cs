using UnityEngine;

public class Crop : Item
{
    private int indexOfCropData;
    public int cropIndex = 0;
    [SerializeField] string cropName => data.itemName;
    [SerializeField] float growTime;
    [SerializeField] BoxCollider2D collider;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Item grownCrop;
    public bool isSeedGrown;

    CropData cropData;
    Vector3Int cropPos;

    public void StartToGrow(Vector3Int pos)
    {
        cropData = new CropData();
        cropData.cropName = cropName;
        cropData.cropPosition = pos;
        GameManager.instance.data.cropDatas.Add(cropData);
        indexOfCropData = GameManager.instance.data.cropDatas.IndexOf(cropData);
        collider.enabled = false;
        sprite.sprite = null;
        cropIndex = 0;
        isSeedGrown = false;
        InvokeRepeating("Grow", 1, growTime);
    }

    public void ContinueToGrow(int index, int indexCrop)
    {
        cropData = GameManager.instance.data.cropDatas[indexCrop];
        indexOfCropData = indexCrop;
        collider.enabled = false;
        sprite.sprite = null;
        cropIndex = index;
        isSeedGrown = false;
        InvokeRepeating("Grow", 1, growTime);
    }

    public void Grow()
    {
        if(cropIndex < 6)
        {
            //GameManager.instance.cropManager.GrowSeed(this, cropIndex, cropIndex + 1);
            cropPos = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
            GameManager.instance.cropManager.UpdateSeedTile(cropPos, data.cropTiles[cropIndex]);
            GameManager.instance.data.cropDatas[indexOfCropData].growIndex = cropIndex;

            if (cropIndex == 5)
            {
                isSeedGrown = true;
                CancelInvoke();
            }

            GameManager.instance.Save();
            cropIndex++;
        }
    }

    public void EndGrow(Vector3Int position)
    {
        Instantiate(grownCrop, position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        grownCrop.PopUp();
        Destroy(this.gameObject);
    }

    public void UpdateIndexOfData()
    {
        indexOfCropData = GameManager.instance.data.cropDatas.IndexOf(cropData);
    }
}