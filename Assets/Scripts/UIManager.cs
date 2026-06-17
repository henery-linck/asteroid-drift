using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject ScorePanel;
    [SerializeField] private TMP_Text timerText;

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
            return;

        timerText.text = GameManager.Instance.GetFormattedSurvivalTime();
    }
}
