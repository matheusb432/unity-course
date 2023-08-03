using RPG.Util;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIPauseState : IUIState
    {
        private readonly UIController controller;

        public UIPauseState(UIController ui)
        {
            controller = ui;
        }

        public void EnterState()
        {
            VisualElement pauseContainer = controller.root.Q<VisualElement>(UIConsts.PAUSE_NAME);

            controller.PlayerInputCmp.SwitchCurrentActionMap(Consts.UI_ACTION_MAP);
            pauseContainer.style.display = DisplayStyle.Flex;

            // NOTE Pausing the game by freezing time
            Time.timeScale = 0;
        }

        public void SelectButton()
        {
            controller.SwitchState(controller.unpausedState);
        }
    }
}
