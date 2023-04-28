using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] GameObject leaderBoardPositionPrefab;

    private void Start() {
        for(int i = 0; i < Car.players.Count; i++)
        {
            GameObject leaderBoardPosition = Instantiate(leaderBoardPositionPrefab);
            //make it child of this
            leaderBoardPosition.transform.SetParent(transform, false);
            leaderBoardPosition.GetComponent<LeaderBoardPosition>().SetName(Car.players[i].playerName);
            leaderBoardPosition.GetComponent<LeaderBoardPosition>().SetScore(Car.players[i].score);
            leaderBoardPosition.GetComponent<LeaderBoardPosition>().SetHealth(Car.players[i].playerHealth);
            leaderBoardPosition.GetComponent<LeaderBoardPosition>().SetLeaderBoardPostionBackgroundColor(Car.players[i].playerColor);
            leaderBoardPosition.GetComponent<LeaderBoardPosition>().SetPosition(i, 200, 125);
        }
    }
}
