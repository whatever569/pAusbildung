using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int playerNumber = 2;
    public string winner = "None";
    private void Start() {
        StartCoroutine(StartGame(0.3f));
    }
    public IEnumerator StartGame(float timeOfGameInMinutes = 0.5f)
    {
        yield return new WaitForSeconds(timeOfGameInMinutes * 60);
        Debug.Log("Game Over");
        GameOver();
    }

    void GameOver()
    {
        //short way to sort by score and then health, without writing own algorithm
        Car.players = Car.players.OrderByDescending(car => car.score)
                          .ThenByDescending(car => car.playerHealth)
                          .ToList();
        //set winner
        winner = Car.players[0].playerName;
        Debug.Log(winner + " wins!");
        //show leaderboard with debuh
        //todo: show leaderboard in game
        Debug.Log("Leaderboard:");
        foreach (Car player in Car.players)
        {
            Debug.Log(player.playerName + " Score: " + player.score + " Health: " + player.playerHealth);
        }
    }
    }
