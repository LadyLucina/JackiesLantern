using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }
    
    //setting starting health and max health
    public HealthSystem playerHealth = new HealthSystem(100, 100);

    //Prevents a duplicate of GameManager from being created
    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }

}
