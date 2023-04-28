using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int playerNumber = 2;
    public string winner = "None";
    public float timeOfGameInMinutes = 0.5f;
    [SerializeField] private TextMeshProUGUI timerLabel;

    [Header("End Screen")] 
    [SerializeField] private GameObject endScreen, leaderBoardPositionPrefab, canvas;


private void Awake() {
if (instance == null)
{
    instance = this;
}
else
{
    Destroy(gameObject);
}
}
    private void Start() {
        StartCoroutine(StartGame(timeOfGameInMinutes));
        StartCoroutine(TimerLabel());
    }
    public IEnumerator StartGame(float timeOfGameInMinutes)
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
        if(Car.players[0].score == Car.players[1].score)
        winner = "No One";
        else winner = Car.players[0].playerName;
        //start end screen
        GameObject endScreenObject = Instantiate(endScreen);
        endScreenObject.transform.SetParent(canvas.transform, false);

    }

    public void GoToMainMenu()
    {
        //move to main menu scene
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator TimerLabel()
    {
        for(int timeRightNow = (int)(timeOfGameInMinutes * 60f); timeRightNow > 0; timeRightNow--)
        {
            if(timeRightNow <= 10) timerLabel.color = Color.red;
            if(timeRightNow % 60 < 10)
            timerLabel.text = (timeRightNow / 60) + ":0" + (timeRightNow % 60);
            else timerLabel.text = (timeRightNow / 60) + ":" + (timeRightNow % 60);
            yield return new WaitForSeconds(1f);
        }
    }
}
