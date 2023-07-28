using RPG.Core;
using RPG.Util;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIController : MonoBehaviour
    {
        private UIDocument uiDocumentCmp;
        public VisualElement root;
        public List<Button> buttons;
        public Label healthLabel;
        public Label potionsLabel;

        public UIBaseState currentState;
        public UIMainMenuState mainMenuState;
        public UIDialogueState dialogueState;

        public VisualElement mainMenuContainer;
        public VisualElement playerInfoContainer;

        public int currentSelection = 0;

        private void Awake()
        {
            uiDocumentCmp = GetComponent<UIDocument>();
            root = uiDocumentCmp.rootVisualElement;

            // ? Q() queries the first element
            mainMenuContainer = root.Q<VisualElement>(Constants.MAIN_MENU_NAME);
            playerInfoContainer = root.Q<VisualElement>(Constants.PLAYER_INFO_NAME);
            healthLabel = playerInfoContainer.Q<Label>(Constants.HEALTH_LABEL_NAME);
            potionsLabel = playerInfoContainer.Q<Label>(Constants.POTIONS_LABEL_NAME);
            mainMenuState = new(this);
            dialogueState = new(this);
        }

        private void OnEnable()
        {
            // ? Registering an event listener
            EventManager.OnChangePlayerHealth += HandleChangePlayerHealth;
            EventManager.OnChangePlayerPotions += HandleChangePlayerPotions;
            EventManager.OnOpenDialogue += HandleOpenDialogue;
        }

        private void OnDisable()
        {
            EventManager.OnChangePlayerHealth -= HandleChangePlayerHealth;
            EventManager.OnChangePlayerPotions -= HandleChangePlayerPotions;
            EventManager.OnOpenDialogue -= HandleOpenDialogue;
        }

        private void Start()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (sceneIndex == Constants.MAIN_MENU_SCENE_IDX)
            {
                currentState = mainMenuState;
                currentState.EnterState();
            }
            else
            {
                playerInfoContainer.style.display = DisplayStyle.Flex;
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

            // TODO refactor
            buttons[currentSelection].RemoveFromClassList("active");

            var input = context.ReadValue<Vector2>();
            currentSelection = Utils.ToIndex(currentSelection + (int)input.x, buttons.Count);

            buttons[currentSelection].AddToClassList("active");
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

        private void HandleOpenDialogue(TextAsset inkJson)
        {
            currentState = dialogueState;
            currentState.EnterState();
        }
    }
}
