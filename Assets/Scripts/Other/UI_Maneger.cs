using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameController : MonoBehaviour
{
    public static GameController UI;

    public TMP_Text txt_Score;

    private int score = 0;

    public void AddScore(int x)
    {
        score += x;
        txt_Score.text = "Score: " + score;
    }

}