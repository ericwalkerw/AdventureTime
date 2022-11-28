using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Maneger : MonoBehaviour
{
    public static UI_Maneger UI;

    public TMP_Text txt_Score;

    private int score = 0;

    public void AddScore(int x)
    {
        score += x;
        txt_Score.text = "Score: " + score;
    }

}
