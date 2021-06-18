using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    public int id { get; private set; }

    [SerializeField] private GameObject cardBackPrefab;
    private GameObject cardBack;
	[SerializeField] private Sprite[] cardBackImages;

	public float animationDuration = 0.2f;
	[HideInInspector] public int randomCardbackIndex;

	private GameController controller;



	void Start()
    {
		

		controller = (GameController)FindObjectOfType(typeof(GameController));
		cardBack = Instantiate(cardBackPrefab) as GameObject;
		cardBack.GetComponent<SpriteRenderer>().sprite = cardBackImages[randomCardbackIndex];

        cardBack.transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
		StartCoroutine(CardSpawnAnimation(cardBack));
		StartCoroutine(CardSpawnAnimation(this.gameObject));
		cardBack.transform.SetParent(transform);
	}

    

    public void SetCard(int id, Sprite image)
	{
		this.id = id;
		GetComponent<SpriteRenderer>().sprite = image;
	}
	public void Unreveal()
	{
		cardBack.SetActive(true);
	}

	private void OnMouseDown()
	{
		if (cardBack.activeSelf && controller.canReveal)
		{
			cardBack.SetActive(false);
			controller.CardRevealed(this);
			StartCoroutine(CardTouchAnimation(this.gameObject));
		}
	}




	public IEnumerator DestroyCard()
	{
		StartCoroutine(CardTouchAnimation(this.gameObject));
		yield return new WaitForSeconds(0.5f);
		Destroy(this.gameObject);
		
	}

	private IEnumerator CardSpawnAnimation(GameObject card)
	{

		Vector3 actualScale = card.transform.localScale;
		Vector3 targetScaleMin = new Vector3(0.8f, 0.8f, 0.8f);
		Vector3 targetScaleMax = new Vector3(1.2f, 1.2f, 1.2f);

		for (float t = 0; t < 1; t += Time.deltaTime / animationDuration)
		{
			card.transform.localScale = Vector3.Lerp(targetScaleMin, targetScaleMax, t);

			yield return null;




		}
		for (float j = 0; j < 1; j += Time.deltaTime / animationDuration)
		{
			card.transform.localScale = Vector3.Lerp(targetScaleMax, actualScale, j);

			yield return null;
		}



	}


	private IEnumerator CardTouchAnimation(GameObject card)
	{

		Vector3 actualScale = card.transform.localScale;
		Vector3 targetScaleMin = new Vector3(0.8f, 0.8f, 0.8f);
		Vector3 targetScaleMax = new Vector3(1.2f, 1.2f, 1.2f);

		for (float t = 0; t < 1; t += Time.deltaTime / animationDuration)
		{
			card.transform.localScale = Vector3.Lerp(actualScale, targetScaleMin, t);

			yield return null;




		}
		for (float j = 0; j < 1; j += Time.deltaTime / animationDuration)
		{
			card.transform.localScale = Vector3.Lerp(targetScaleMin, actualScale, j);

			yield return null;
		}



	}
}
