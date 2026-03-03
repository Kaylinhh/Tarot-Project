using UnityEngine;
using UnityEngine.UI;

public class TabSystem : MonoBehaviour
{
    [Header("Tabs")]
    [SerializeField] Toggle tabAlcohol;
    [SerializeField] Toggle tabFruit;
    [SerializeField] Toggle tabHerb;
    [SerializeField] Toggle tabLiquor;
    [SerializeField] Toggle tabMixer;
    [SerializeField] Toggle tabOther;
    [SerializeField] Toggle tabSyrup;

    [Header("Content Panels")]
    [SerializeField] GameObject alcoholContent;
    [SerializeField] GameObject fruitContent;
    [SerializeField] GameObject herbContent;
    [SerializeField] GameObject liquorContent;
    [SerializeField] GameObject mixerContent;
    [SerializeField] GameObject otherContent;
    [SerializeField] GameObject syrupContent;

    void Start()
    {
        // Subscribe to toggles
        tabAlcohol.onValueChanged.AddListener((isOn) => { if (isOn) ShowContent(alcoholContent); });
        tabFruit.onValueChanged.AddListener((isOn) => { if (isOn) ShowContent(fruitContent); });
        tabHerb.onValueChanged.AddListener((isOn) => { if (isOn) ShowContent(herbContent); });
        tabLiquor.onValueChanged.AddListener((isOn) => { if (isOn) ShowContent(liquorContent); });
        tabMixer.onValueChanged.AddListener((isOn) => { if (isOn) ShowContent(mixerContent); });
        tabOther.onValueChanged.AddListener((isOn) => { if (isOn) ShowContent(otherContent); });
        tabSyrup.onValueChanged.AddListener((isOn) => { if (isOn) ShowContent(syrupContent); });

        // Show first one by default
        ShowContent(alcoholContent);
    }

    void ShowContent(GameObject contentToShow)
    {
        // Hide all
        alcoholContent.SetActive(false);
        fruitContent.SetActive(false);
        herbContent.SetActive(false);
        liquorContent.SetActive(false);
        mixerContent.SetActive(false);
        otherContent.SetActive(false);
        syrupContent.SetActive(false);

        // Show the one we want
        contentToShow.SetActive(true);
    }
}