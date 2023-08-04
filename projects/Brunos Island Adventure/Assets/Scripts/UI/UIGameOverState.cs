using RPG.Core;
using RPG.Util;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public sealed class UIGameOverState : IUIState
    {
        private readonly UIController controller;

        public UIGameOverState(UIController ui)
        {
            controller = ui;
        }

        public void EnterState()
        {
            VisualElement gameOverContainer = controller.root.Q<VisualElement>(
                UIConsts.GAME_OVER_NAME
            );

            controller.PlayerInputCmp.SwitchCurrentActionMap(Consts.UI_ACTION_MAP);
            gameOverContainer.style.display = DisplayStyle.Flex;

            controller.PlayAudio(UIAudioClip.GameOver);
        }

        public void SelectButton()
        {
            PlayerPrefs.DeleteAll();
            controller.StartCoroutine(SceneTransition.Initiate(Consts.MAIN_MENU_SCENE_IDX));
        }
    }
}
