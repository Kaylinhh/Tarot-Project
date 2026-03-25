using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailySummaryUI : MonoBehaviour
{
    // ===== CONFIGURATION =====
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardContainer;
    [SerializeField] private GameObject deckButton;
    [SerializeField] private GameObject endText;

    // ===== STATE =====
    private List<GameObject> instantiatedCards = new List<GameObject>();
    private bool isRevealing = false;

    // ===== UI BUTTONS TO HIDE =====
    private GameObject grimoireIcon;
    private GameObject historyLogButton;

    void Start()
    {
        if (DataManager.Instance != null)
        {
            List<CharacterData> todaysCharacters = DataManager.Instance.charactersOfTheDay;
            Setup(todaysCharacters.ToArray());
        }
        else
        {
            Debug.LogError("DataManager not found in CardReading scene!");
        }

        grimoireIcon = GameObject.Find("IconGrimoireButton");
        grimoireIcon?.SetActive(false);
        historyLogButton = GameObject.Find("HistoryLogButton");
        historyLogButton?.SetActive(false);

    }

    public void Setup(CharacterData[] charactersOfTheDay)
    {
        // clean up any existing cards
        foreach (Transform child in cardContainer)
            Destroy(child.gameObject);

        instantiatedCards.Clear();

        // create a card for each character and set it up
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
