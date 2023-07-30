﻿using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using RPG.Util;

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
        }

        public override void SelectButton()
        {
            questItemContainer.style.display = DisplayStyle.None;
            playerInputCmp.SwitchCurrentActionMap(Constants.GAMEPLAY_ACTION_MAP);
        }
    }
}
