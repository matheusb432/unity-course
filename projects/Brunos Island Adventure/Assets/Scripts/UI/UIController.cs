using System.Collections;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.UI
{
    public class UIController : MonoBehaviour
    {
        private UIDocument uiDocumentCmp;
        public VisualElement root;
        public List<Button> buttons;

        public UIBaseState currentState;
        public UIMainMenuState mainMenuState;

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
    }
}
