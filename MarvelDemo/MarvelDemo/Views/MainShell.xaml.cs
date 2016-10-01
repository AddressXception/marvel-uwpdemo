using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MarvelDemo.Core.Models;
using MarvelDemo.Core.ViewModels;

namespace MarvelDemo.Views
{
    public sealed partial class MainShell
    {
        Frame RootFrame => Window.Current.Content as Frame;

        public MainShell()
        {
            InitializeComponent();

            DataContext = new MainShellViewModel();

            //HOLOLENS - The HoloLens shell does not have MainSplitView so we need to check for null.
            if (MainSplitView != null)
                PaneGrid.Width = MainSplitView.IsPaneOpen ? MainSplitView.OpenPaneLength : MainSplitView.CompactPaneLength;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
            PaneGrid.Width = MainSplitView.IsPaneOpen ? MainSplitView.OpenPaneLength : MainSplitView.CompactPaneLength;
        }

        private void NavItemsList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = NavItemsList.SelectedItem as Character;

            //HOLOLENS - The HoloLens shell does not have ContentFrame so we need to check for null.
            if (ContentFrame != null)
                ContentFrame.Navigate(typeof(MainView), selectedItem);
            else
            {
                RootFrame.Navigate(typeof(MainView), selectedItem);
            }
        }

        private void InfoButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            //HOLOLENS - The HoloLens shell does not have ContentFrame so we need to check for null.
            if (ContentFrame != null)
                ContentFrame.Navigate(typeof(AboutView));
            else
            {
                RootFrame.Navigate(typeof(AboutView));
            }
        }
    }
}
