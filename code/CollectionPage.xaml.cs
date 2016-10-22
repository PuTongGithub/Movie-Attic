using MovieCollection.data;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MovieCollection
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CollectionPage : Page
    {
        private ObservableCollection<string> SortItems;
        private List<CollectedMovie> mainMovieList;
        private ObservableCollection<CollectedMovie> showMovieList;
        private CollectedMovie movie;
        public CollectionPage()
        {
            this.InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Enabled;

            SortItems = new ObservableCollection<string>();
            showMovieList = new ObservableCollection<CollectedMovie>();

            mainMovieList = SQLiteOperation.QueryDataByType(SQLiteOperation.OrderType.CollectionDate);
            mainMovieList.Reverse();
            foreach (var item in mainMovieList)
            {
                showMovieList.Add(item);
            }

            if (showMovieList.Count() == 0)
            {
                NoCollectionHit.Visibility = Visibility.Visible;
                GoSearchHit.Visibility = Visibility.Visible; 
            }
            else
            {
                ComboBoxesGrid.Visibility = Visibility.Visible;
                MoviesListView.Visibility = Visibility.Visible;
            }
        }

        private void ClassComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemClass = (ComboBoxItem)SortClassComboBox.SelectedItem;
            switch (itemClass.Content.ToString())
            {
                case "Title":
                    SortItems.Clear();
                    showMovieList.Clear();
                    SortItemComboBox.Visibility = Visibility.Collapsed;
                    mainMovieList = SQLiteOperation.QueryDataByType(SQLiteOperation.OrderType.Title);
                    foreach(var item in mainMovieList)
                    {
                        showMovieList.Add(item);
                    }
                    break;

                case "Rating":
                    SortItems.Clear();
                    showMovieList.Clear();
                    SortItemComboBox.Visibility = Visibility.Collapsed;
                    mainMovieList = SQLiteOperation.QueryDataByType(SQLiteOperation.OrderType.RatingSum);
                    mainMovieList.Reverse();
                    foreach (var item in mainMovieList)
                    {
                        showMovieList.Add(item);
                    }
                    break;

                case "Year":
                    SortItems.Clear();
                    showMovieList.Clear();
                    SortItemComboBox.Visibility = Visibility.Visible;
                    mainMovieList = SQLiteOperation.QueryDataByType(SQLiteOperation.OrderType.Year);
                    foreach (var item in mainMovieList)
                    {
                        showMovieList.Add(item);
                    }

                    var years = mainMovieList.GroupBy(v => v.Year).Select(g => g.First()).ToList();
                    years.OrderBy(v => v.Year);
                    foreach(var item in years)
                    {
                        SortItems.Add(item.Year);
                    }
                    break;

                case "Director":
                    SortItems.Clear();
                    showMovieList.Clear();
                    SortItemComboBox.Visibility = Visibility.Visible;
                    mainMovieList = SQLiteOperation.QueryDataByType(SQLiteOperation.OrderType.Director);
                    foreach (var item in mainMovieList)
                    {
                        showMovieList.Add(item);
                    }

                    var directors = mainMovieList.GroupBy(v => v.Director).Select(g => g.First()).ToList();
                    directors.OrderBy(v => v.Director);
                    foreach(var item in directors)
                    {
                        SortItems.Add(item.Director);
                    }
                    break;

                case "Tag":
                    SortItems.Clear();
                    showMovieList.Clear();
                    SortItemComboBox.Visibility = Visibility.Visible;
                    var tags = CollectedMovieTagList.TagList;
                    foreach(var t in tags)
                    {
                        SortItems.Add(t);
                    }
                    mainMovieList = SQLiteOperation.QueryDataAll();
                    foreach (var item in mainMovieList)
                    {
                        showMovieList.Add(item);
                    }
                    break;

                default:break;
            }
        }

        private void SortItemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemClass = (ComboBoxItem)SortClassComboBox.SelectedItem;
            var item = (string)SortItemComboBox.SelectedItem;
            List<CollectedMovie> movieList = new List<CollectedMovie>();

            if (itemClass.Content.ToString() == "Year")
            {
                movieList = mainMovieList.Where(v => v.Year == item).ToList();
            }
            if (itemClass.Content.ToString() == "Director")
            {
                movieList = mainMovieList.Where(v => v.Director == item).ToList();                
            }
            if (itemClass.Content.ToString() == "Tag")
            {
                movieList = mainMovieList.Where(v => v.Tags.Contains(";" + item + ";")).ToList();
            }

            showMovieList.Clear();
            foreach(var i in movieList)
            {
                showMovieList.Add(i);
            }
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {            
            movie = (CollectedMovie)e.ClickedItem;
            PosterImage.Source= new BitmapImage(new Uri(movie.Poster, UriKind.Absolute));

            TitleTextBlock.Text = movie.Title;
            ReleasedTextBlock.Text = movie.Released;
            RatingSumTextBlock.Text = movie.RatingSum;
            GenreTextBlock.Text = movie.Genre;
            DirectorTextBlock.Text = movie.Director;
            WriterTextBlock.Text = movie.Writer;
            ActorsTextBlock.Text = movie.Actors;
            imdbRatingTextBlock.Text = movie.imdbRating;
            CollectionDateTextBlock.Text = movie.CollectionDate;
            FilmCriticTextBlock.Text = movie.FilmCritic;

            MovieInformationGrid.Visibility = Visibility.Visible;
        }

        private void MoreInformationButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MoreInformationPage), movie);
        }

        private void DeleteYes_Click(object sender, RoutedEventArgs e)
        {
            SQLiteOperation.DeleteData(movie);
            showMovieList.Remove(movie);
            mainMovieList.Remove(movie);
            MovieInformationGrid.Visibility = Visibility.Collapsed;
            DeleteButtonFlyout.Hide();
        }

        private void DeleteNo_Click(object sender, RoutedEventArgs e)
        {
            DeleteButtonFlyout.Hide();
        }
        
    }
}
