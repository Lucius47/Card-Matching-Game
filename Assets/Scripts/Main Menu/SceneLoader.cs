using UnityEngine;
using System.Collections;

public class SceneLoader: MonoBehaviour
{
	[SerializeField] private SceneAnimationPlayer sceneLoader;
	

	
	public void EasySceneLoader()
	{
		StartCoroutine(sceneLoader.LoadScene(1));
	}
	public void MediumSceneLoader()
	{
		StartCoroutine(sceneLoader.LoadScene(2));
	}
	public void HardSceneLoader()
	{
		StartCoroutine(sceneLoader.LoadScene(3));
	}

	
}
