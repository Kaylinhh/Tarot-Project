using UnityEngine;
using UnityEngine.EventSystems;

public class GrimoireOverlay : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the click was on the overlay itself (outside the book)
        if (eventData.pointerCurrentRaycast.gameObject == gameObject)
            gameObject.SetActive(false);
    }
}