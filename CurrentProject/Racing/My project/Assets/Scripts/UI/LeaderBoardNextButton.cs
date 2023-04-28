using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeaderBoardNextButton : MonoBehaviour
{
    public Button nextButton;

    private void Start()
    {
        nextButton.onClick.AddListener(NextButton);
    }
    public void NextButton()
    {
        GameManager.instance.GoToMainMenu();

}
}
