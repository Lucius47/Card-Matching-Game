using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EasyCardsSpawner : MonoBehaviour
{
    private int numberOfRows = 3;
    private int numberOfCols = 2;
    private float offsetX = 1.9f;
    private float offsetY = 2.3f;

    [SerializeField] private Sprite[] allImages;
    [SerializeField] private GameObject cardPrefab;
    private GameObject card;

    public int numberOfCardBacks = 3;


    public IEnumerator PlaceCards()
    {
        int randomCardbackIndex = UnityEngine.Random.Range(0, numberOfCardBacks);

        int[] numbers = new int[] {0, 0, 1, 1, 2, 2};
        numbers = ShuffleArray(numbers);

        //Take a random list from the array of all images;
        List<Sprite> selectedImages = new List<Sprite>();
		for (int k = 0; k < 10; k++)
		{
            int randomIndex = UnityEngine.Random.Range(0, allImages.Length);
            if (!selectedImages.Contains(allImages[randomIndex]) && selectedImages.Count != 3)
			{
                selectedImages.Add(allImages[randomIndex]);
            }
            
		}


        for (int i = 0; i < numberOfCols; i++)
        {
            for (int j = 0; j < numberOfRows; j++)
            {

                card = Instantiate(cardPrefab) as GameObject;
                MemoryCard memoryCard = card.GetComponent<MemoryCard>();
                


                int index = j * numberOfCols + i;
                int id = numbers[index];

                memoryCard.SetCard(id, selectedImages[id]);

                float posX = (offsetX * i) + transform.position.x;
                float posY = -(offsetY * j) + transform.position.y;
                card.transform.position = new Vector3(posX, posY, transform.position.z);
                card.transform.SetParent(transform);

                memoryCard.randomCardbackIndex = randomCardbackIndex;

                yield return new WaitForSeconds(0.1f);
            }
        }
        {

        }
    }


















    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int temp = newArray[i];
            int r = UnityEngine.Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = temp;
        }
        return newArray;
    }
}
