using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIDialogueState : UIBaseState
    {
        private VisualElement dialogueContainer;
        private Label dialogueText;
        private VisualElement nextButton;
        private VisualElement choicesGroup;

        public UIDialogueState(UIController ui) : base(ui) { }

        public override void EnterState()
        {
            dialogueContainer = controller.root.Q("dialogue");
            dialogueText = controller.root.Q<Label>("dialogue-text");
            nextButton = controller.root.Q("dialogue-next-button");
            choicesGroup = controller.root.Q("choices-group");

            dialogueContainer.style.display = DisplayStyle.Flex;
        }

        public override void SelectButton() { }
    }
}
