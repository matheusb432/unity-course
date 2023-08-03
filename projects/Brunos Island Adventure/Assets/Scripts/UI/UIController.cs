using RPG.Core;
using RPG.Quests;
using RPG.Util;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIController : MonoBehaviour
    {
        public VisualElement root;
        public List<Button> buttons = new();
        public Label healthLabel;
        public Label potionsLabel;
        public VisualElement questItemIcon;

        public AudioClip gameOverAudio;
        public AudioClip victoryAudio;

        public IUIState currentState;
        public UIMainMenuState mainMenuState;
        public UIDialogueState dialogueState;
        public UIQuestItemState questItemState;
        public UIVictoryState victoryState;
        public UIGameOverState gameOverState;
        public UIPauseState pauseState;
        public UIUnpausedState unpausedState;

        public VisualElement mainMenuContainer;
        public VisualElement playerInfoContainer;

        public bool canPause = true;
        public int ActiveBtnIdx { get; private set; } = 0;
        public PlayerInput PlayerInputCmp { get; private set; }

        private UIDocument uiDocumentCmp;
        private AudioSource audioSourceCmp;

        private void Awake()
        {
            audioSourceCmp = GetComponent<AudioSource>();
            uiDocumentCmp = GetComponent<UIDocument>();
            root = uiDocumentCmp.rootVisualElement;

            PlayerInputCmp = GameObject
                .FindGameObjectWithTag(Consts.GAME_MANAGER_TAG)
                .GetComponent<PlayerInput>();

            // ? Q() queries the first element
            mainMenuContainer = root.Q<VisualElement>(UIConsts.MAIN_MENU_NAME);
            playerInfoContainer = root.Q<VisualElement>(UIConsts.PLAYER_INFO_NAME);
            healthLabel = playerInfoContainer.Q<Label>(UIConsts.HEALTH_LABEL_NAME);
            potionsLabel = playerInfoContainer.Q<Label>(UIConsts.POTIONS_LABEL_NAME);
            questItemIcon = playerInfoContainer.Q<VisualElement>(UIConsts.QUEST_ITEM_ICON_NAME);

            mainMenuState = new(this);
            dialogueState = new(this);
            questItemState = new(this);
            victoryState = new(this);
            gameOverState = new(this);
            pauseState = new(this);
            unpausedState = new(this);
        }

        private void OnEnable()
        {
            // ? Registering event listeners
            EventManager.OnChangePlayerHealth += HandleChangePlayerHealth;
            EventManager.OnChangePlayerPotions += HandleChangePlayerPotions;
            EventManager.OnOpenDialogue += HandleOpenDialogue;
            EventManager.OnTreasureChestOpen += HandleTreasureChestOpen;
            EventManager.OnVictory += HandleVictory;
            EventManager.OnGameOver += HandleGameOver;
        }

        private void OnDisable()
        {
            EventManager.OnChangePlayerHealth -= HandleChangePlayerHealth;
            EventManager.OnChangePlayerPotions -= HandleChangePlayerPotions;
            EventManager.OnOpenDialogue -= HandleOpenDialogue;
            EventManager.OnTreasureChestOpen -= HandleTreasureChestOpen;
            EventManager.OnVictory -= HandleVictory;
            EventManager.OnGameOver -= HandleGameOver;
        }

        private void Start()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (sceneIndex == Consts.MAIN_MENU_SCENE_IDX)
            {
                SwitchState(mainMenuState);
            }
            else
            {
                playerInfoContainer.style.display = DisplayStyle.Flex;
            }
        }

        public void SwitchState(IUIState newState)
        {
            currentState = newState;
            currentState.EnterState();
        }

        public void SetActiveButton(int newIndex)
        {
            if (buttons.Count == 0)
                return;

            if (newIndex != ActiveBtnIdx)
                buttons[ActiveBtnIdx]?.RemoveFromClassList(UIConsts.ACTIVE_CLASS);

            ActiveBtnIdx = newIndex;

            buttons[ActiveBtnIdx].AddToClassList(UIConsts.ACTIVE_CLASS);
        }

        public void PlayAudio(UIAudioClip audio, bool asOneShot = true)
        {
            AudioClip clip = audio switch
            {
                UIAudioClip.Victory => victoryAudio,
                UIAudioClip.GameOver => gameOverAudio,
                _ => throw new ArgumentOutOfRangeException(nameof(audio)),
            };

            if (asOneShot)
            {
                // NOTE PlayOneShot() doesn't cancel audio clips that are already playing.
                audioSourceCmp.PlayOneShot(clip);
            }
            else
            {
                audioSourceCmp.clip = clip;
                audioSourceCmp.Play();
            }
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            currentState.SelectButton();
        }

        public void HandleNavigate(InputAction.CallbackContext context)
        {
            if (!context.performed || buttons.Count == 0)
                return;

            var input = context.ReadValue<Vector2>();
            var newIndex = Utils.ToIndex(ActiveBtnIdx + (int)input.x, buttons.Count);
            SetActiveButton(newIndex);
        }

        public void HandlePause(InputAction.CallbackContext context)
        {
            if (!context.performed || !canPause)
                return;

            SwitchState(currentState == pauseState ? unpausedState : pauseState);
        }

        // NOTE the event handler must have the same method signature as the event itself
        private void HandleChangePlayerHealth(float newHealth)
        {
            healthLabel.text = newHealth.ToString();
        }

        private void HandleChangePlayerPotions(int newPotions)
        {
            potionsLabel.text = newPotions.ToString();
        }

        private void HandleOpenDialogue(TextAsset inkJson, GameObject npc)
        {
            SwitchState(dialogueState);
            // ? Equivalent to `(currentState as UIDialogueState).SetStory(inkJson);` since they point to the same data
            dialogueState.SetStory(inkJson, npc);
        }

        private void HandleTreasureChestOpen(QuestItemSO item, bool showUi)
        {
            questItemIcon.style.display = DisplayStyle.Flex;

            if (!showUi)
                return;

            SwitchState(questItemState);
            questItemState.SetQuestItemLabel(item.itemName);
        }

        private void HandleVictory() => SwitchState(victoryState);

        private void HandleGameOver() => SwitchState(gameOverState);
    }

    public enum UIAudioClip
    {
        Victory,
        GameOver
    }
}
