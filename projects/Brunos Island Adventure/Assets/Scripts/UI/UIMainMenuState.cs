using UnityEngine.UIElements;
using UnityEngine;

namespace RPG.UI
{
    public class UIMainMenuState : UIBaseState
    {
        public UIMainMenuState(UIController controller) : base(controller) { }

        public override void EnterState()
        {
            // NOTE Querying UI elements with `.menu-button` class
            controller.buttons = controller.root.Query<Button>(null, "menu-button").ToList();

            Debug.Log(controller.buttons.Count);
        }

        public override void SelectButton()
        {
            Debug.Log("button selected!");
        }
    }
}
