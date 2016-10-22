using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MovieCollection.data;
using Windows.UI;
using Windows.System;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace MovieCollection
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>    
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            CollectedMovieTagList.GetTagList();
            SQLiteOperation.GetDbConnection();
            MainPageFrame.Navigate(typeof(HomePage));
            HomeItem.IsSelected = true;
            HumburgerMainList.SelectedIndex = 0;
            
            var coreTitleBar = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().TitleBar;
            var appTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            appTitleBar.ButtonBackgroundColor = Colors.Transparent;
            appTitleBar.ButtonForegroundColor = Colors.White;
            Window.Current.SetTitleBar(TitleText);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void HumburgerMainList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HomeItem.IsSelected)
            {
                MainPageFrame.Navigate(typeof(HomePage));
            }
            if (SearchItem.IsSelected)
            {
                ResetPageCache();
                MainPageFrame.Navigate(typeof(SearchPage));
            }
            if (CollectionItem.IsSelected)
            {
                ResetPageCache();
                MainPageFrame.Navigate(typeof(CollectionPage));
            }
        }

        private void ResetPageCache()
        {
            var cacheSize = MainPageFrame.CacheSize;
            MainPageFrame.CacheSize = 0;
            MainPageFrame.CacheSize = cacheSize;
        }

        private void ListBoxItem_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var item = (ListBoxItem)sender;
            item.Background = new SolidColorBrush(Colors.Black);
        }

        private void ListBoxItem_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            var item = (ListBoxItem)sender;
            item.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AboutSplitView.IsPaneOpen = !AboutSplitView.IsPaneOpen;
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                Uri targetUri = new Uri("ms-windows-store://review/?ProductId=9nblggh42jk9");
                await Launcher.LaunchUriAsync(targetUri);
            }
            catch { }            
        }
    }
}
