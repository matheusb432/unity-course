using Ink.Runtime;
using RPG.Util;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIDialogueState : UIBaseState
    {
        private VisualElement dialogueContainer;
        private Label dialogueText;
        private VisualElement nextButton;
        private VisualElement choicesGroup;

        // ! This should be initialized in the constructor.
        private Story currentStory;

        private PlayerInput playerInputCmp;

        private bool hasChoices = false;

        public UIDialogueState(UIController ui) : base(ui) { }

        // TODO refactor, the method should accept the necessary arguments to initialize this, like currentStory with the TextAsset
        public override void EnterState()
        {
            dialogueContainer = controller.root.Q("dialogue");
            dialogueText = controller.root.Q<Label>("dialogue-text");
            nextButton = controller.root.Q("dialogue-next-button");
            choicesGroup = controller.root.Q("choices-group");

            dialogueContainer.style.display = DisplayStyle.Flex;
            playerInputCmp = GameObject
                .FindGameObjectWithTag(Constants.GAME_MANAGER_TAG)
                .GetComponent<PlayerInput>();
            playerInputCmp.SwitchCurrentActionMap(Constants.UI_ACTION_MAP);
        }

        public override void SelectButton()
        {
            UpdateDialogue();
        }

        /// <summary>
        /// This must be called <i>after</i> EnterState
        /// </summary>
        public void SetStory(TextAsset inkJson)
        {
            currentStory = new Story(inkJson.text);
            UpdateDialogue();
        }

        private void UpdateDialogue()
        {
            if (hasChoices)
            {
                currentStory.ChooseChoiceIndex(controller.currentSelection);
            }

            if (!currentStory.canContinue)
            {
                ExitDialogue();
                return;
            }

            dialogueText.text = currentStory.Continue();

            hasChoices = currentStory.currentChoices.Count > 0;

            if (hasChoices)
            {
                HandleNewChoices(currentStory.currentChoices);
                Debug.Log("Has choices");
            }
            else
            {
                nextButton.style.display = DisplayStyle.Flex;
                choicesGroup.style.display = DisplayStyle.None;
            }
        }

        private void HandleNewChoices(List<Choice> choices)
        {
            nextButton.style.display = DisplayStyle.None;
            choicesGroup.style.display = DisplayStyle.Flex;

            choicesGroup.Clear();
            controller.buttons?.Clear();

            choices.ForEach(CreateNewChoiceButton);

            controller.buttons = choicesGroup.Query<Button>().ToList();
            // ! instead of index 0 this should just activate via currentSelection, after it has been reset
            // ! maybe via currentSelection setter logic.
            controller.buttons[0].AddToClassList("active");

            // TODO refactor - controller.buttons should have a dedicated setter since the currentSelection must always be reset
            controller.currentSelection = 0;
        }

        private void CreateNewChoiceButton(Choice choice)
        {
            Debug.Log(choice.text);
            Button choiceButton = new();

            // TODO refactor - class strings to constants
            choiceButton.AddToClassList("menu-button");
            choiceButton.text = choice.text;
            choiceButton.style.marginRight = 20;
            choicesGroup.Add(choiceButton);
        }

        private void ExitDialogue()
        {
            dialogueContainer.style.display = DisplayStyle.None;
            playerInputCmp.SwitchCurrentActionMap(Constants.GAMEPLAY_ACTION_MAP);
        }
    }
}
