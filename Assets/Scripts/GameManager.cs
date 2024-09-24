using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton
    private float finalTime; 
    private int finalScore;

    private void Awake()
    {
    
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void SetFinalTime(float time)
    {
        finalTime = time;
    }

    public float GetFinalTime()
    {
        return finalTime;
    }

      public void SetFinalScore(int score)
    {
        finalScore = score;
    }

    public int GetFinalScore()
    {
        return finalScore;
    }
}