using Ink.Runtime;
using RPG.Character;
using RPG.Util;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public sealed class UIDialogueState : IUIState
    {
        private readonly UIController controller;
        private VisualElement dialogueContainer;
        private Label dialogueText;
        private VisualElement nextButton;
        private VisualElement choicesGroup;

        private Story currentStory;

        private NpcController npcController;

        private bool hasChoices = false;

        public UIDialogueState(UIController ui)
        {
            controller = ui;
        }

        public void EnterState()
        {
            dialogueContainer = controller.root.Q("dialogue");
            dialogueText = controller.root.Q<Label>("dialogue-text");
            nextButton = controller.root.Q("dialogue-next-button");
            choicesGroup = controller.root.Q("choices-group");

            dialogueContainer.style.display = DisplayStyle.Flex;
            controller.PlayerInputCmp.SwitchCurrentActionMap(Consts.UI_ACTION_MAP);

            controller.canPause = false;
        }

        public void SelectButton()
        {
            UpdateDialogue();
        }

        /// <summary>
        /// This must be called <i>after</i> EnterState
        /// </summary>
        public void SetStory(TextAsset inkJson, GameObject npc)
        {
            currentStory = new Story(inkJson.text);
            currentStory.BindExternalFunction("VerifyQuest", VerifyQuest);

            npcController = npc.GetComponent<NpcController>();

            if (npcController.AlreadyCompletedQuest)
                currentStory.ChoosePathString("postCompletion");

            UpdateDialogue();
        }

        private void UpdateDialogue()
        {
            if (hasChoices)
            {
                currentStory.ChooseChoiceIndex(controller.ActiveBtnIdx);
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

            controller.SetActiveButton(0);
        }

        private void CreateNewChoiceButton(Choice choice)
        {
            Button choiceButton = new();

            choiceButton.AddToClassList(UIConsts.MENU_BUTTON_CLASS);
            choiceButton.text = choice.text;
            choiceButton.style.marginRight = 20;
            choicesGroup.Add(choiceButton);
        }

        private void ExitDialogue()
        {
            dialogueContainer.style.display = DisplayStyle.None;
            controller.PlayerInputCmp.SwitchCurrentActionMap(Consts.GAMEPLAY_ACTION_MAP);

            controller.canPause = true;
        }

        public void VerifyQuest()
        {
            var foundItem = npcController.CheckPlayerForQuestItem();
            currentStory.variablesState["questCompleted"] = foundItem;
        }
    }
}
