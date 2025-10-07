using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailySummaryUI : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;      // ton prefab CardUI
    [SerializeField] private Transform cardContainer;    // le container avec layout group

    private List<GameObject> instantiatedCards = new List<GameObject>();
    private bool isRevealing = false;

    void Start()
    {

    }

    public void Setup(CharacterData[] charactersOfTheDay)
    {
        // Nettoyer d'anciennes cartes
        foreach (Transform child in cardContainer)
            Destroy(child.gameObject);

        instantiatedCards.Clear();

        // Créer une carte pour chaque personnage du jour
        foreach (var character in charactersOfTheDay)
        {
            GameObject cardGO = Instantiate(cardPrefab, cardContainer);
            cardGO.GetComponent<CardUI>().Setup(character);
            cardGO.SetActive(false); // invisible au départ
            instantiatedCards.Add(cardGO);
            Debug.Log($"SETUP characters of the day: {character.characterName}");

        }
    }

    public void OnClickDeck()
    {
        if (DayDataManager.Instance != null)
        {
            List<CharacterData> todaysCharacters = DayDataManager.Instance.charactersOfTheDay;
            Setup(todaysCharacters.ToArray());
            Debug.Log("Todays characters count: " + todaysCharacters.Count);

        }
        else
        {
            Debug.LogError("DayDataManager not found in CardReading scene!");
        }

        if (!isRevealing)
            StartCoroutine(RevealCardsCoroutine());
    }

    private IEnumerator RevealCardsCoroutine()
    {
        isRevealing = true;

        foreach (var card in instantiatedCards)
        {
            card.SetActive(true);
            yield return new WaitForSeconds(1f); // délai entre chaque révélation
        }

        isRevealing = false;
    }
}
