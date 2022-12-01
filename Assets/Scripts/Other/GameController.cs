using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameController : MonoBehaviour
{
    public static GameController instance;

    public TMP_Text txt_Score;
    public TMP_Text txt_ScoreAtResume;

    private int score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    public void AddScore(int x)
    {
        score += x;
        txt_Score.text = "Score: " + score;
        txt_ScoreAtResume.text = "Your Score: " + score;
    }

}
