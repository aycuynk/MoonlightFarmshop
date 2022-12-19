using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : AIPath
{
    [SerializeField] Animator anim;
    private Transform leavePoint;

    public void SetTarget(Transform target, Transform leavePoint)
    {
        anim = transform.GetChild(0).GetComponent<Animator>();

        float rndX = Random.Range(-2f, 2f);
        float rndY = Random.Range(-0.1f, 0f);

        target.localPosition = new Vector2(rndX, rndY);
        this.target = target;
        this.leavePoint = leavePoint;
    }

    public void SetMovement(bool value)
    {
        Vector2 deltaPos = target.position - transform.position;

        Vector2 direction = new Vector2(Mathf.Sign(deltaPos.x), Mathf.Sign(deltaPos.y));

        anim.SetFloat("horizontal", direction.x);
        anim.SetFloat("vertical", direction.y);
        anim.SetBool("isMoving", value);

    }

    public override void OnTargetReached()
    {
        base.OnTargetReached();
        if(target.tag == "Counter")
        {
            SetMovement(false);
            Destroy(target.gameObject);
            target = null;
            BuyItem();
            Invoke("Leave", 2f);
        }
        else
        {
            Destroy(target.gameObject);
            target = null;
            gameObject.SetActive(false);
        }

    }

    private void BuyItem()
    {
        List<Counter.CounterSlot> filledSlots = GameManager.instance.counterManager.GetFilledSlots();
        if (filledSlots.Count > 0)
        {
            int rnd = Random.Range(0, filledSlots.Count);
            Item item = GameManager.instance.ItemManager.GetItemByName(filledSlots[rnd].itemName);
            Transform buyFX = transform.GetChild(1);
            buyFX.GetComponent<TextMesh>().text = item.data.sellPrice.ToString();
            buyFX.gameObject.SetActive(true);
            GameManager.instance.counterManager.SellItem(filledSlots[rnd]);
        }
    }

    private void Leave()
    {
        target = leavePoint;
        SetMovement(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
