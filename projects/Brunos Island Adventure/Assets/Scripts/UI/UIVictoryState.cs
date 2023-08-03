using RPG.Core;
using RPG.Util;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIVictoryState : IUIState
    {
        private readonly UIController controller;

        public UIVictoryState(UIController ui)
        {
            controller = ui;
        }

        public void EnterState()
        {
            VisualElement victoryContainer = controller.root.Q<VisualElement>(
                UIConsts.VICTORY_NAME
            );

            controller.PlayerInputCmp.SwitchCurrentActionMap(Consts.UI_ACTION_MAP);
            victoryContainer.style.display = DisplayStyle.Flex;

            controller.PlayAudio(UIAudioClip.Victory, asOneShot: false);

            controller.canPause = false;
        }

        public void SelectButton()
        {
            PlayerPrefs.DeleteAll();
            controller.StartCoroutine(SceneTransition.Initiate(Consts.MAIN_MENU_SCENE_IDX));
        }
    }
}
