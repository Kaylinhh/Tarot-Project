using UnityEngine;
using UnityEngine.EventSystems;

public class GrimoireOverlay : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Vérifie si on a cliqué sur l'overlay (pas sur le livre)
        if (eventData.pointerCurrentRaycast.gameObject == gameObject)
        {
            Debug.Log("[GrimoireOverlay] Clicked outside - closing");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("[GrimoireOverlay] Clicked on book - staying open");
        }
    }
}