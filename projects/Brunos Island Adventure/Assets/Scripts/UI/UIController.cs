using System.Collections;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using RPG.Util;

namespace RPG.UI
{
    public class UIController : MonoBehaviour
    {
        private UIDocument uiDocumentCmp;
        public VisualElement root;
        public List<Button> buttons;

        public UIBaseState currentState;
        public UIMainMenuState mainMenuState;

        public int currentSelection = 0;

        private void Awake()
        {
            mainMenuState = new(this);

            uiDocumentCmp = GetComponent<UIDocument>();
            root = uiDocumentCmp.rootVisualElement;
        }

        void Start()
        {
            currentState = mainMenuState;
            currentState.EnterState();
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            currentState.SelectButton();
        }

        public void HandleNavigate(InputAction.CallbackContext context)
        {
            if (!context.performed || buttons.Count == 0)
                return;

            // TODO refactor
            buttons[currentSelection].RemoveFromClassList("active");

            var input = context.ReadValue<Vector2>();
            currentSelection = Utils.ToIndex(currentSelection + (int)input.x, buttons.Count);

            buttons[currentSelection].AddToClassList("active");
        }
    }
}
