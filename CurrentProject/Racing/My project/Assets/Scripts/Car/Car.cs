using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public static int playerNumber = 0;
    public string playerName;
    public int killScore = 0;
    public int playerHealth = 100;

    private void Awake() {
        playerNumber++;
        playerName = "Player " + playerNumber;
        gameObject.name = playerName;
    }

    public void TakeDamage(int damage, string bulletOriginName)
    {
        
        playerHealth -= damage;
        if (playerHealth <= 0) {
            //Todo: add kill score to the player who shot the bullet
            Destroy(gameObject);
    }
}
}


