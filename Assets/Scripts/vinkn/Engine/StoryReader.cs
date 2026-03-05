using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.Events;

namespace vinkn
{
    public class StoryReader : MonoBehaviour
    {
        public enum StoryReadState { NONE, READ, WAIT, RESUME };

        [System.Serializable] public class StoryEventTrigger : UnityEvent<StoryReader, object[]> { }
        [System.Serializable] public class VariableEventTrigger : UnityEvent<string, object> { }

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

        public Story story { get; private set; }

        [Header("Configuration")]
        [SerializeField] bool startOnAwake;
        [SerializeField] TextAsset storyAsset;
        [SerializeField] List<EventLink> eventList;
        [SerializeField] List<VariableLink> variableList;

        [Header("Events")]
        [SerializeField] UnityEvent<string, List<string>> OnNextLine;
        [SerializeField] UnityEvent<List<Choice>> OnChoices;
        [SerializeField] UnityEvent OnStoryEnd;

        public StoryReadState state { get; private set; }
        public bool isPaused = false;

        NovelCanvas novelCanvas;

        void Awake()
        {
            if (storyAsset == null)
            {
                gameObject.SetActive(false);
                throw new UnassignedReferenceException("storyAsset is empty");
            }

            story = new Story(storyAsset.text);
            state = StoryReadState.READ;

            foreach (EventLink e in eventList)
            {
                story.BindExternalFunctionGeneral(e.eventName, (object[] args) =>
                {
                    e.onTrigger?.Invoke(this, args);
                    return null;
                }, false);
            }

            foreach (VariableLink v in variableList)
            {
                story.ObserveVariable(v.variableName, (string varName, object varValue) =>
                    v.onTrigger?.Invoke(varName, varValue));
            }

            novelCanvas = FindAnyObjectByType<NovelCanvas>();
        }

        protected virtual IEnumerator Start()
        {
            yield return null;

            if (startOnAwake)
                Next();
        }

        public virtual void Next()
        {
            Debug.Log($"Next() called - isPaused: {isPaused}, canContinue: {story.canContinue}");

            if (isPaused)
            {
                Debug.Log("Story is paused");
                return;
            }

            if (story.canContinue)
            {
                string content = story.Continue().Trim();

                // PAUSE to change scene
                if (story.currentTags.Contains("PAUSE"))
                {
                    isPaused = true;
                    if (string.IsNullOrEmpty(content))
                    {
                        return;
                    }
                }

                if (story.currentTags.Contains("PAUSE_MINIGAME"))
                {
                    isPaused = true;
                    NovelCanvas canvas = FindAnyObjectByType<NovelCanvas>();
                    if (canvas != null)
                    {
                        canvas.DisplayUI(false);
                    }
                    if (string.IsNullOrEmpty(content))
                    {
                        return;
                    }
                }

                // Si content vide, skip et rappelle Next()
                if (string.IsNullOrEmpty(content))
                {
                    Debug.Log("Empty content, calling Next() again");
                    Next();
                    return;
                }

                // LOG HISTORY (seulement si content existe)
                if (DialogueHistory.Instance != null)
                {
                    DialogueHistory.Instance.AddEntry(content, story.currentTags);
                }

                // INVOKE UNE SEULE FOIS
                OnNextLine?.Invoke(content, story.currentTags);
            }
            else if (story.currentChoices?.Count > 0)
            {
                Debug.Log($"Choices available: {story.currentChoices.Count}");
                OnChoices?.Invoke(story.currentChoices);
            }
            else
            {
                Debug.Log("Story ended");
                OnStoryEnd?.Invoke();
            }
        }

        public void Resume()
        {
            Debug.Log($"RESUME CALLED - isPaused BEFORE: {isPaused}");
            isPaused = false;

            // Réaffiche l'UI
            NovelCanvas canvas = FindAnyObjectByType<NovelCanvas>();
            if (canvas != null)
            {
                canvas.DisplayStory(true);
                Debug.Log("[SR] UI shown on Resume");
            }

            Debug.Log($"isPaused NOW: {isPaused}");
            Next();
        }

        public virtual void SelectChoice(Choice choice)
        {
            story.ChooseChoiceIndex(choice.index);
            Next();
        }

        public void CancelWait()
        {
            if (state == StoryReadState.WAIT)
            {
                state = StoryReadState.READ;
            }
        }

        public void ThenWaitPlayer()
        {
            state = StoryReadState.WAIT;
        }

        public void SetStory(TextAsset asset)
        {
            storyAsset = asset;
            story = new Story(asset.text);
        }
    }
}