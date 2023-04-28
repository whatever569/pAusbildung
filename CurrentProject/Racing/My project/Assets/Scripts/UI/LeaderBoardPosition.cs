using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderBoardPosition : MonoBehaviour
{
   [SerializeField] TMPro.TextMeshProUGUI nameText, scoreText, healthText, positionText;
   Image leaderBoardPositionBackground;
    private void Awake() {
    }
    public void SetName(string name)
    {
        nameText.text = name;
    }
    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
    public void SetHealth(int health)
    {
        healthText.text = health.ToString();
    }
    public void SetLeaderBoardPostionBackgroundColor(Color color)
    {
        leaderBoardPositionBackground = GetComponent<Image>();
        color.a = 1;
        leaderBoardPositionBackground.color = color;
    }
    public void SetPosition(int position, float positionOfTheFirstPosition, float positionDiffernceBetweenLeaderBoardPositions)
    {
        positionText.text = (position + 1).ToString();
        transform.localPosition = new Vector3(0, positionOfTheFirstPosition - positionDiffernceBetweenLeaderBoardPositions * position, 0);
    }
}
