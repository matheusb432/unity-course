namespace RPG.UI
{
    public abstract class UIBaseState
    {
        public UIController controller;

        // TODO refactor ? this seems to create a leaky abstraction and this entire abstract class should instead be an interface.
        public UIBaseState(UIController controller)
        {
            this.controller = controller;
        }

        public abstract void EnterState();

        public abstract void SelectButton();
    }
}
