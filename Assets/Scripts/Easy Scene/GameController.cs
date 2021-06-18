using System.Collections;
using UnityEngine;


public class GameController : MonoBehaviour
{
    [SerializeField] private EasyCardsSpawner spawner;
    [SerializeField] private UIController uiController;
    //[SerializeField] private SceneAnimationPlayer sceneLoader;

    [SerializeField] private int numberOfLevels;
    [SerializeField] private int scorePenalty;
    [SerializeField] private int scoreReward;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;



    private void Start()
    {
        NextLevel();
    }

    public bool canReveal
    {
        get { return (_secondRevealed == null && !uiController.gameOver); }
    }

    public void CardRevealed(MemoryCard card)
    {
        
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }

    }



    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            FindObjectOfType<AudioManager>().Play("match");
            uiController.score += scoreReward;
            StartCoroutine(_firstRevealed.DestroyCard());
            StartCoroutine(_secondRevealed.DestroyCard());
            
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            FindObjectOfType<AudioManager>().Play("notMatch");
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
            uiController.score -= scorePenalty;
        }

        _firstRevealed = null;
        _secondRevealed = null;

        StartCoroutine(AreThereAnyCardsLeft());
        
    }

    private IEnumerator AreThereAnyCardsLeft ()
	{
        yield return new WaitForSeconds(0.6f);
        if (FindObjectOfType<MemoryCard>() == null)
        {
            if (uiController.level != numberOfLevels)
			{
                uiController.OnGameWin();
            }
            else
			{
                uiController.OnModeCompleted();
			}
            
        }
    }



    public void NextLevel()
    {
        uiController.TimerReset();
        uiController.gameOver = false;
        uiController.gameWinUI.gameObject.SetActive(false);
        uiController.SetLevel();
        StartCoroutine(spawner.PlaceCards());
    }

    
}
