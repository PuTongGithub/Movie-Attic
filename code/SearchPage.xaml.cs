using MovieCollection.data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MovieCollection
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        private ObservableCollection<Search> searchList;
        private double originHeight;
        private bool isLoding;
        private int countPage;
        public SearchPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            searchList = new ObservableCollection<Search>();
            originHeight = 0;
            isLoding = false;
            countPage = 0;
        }

        private async void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            searchList.Clear();
            originHeight = 0;
            countPage = 0;
            MoreLettersWorning.Visibility = Visibility.Collapsed;
            NotFindWorning.Visibility = Visibility.Collapsed;
            SearchResultList.Visibility = Visibility.Collapsed;

            if (SearchBox.Text.Length < 2)
            {
                MoreLettersWorning.Visibility = Visibility.Visible;
            }
            else
            {
                SearchingProgressRing.Visibility = Visibility.Visible;
                SearchingProgressRing.IsActive = true;
                try
                {
                    Task t = GetSearchList.GetData(searchList, SearchBox.Text);
                    await t;
                    countPage++;
                    t = GetSearchList.GetData(searchList, SearchBox.Text, countPage + 1);
                    await t;
                    countPage++;
                }
                catch (Exception) { }
                finally
                {
                    SearchingProgressRing.Visibility = Visibility.Collapsed;
                    SearchingProgressRing.IsActive = false;
                }

                if (searchList.Count() == 0)
                {
                    NotFindWorning.Visibility = Visibility.Visible;
                }
                else
                {
                    SearchResultList.Visibility = Visibility.Visible;
                }
            }            
        }

        private void SearchResultList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedMovie = (Search)e.ClickedItem;
            Frame.Navigate(typeof(DetailedInfomationPage),clickedMovie.imdbID);
        }

        private async void SearchResultScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (searchList.Count() % 10 != 0) return;
            if (SearchResultScrollViewer.VerticalOffset == originHeight) return;
            originHeight = SearchResultScrollViewer.VerticalOffset;

            if (isLoding) return;
            if (SearchResultScrollViewer.VerticalOffset <= SearchResultScrollViewer.ScrollableHeight - 500) return;
            //if(_currentPage >= countPage + 1) return;

            isLoding = true;
            try
            {
                Task t = GetSearchList.GetData(searchList, SearchBox.Text, countPage + 1);
                await t;
                countPage++;
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                isLoding = false;
            }
            
        }
    }
}
