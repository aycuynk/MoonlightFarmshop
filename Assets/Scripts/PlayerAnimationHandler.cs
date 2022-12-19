using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] Movement playerMovement;
    [SerializeField] Player player;

    public void HitEnter()
    {
        playerMovement.SetPlayerSpeed(0);
    }

    public void HitExit()
    {
        player.Dig();
        playerMovement.SetPlayerSpeed(2);
    }
}
