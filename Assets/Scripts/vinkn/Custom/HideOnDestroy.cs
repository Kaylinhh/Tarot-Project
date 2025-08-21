using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnDestroy : MonoBehaviour
{
    private void Start()
    {
        FindAnyObjectByType<NovelCanvas>().DisplayUI(true);

    }
    private void OnDestroy()
    {
        FindAnyObjectByType<NovelCanvas>().DisplayUI(false);
    }
}
