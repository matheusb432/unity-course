using RPG.Util;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public sealed class UIUnpausedState : IUIState
    {
        private readonly UIController controller;

        public UIUnpausedState(UIController ui)
        {
            controller = ui;
        }

        public void EnterState()
        {
            VisualElement pauseContainer = controller.root.Q<VisualElement>(UIConsts.PAUSE_NAME);

            controller.PlayerInputCmp.SwitchCurrentActionMap(Consts.GAMEPLAY_ACTION_MAP);
            pauseContainer.style.display = DisplayStyle.None;

            Time.timeScale = 1;
        }

        public void SelectButton() { }
    }
}
