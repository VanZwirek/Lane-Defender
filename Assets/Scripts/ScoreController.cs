using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int highScore;
    public int score;

    public TMP_Text highScoreText;
    public TMP_Text scoreText;

    static public bool once_call;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!once_call)
        {
            DontDestroyOnLoad(this);
            once_call = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = ("Score:" + score.ToString());
        highScoreText.text = ("High Score:" + highScore.ToString());

        if (highScore < score)
        {
            Debug.Log("new record!");
            highScore = score;
        }
    }
}
