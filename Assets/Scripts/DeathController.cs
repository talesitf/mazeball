using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreenController : MonoBehaviour
{

    public TextMeshProUGUI finalTimeText;
    public TextMeshProUGUI finalScoreText;

    public void RestartMiniGame()
    {
        SceneManager.LoadScene("Minigame");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void Start()
    {
        float finalTime = AudioManager.Instance.GetFinalTime();
        int finalScore = AudioManager.Instance.GetFinalScore();
        
        finalTimeText.text = "Final Time: " + finalTime.ToString("F2") + "s";
        finalScoreText.text = "Final Score: " + finalScore.ToString();
    }
}