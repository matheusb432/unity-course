using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using RPG.Util;
using RPG.Core;
using UnityEngine.InputSystem.XR;

namespace RPG.UI
{
    public class UIVictoryState : UIBaseState
    {
        public UIVictoryState(UIController uiController) : base(uiController) { }

        public override void EnterState()
        {
            PlayerInput playerInputCmp = GameObject
                .FindGameObjectWithTag(Constants.GAME_MANAGER_TAG)
                .GetComponent<PlayerInput>();
            VisualElement victoryContainer = controller.root.Q<VisualElement>(
                UIConstants.VICTORY_NAME
            );

            playerInputCmp.SwitchCurrentActionMap(Constants.UI_ACTION_MAP);
            victoryContainer.style.display = DisplayStyle.Flex;

            // TODO refactor - maybe encapsulate the audio source and instead expose a `PlayAudio` method on the controller?
            controller.audioSourceCmp.clip = controller.victoryAudio;
            controller.audioSourceCmp.Play();
        }

        public override void SelectButton()
        {
            PlayerPrefs.DeleteAll();
            controller.StartCoroutine(SceneTransition.Initiate(Constants.MAIN_MENU_SCENE_IDX));
        }
    }
}
