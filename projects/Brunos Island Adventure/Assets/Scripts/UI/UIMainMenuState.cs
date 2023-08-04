using RPG.Core;
using RPG.Util;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public sealed class UIMainMenuState : IUIState
    {
        private int sceneIndex;

        private readonly UIController controller;

        public UIMainMenuState(UIController ui)
        {
            controller = ui;
        }

        public void EnterState()
        {
            if (PlayerPrefs.HasKey(SaveConsts.SCENE_INDEX))
            {
                sceneIndex = PlayerPrefs.GetInt(SaveConsts.SCENE_INDEX);
                AddContinueButton();
            }

            controller.mainMenuContainer.style.display = DisplayStyle.Flex;

            // NOTE Querying UI elements with `.menu-button` class
            controller.buttons = controller.mainMenuContainer
                .Query<Button>(null, UIConsts.MENU_BUTTON_CLASS)
                .ToList();
            controller.SetActiveButton(0);
        }

        public void SelectButton()
        {
            var btn = controller.buttons[controller.ActiveBtnIdx];

            if (btn.name == "start-btn")
            {
                // ? Deletes saved data
                PlayerPrefs.DeleteAll();
                // NOTE The coroutine will finish once the IEnumerator ends
                controller.StartCoroutine(SceneTransition.Initiate(Consts.ISLAND_SCENE_IDX));
            }
            else
            {
                controller.StartCoroutine(SceneTransition.Initiate(sceneIndex));
            }
        }

        private void AddContinueButton()
        {
            Button continueBtn = new();
            continueBtn.AddToClassList(UIConsts.MENU_BUTTON_CLASS);
            continueBtn.text = "Continue";

            var mainMenuButtons = controller.mainMenuContainer.Q<VisualElement>("buttons");

            mainMenuButtons.Add(continueBtn);
        }
    }
}
