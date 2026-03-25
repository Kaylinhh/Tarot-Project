using UnityEngine;
using UnityEngine.EventSystems;

public class ConditionalEventSystem : MonoBehaviour
{
    void Start()
    {
        if (!GameModeManager.Instance.IsArcadeMode)
        {
            gameObject.SetActive(false);
        }
    }
}