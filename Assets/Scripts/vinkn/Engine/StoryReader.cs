using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace vinkn
{
    public class StoryReader : MonoBehaviour
    {
        // ===== TYPES POUR EVENTS INK =====
        [System.Serializable]
        public class StoryEventTrigger : UnityEvent<StoryReader, object[]> { }

        [System.Serializable]
        public class VariableEventTrigger : UnityEvent<string, object> { }

        [System.Serializable]
        public class EventLink
        {
            public string eventName;
            public StoryEventTrigger onTrigger;
        }

        [System.Serializable]
        public class VariableLink
        {
            public string variableName;
            public VariableEventTrigger onTrigger;
        }

        // ===== STORY =====
        public Story story { get; private set; }
        public bool isPaused = false;

        // ===== CONFIGURATION =====
        [Header("Configuration")]
        [SerializeField] bool startOnAwake;
        [SerializeField] TextAsset storyAsset;
        [SerializeField] List<EventLink> eventList;
        [SerializeField] List<VariableLink> variableList;

        // ===== EVENTS =====
        [Header("Events")]
        [SerializeField] UnityEvent<string, List<string>> OnNextLine;
        [SerializeField] UnityEvent<List<Choice>> OnChoices;
        [SerializeField] UnityEvent OnStoryEnd;
        [SerializeField] UnityEvent OnMinigameStart;
        [SerializeField] UnityEvent OnMinigameEnd;

        // ===== INITIALIZATION =====
        void Awake()
        {
            if (storyAsset == null)
            {
                gameObject.SetActive(false);
                throw new UnassignedReferenceException("storyAsset is empty");
            }

            story = new Story(storyAsset.text);

            // Bind external functions (loadScene, meetCharacter, etc.)
            foreach (EventLink e in eventList)
            {
                story.BindExternalFunctionGeneral(e.eventName, (object[] args) =>
                {
                    e.onTrigger?.Invoke(this, args);
                    return null;
                }, false);
            }

            // Observe Ink variables
            foreach (VariableLink v in variableList)
            {
                story.ObserveVariable(v.variableName, (string varName, object varValue) =>
                    v.onTrigger?.Invoke(varName, varValue));
            }
        }

        protected virtual IEnumerator Start()
        {
            yield return null;

            if (startOnAwake)
                Next();
        }

        // ===== DIALOGUE FLOW =====
        public virtual void Next()
        {
            if (isPaused)
                return;

            if (story.canContinue)
            {
                string content = story.Continue().Trim();

                // Handle PAUSE tag (scene change)
                if (story.currentTags.Contains("PAUSE"))
                {
                    isPaused = true;
                    if (string.IsNullOrEmpty(content))
                        return;
                }

                // Handle PAUSE_MINIGAME tag
                if (story.currentTags.Contains("PAUSE_MINIGAME"))
                {
                    isPaused = true;
                    OnMinigameStart?.Invoke();

                    if (string.IsNullOrEmpty(content))
                        return;
                }

                // Skip empty lines
                if (string.IsNullOrEmpty(content))
                {
                    Next();
                    return;
                }

                // Log to history
                DialogueHistory.Instance?.AddEntry(content, story.currentTags);

                // Display line
                OnNextLine?.Invoke(content, story.currentTags);
            }
            else if (story.currentChoices?.Count > 0)
            {
                OnChoices?.Invoke(story.currentChoices);
            }
            else
            {
                OnStoryEnd?.Invoke();
            }
        }

        public void Resume()
        {
            isPaused = false;
            OnMinigameEnd?.Invoke();
            Next();
        }

        public virtual void SelectChoice(Choice choice)
        {
            story.ChooseChoiceIndex(choice.index);
            Next();
        }

        // ===== UTILITY =====
        public void SetStory(TextAsset asset)
        {
            storyAsset = asset;
            story = new Story(asset.text);
        }
    }
}