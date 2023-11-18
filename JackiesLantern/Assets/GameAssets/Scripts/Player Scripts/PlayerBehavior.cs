using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] HealthBarScript healthbar;


  
   /* void Update()
    {//This is to varify that the player is actually losing and regening health
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerHealthDamage(20);
            Debug.Log(GameManager.gameManager.playerHealth.Health);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayerHeal(15);
            Debug.Log(GameManager.gameManager.playerHealth.Health);
        }
     }*/

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
