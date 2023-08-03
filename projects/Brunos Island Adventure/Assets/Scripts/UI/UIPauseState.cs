using RPG.Util;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine;

namespace RPG.UI
{
    public class UIPauseState : UIBaseState
    {
        public UIPauseState(UIController uiController) : base(uiController) { }

        public override void EnterState()
        {
            PlayerInput playerInputCmp = GameObject
                .FindGameObjectWithTag(Constants.GAME_MANAGER_TAG)
                .GetComponent<PlayerInput>();
            VisualElement pauseContainer = controller.root.Q<VisualElement>(UIConstants.PAUSE_NAME);

            playerInputCmp.SwitchCurrentActionMap(Constants.UI_ACTION_MAP);
            pauseContainer.style.display = DisplayStyle.Flex;

            // NOTE Pausing the game by freezing time
            Time.timeScale = 0;
        }

        public override void SelectButton()
        {
            controller.currentState = controller.unpausedState;
            controller.currentState.EnterState();
        }
    }
}
