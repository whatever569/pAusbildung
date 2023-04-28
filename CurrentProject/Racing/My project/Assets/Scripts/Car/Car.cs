using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Car : MonoBehaviour
{
    public static int playerNumber = 0;
    public static List<Car> players = new List<Car>();
    public string playerName;
    public int playerHealth = 100;
    public Color playerColor;
    public int score = 0;

    private void Awake() {
        players.Add(this);
        playerNumber++;
        if(playerName == "")
        playerName = "Player " + playerNumber;
        gameObject.name = playerName;
    }

    public void TakeDamage(int damage, string bulletOriginName)
    {
        
        playerHealth -= damage;
        if (playerHealth <= 0) {
            GameObject bulletOrigin = GameObject.Find(bulletOriginName);
            Car bulletOriginScript = bulletOrigin.GetComponent<Car>();
            bulletOriginScript.score++;
            Destroy(gameObject);
    }
}
}


