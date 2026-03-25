using UnityEngine;

namespace vinkn
{
    public class DisplayAnchor : MonoBehaviour
    {
        public string tagID => inkTag;

        [SerializeField] string inkTag;

        void Start()
        {
            if (VNEngine.Instance != null)
            {
                VNEngine.Instance.Add(this);
            }
            else
            {
                Debug.LogWarning($"[DisplayAnchor] VNEngine.Instance is null! Anchor '{inkTag}' not registered.");
            }
        }
    }
}