using RPG.Core;
using RPG.Util;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIMainMenuState : UIBaseState
    {
        private int sceneIndex;

        public UIMainMenuState(UIController controller) : base(controller) { }

        public override void EnterState()
        {
            if (PlayerPrefs.HasKey(SaveConstants.SCENE_INDEX))
            {
                sceneIndex = PlayerPrefs.GetInt(SaveConstants.SCENE_INDEX);
                AddContinueButton();
            }

            controller.mainMenuContainer.style.display = DisplayStyle.Flex;

            // NOTE Querying UI elements with `.menu-button` class
            controller.buttons = controller.mainMenuContainer
                .Query<Button>(null, "menu-button")
                .ToList();

            controller.buttons[0].AddToClassList("active");
        }

        public override void SelectButton()
        {
            var btn = controller.buttons[controller.currentSelection];

            if (btn.name == "start-btn")
            {
                // ? Deletes saved data
                PlayerPrefs.DeleteAll();
                SceneTransition.Initiate(Constants.ISLAND_SCENE_IDX);
            }
            else
            {
                SceneTransition.Initiate(sceneIndex);
            }
        }

        private void AddContinueButton()
        {
            Button continueBtn = new();
            continueBtn.AddToClassList("menu-button");
            continueBtn.text = "Continue";

            var mainMenuButtons = controller.mainMenuContainer.Q<VisualElement>("buttons");

            mainMenuButtons.Add(continueBtn);
        }
    }
}
