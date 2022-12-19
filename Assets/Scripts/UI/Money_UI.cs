using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money_UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Animator anim;

    private void Start()
    {
        SetText(GameManager.instance.data.playerData.money);
    }

    public void SetText(int value)
    {
        moneyText.text = value.ToString();
        anim.Play("UpdateMoney", 0, 0);
    }
}
