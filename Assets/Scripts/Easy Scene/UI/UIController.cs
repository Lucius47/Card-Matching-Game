using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] public Canvas gameWinUI;
    [SerializeField] public Canvas gameOverUI;
    [SerializeField] private Text timer;
    [SerializeField] private Text levelNumber;
    [SerializeField] private Canvas ModeCompletedUI;
    [SerializeField] private Text scoreOnGameWin;
    [SerializeField] private Slider timeBar;

    [SerializeField] private float totalTime;
    

    private float timeLeft;
    public int score;
    public bool gameOver = false;
    public int level = 0;

	private void Start()
	{
        TimerReset();
        timeBar.maxValue = totalTime;
	}
	private void Update()
	{
        if (!gameOver)
		{
            scoreText.text = score.ToString();
            if (timeLeft > 0.1f && !gameOver)
            {
                timeLeft -= Time.smoothDeltaTime;
                timer.text = ((int)timeLeft).ToString();
            }

            if (timeLeft <= 0.1f)
            {
                OnGameOver();
            }
            timeBar.value = timeLeft;
        }
	}

    public void SetLevel()
	{
        level++;
        levelNumber.text = "Level " + level;
    }

	

    public void OnGameWin()
	{
        FindObjectOfType<AudioManager>().Play("gameWin");
        gameOver = true;
        score += (int)timeLeft;
        scoreOnGameWin.text = score.ToString();
        gameWinUI.gameObject.SetActive(true);
        
	}
    public void OnGameOver()
    {
        FindObjectOfType<AudioManager>().Play("gameOver");
        gameOver = true;
        gameOverUI.gameObject.SetActive(true);
    }

    public void TimerReset()
	{
        timeLeft = totalTime;
	}

    public void OnModeCompleted()
	{
        FindObjectOfType<AudioManager>().Play("gameWin");
        gameOver = true;
        gameWinUI.gameObject.SetActive(false);

        ModeCompletedUI.gameObject.SetActive(true);
	}
}
