using RPG.Core;
using RPG.Util;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIQuestItemState : IUIState
    {
        private VisualElement questItemContainer;
        private Label questItemText;

        private readonly UIController controller;

        public UIQuestItemState(UIController ui)
        {
            controller = ui;
        }

        public void EnterState()
        {
            controller.PlayerInputCmp.SwitchCurrentActionMap(Consts.UI_ACTION_MAP);

            questItemContainer = controller.root.Q<VisualElement>(UIConsts.QUEST_ITEM_NAME);
            questItemText = questItemContainer.Q<Label>(UIConsts.QUEST_ITEM_LABEL_NAME);

            questItemContainer.style.display = DisplayStyle.Flex;

            GameManager.IsUiOpen = true;

            controller.canPause = false;
        }

        public void SelectButton()
        {
            questItemContainer.style.display = DisplayStyle.None;

            controller.PlayerInputCmp.SwitchCurrentActionMap(Consts.GAMEPLAY_ACTION_MAP);
            GameManager.IsUiOpen = false;

            controller.canPause = true;
        }

        // TODO refactor - should be necessary to init the state, similar to UIDialogueState's issue
        public void SetQuestItemLabel(string name)
        {
            questItemText.text = name;
        }
    }
}
