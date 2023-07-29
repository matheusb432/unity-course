using UnityEngine;
using RPG.Util;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Ink.Runtime;

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

        public override void SelectButton() { }

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
            dialogueText.text = currentStory.Continue();
        }
    }
}
