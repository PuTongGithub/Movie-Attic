﻿using MovieCollection.data;
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
    public sealed partial class HomePage : Page
    {
        private ObservableCollection<HomePageData> DataList;
        public HomePage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            DataList = new ObservableCollection<HomePageData>();
            GetHomePageDataList.SetDataList(DataList);
            GetHomePageDataList.GetDataList(DataList);
        }

        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            var movie = (Movie)(sender as Button).DataContext;
            Frame.Navigate(typeof(DetailedInfomationPage), movie.imdbID);
        }
    }
}
