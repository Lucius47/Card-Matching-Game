using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
	[SerializeField] private float transitionTime;
    

	public IEnumerator LoadScene (int levelIndex)
	{
		transition.SetTrigger("Start");
		yield return new WaitForSeconds(transitionTime);
		SceneManager.LoadScene(levelIndex);
	}

	public void GoToMainMenu()
	{
		StartCoroutine(LoadScene(0));
	}
}
