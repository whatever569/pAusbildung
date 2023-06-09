using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
[RequireComponent(typeof(CustomTags))]
public class Car : MonoBehaviour
{
    public static int playerNumber = 0;
    public static List<Car> players = new List<Car>();
    public string playerName;
    public int playerHealth = 100;
    public Color playerColor;
    public int score = 0;
    public GameObject gun;

    private void Awake() {
        players.Add(this);
        playerNumber++;
        if(playerName == "")
        playerName = "Player " + playerNumber;
        gameObject.name = playerName;
    }

    /// <summary>
    /// Take away health from player
    /// </summary>
    /// <param name="damage">Amount of damage to take</param>
    /// <param name="bulletOriginName">Name of the bullet origin, when this car gets destroyed it adds score to the bulletOriginName car</param>
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

    public CarData GetCarData()
    {
        return new CarData(GetComponent<CarController>().GetCarControllerSettings(), gun.GetComponent<GunController>().GetGunControllerSettings());
    }

    /// <summary>
    /// Tip: Get cardata using GetCarData() and set it using SetCarData(), edit the desired settings on the CarData struct and then set it using SetCarData()
    /// </summary>
    public void SetCarData(CarData carData)
    {
        GetComponent<CarController>().SetCarControllerSettings(carData.CarControllerSettings);
        gun.GetComponent<GunController>().SetGunControllerSettings(carData.GunControllerSettings);
    }
    
}


