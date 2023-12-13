using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] HealthBarScript healthbar;

    //Method that has the player take damage
    private void PlayerHealthDamage(int damage)
    {
        GameManager.gameManager.playerHealth.damageHealth(damage);
        healthbar.SetHealth(GameManager.gameManager.playerHealth.Health);
    }

    //Method that heals the player
    private void PlayerHeal(int healing)
    {
        GameManager.gameManager.playerHealth.regenHealth(healing);
        healthbar.SetHealth(GameManager.gameManager.playerHealth.Health);
    }
}
