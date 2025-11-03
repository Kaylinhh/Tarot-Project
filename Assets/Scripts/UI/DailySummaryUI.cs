using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailySummaryUI : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;      // ton prefab CardUI
    [SerializeField] private Transform cardContainer;    // le container avec layout group
    [SerializeField] private GameObject deckButton;
    private List<GameObject> instantiatedCards = new List<GameObject>();
    private bool isRevealing = false;
    private GameObject grimoireIcon;

    void Start()
    {
        if (DataManager.Instance != null)
        {
            List<CharacterData> todaysCharacters = DataManager.Instance.charactersOfTheDay;
            Setup(todaysCharacters.ToArray());
            Debug.Log("Todays characters count: " + todaysCharacters.Count);

        }
        else
        {
            Debug.LogError("DayDataManager not found in CardReading scene!");
        }

        grimoireIcon = GameObject.Find("IconGrimoireButton");
        grimoireIcon.SetActive(false);

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
            cardGO.SetActive(true); 
            var canvasGroup = cardGO.GetComponent<CanvasGroup>();
            
            if (canvasGroup == null )
                canvasGroup = cardGO.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;

            cardGO.GetComponent<CardUI>().Setup(character);
            instantiatedCards.Add(cardGO);

        }
    }

    public void OnClickDeck()
    {
        deckButton.SetActive(false);

        if (!isRevealing)
            StartCoroutine(RevealCardsCoroutine());
    }

    private IEnumerator RevealCardsCoroutine()
    {
        isRevealing = true;

        foreach (var card in instantiatedCards)
        {
            StartCoroutine(FadeIn(card));
            yield return new WaitForSeconds(0.5f);
        }

        isRevealing = false;
    }

    private IEnumerator FadeIn(GameObject card)
    {
        var canvasGroup = card.GetComponent<CanvasGroup>();
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration) 
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }
}
