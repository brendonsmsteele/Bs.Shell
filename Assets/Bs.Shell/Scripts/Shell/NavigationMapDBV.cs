namespace Bs.Shell.Navigation
{
    public class NavigationMapDBV : DataBoundViews<NavigationMapItemViewModel>
    {
        public event DelegateMessage OnMessage;

        protected override View<NavigationMapItemViewModel> AddView(NavigationMapItemViewModel viewModel)
        {
            var super = (NavigationMapItem)base.AddView(viewModel);
            super.OnMessage += View_OnMessage;
            return super;
        }

        protected override void RemoveView(NavigationMapItemViewModel viewModel, View<NavigationMapItemViewModel> view)
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