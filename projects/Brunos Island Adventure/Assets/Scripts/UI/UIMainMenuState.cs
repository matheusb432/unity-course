﻿using UnityEngine.UIElements;
using UnityEngine;
using RPG.Core;
using RPG.Util;

namespace RPG.UI
{
    public class UIMainMenuState : UIBaseState
    {
        public UIMainMenuState(UIController controller) : base(controller) { }

        public override void EnterState()
        {
            // NOTE Querying UI elements with `.menu-button` class
            controller.buttons = controller.root.Query<Button>(null, "menu-button").ToList();

            controller.buttons[0].AddToClassList("active");
        }

        public override void SelectButton()
        {
            var btn = controller.buttons[controller.currentSelection];

            if (btn.name == "start-btn")
                SceneTransition.Initiate(Constants.ISLAND_SCENE_IDX);
        }
    }
}
