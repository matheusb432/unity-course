using RPG.Util;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine;
using RPG.Core;

namespace RPG.UI
{
    public class UIGameOverState : UIBaseState
    {
        public UIGameOverState(UIController uiController) : base(uiController) { }

        public override void EnterState()
        {
            PlayerInput playerInputCmp = GameObject
                .FindGameObjectWithTag(Constants.GAME_MANAGER_TAG)
                .GetComponent<PlayerInput>();

            VisualElement gameOverContainer = controller.root.Q<VisualElement>(
                UIConstants.GAME_OVER_NAME
            );

            playerInputCmp.SwitchCurrentActionMap(Constants.UI_ACTION_MAP);
            gameOverContainer.style.display = DisplayStyle.Flex;

            // NOTE PlayOneShot() doesn't cancel audio clips that are already playing.
            controller.audioSourceCmp.PlayOneShot(controller.gameOverAudio);
        }

        public override void SelectButton()
        {
            PlayerPrefs.DeleteAll();
            controller.StartCoroutine(SceneTransition.Initiate(Constants.MAIN_MENU_SCENE_IDX));
        }
    }
}
