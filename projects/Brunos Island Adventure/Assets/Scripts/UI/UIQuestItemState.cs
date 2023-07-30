using RPG.Core;
using RPG.Util;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIQuestItemState : UIBaseState
    {
        private VisualElement questItemContainer;
        private Label questItemText;
        private PlayerInput playerInputCmp;

        public UIQuestItemState(UIController ui) : base(ui) { }

        public override void EnterState()
        {
            playerInputCmp = GameObject
                .FindGameObjectWithTag(Constants.GAME_MANAGER_TAG)
                .GetComponent<PlayerInput>();

            playerInputCmp.SwitchCurrentActionMap(Constants.UI_ACTION_MAP);

            questItemContainer = controller.root.Q<VisualElement>(UIConstants.QUEST_ITEM_NAME);
            questItemText = questItemContainer.Q<Label>(UIConstants.QUEST_ITEM_LABEL_NAME);

            questItemContainer.style.display = DisplayStyle.Flex;

            EventManager.RaiseToggleUI(true);
        }

        public override void SelectButton()
        {
            questItemContainer.style.display = DisplayStyle.None;

            // TODO refactor ? extract switch action map logic in toggle UI event handler?
            // ! If the action map is not switched this will effectively freeze the game
            playerInputCmp.SwitchCurrentActionMap(Constants.GAMEPLAY_ACTION_MAP);
            EventManager.RaiseToggleUI(false);
        }

        // TODO refactor - should be necessary to init the state, similar to UIDialogueState's issue
        public void SetQuestItemLabel(string name)
        {
            questItemText.text = name;
        }
    }
}
