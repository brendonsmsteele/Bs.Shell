namespace Bs.Shell.Navigation
{
    public class NavigationMapDBV : DataBoundViews<NavigationMapItem.Model>
    {
        public event DelegateMessage OnMessage;

        protected override View<NavigationMapItem.Model> AddView(NavigationMapItem.Model model)
        {
            var super = (NavigationMapItem)base.AddView(model);
            super.OnMessage += View_OnMessage;
            return super;
        }

        protected override void RemoveView(NavigationMapItem.Model viewModel, View<NavigationMapItem.Model> view)
        {
            var super = (NavigationMapItem)base.AddView(viewModel);
            super.OnMessage -= View_OnMessage;
            base.RemoveView(viewModel, view);
        }

        private void View_OnMessage(string message)
        {
            OnMessage?.Invoke(message);
        }
    }
}