﻿using MovieCollection.data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
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
    public sealed partial class DetailedInfomationPage : Page
    {
        private string imdbID;
        private ObservableCollection<ForTagsDataBind> AddTagsList;
        private Movie movie;
        private List<string> SuggestTags;
        public DetailedInfomationPage()
        {
            this.InitializeComponent();
            AddTagsList = new ObservableCollection<ForTagsDataBind>();
            SuggestTags = CollectedMovieTagList.TagList;
        }

        private async void getData()
        {
            try
            {
                LodingRing.IsActive = true;
                LodingRing.Visibility = Visibility.Visible;

                movie = await GetMovie.GetData(imdbID);
                BackgroundImage.Source = new BitmapImage(new Uri(movie.Poster, UriKind.Absolute));
                PosterImage.Source = new BitmapImage(new Uri(movie.Poster, UriKind.Absolute));

                TitleTextBlock.Text = movie.Title;
                ReleasedTextBlock.Text = movie.Released;
                RatedTextBlock.Text = movie.Rated;
                RuntimeTextBlock.Text = movie.Runtime;
                GenreTextBlock.Text = movie.Genre;
                DirectorTextBlock.Text = movie.Director;
                WriterTextBlock.Text = movie.Writer;
                ActorsTextBlock.Text = movie.Actors;
                CountryTextBlock.Text = movie.Country;
                LanguageTextBlock.Text = movie.Language;
                imdbRatingTextBlock.Text = movie.imdbRating;
                imdbVotesTextBlock.Text = movie.imdbVotes;
                AwardsTextBlock.Text = movie.Awards;
                PlotTextBlock.Text = movie.Plot;

                RatingSlider.Value = (int)float.Parse(movie.imdbRating);
                SetTags(movie.Genre);
            }
            catch(Exception)
            {
                return;
            }
            finally
            {
                LodingRing.IsActive = false;
                LodingRing.Visibility = Visibility.Collapsed;
            }
        }

        private void SetTags(string tags)
        {
            StringBuilder builder = new StringBuilder();
            foreach(var ch in tags)
            {
                if (ch == ',')
                {
                    var addTag = new ForTagsDataBind() { Tag = builder.ToString() };
                    AddTagsList.Add(addTag);
                    builder.Clear();
                }
                else if (ch == ' ') { }
                else
                {
                    builder.Append(ch);
                }
            }
            var AddTag = new ForTagsDataBind() { Tag = builder.ToString() };
            AddTagsList.Add(AddTag);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            imdbID = (string)e.Parameter;
            getData();

            var test = SQLiteOperation.QueryData(imdbID);
            if (test != null)
            {
                CollectButton.Visibility = Visibility.Collapsed;
                CollectedButton.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack) Frame.GoBack();
        }

        private void CollectButton_Click(object sender, RoutedEventArgs e)
        {
            AddCollectionSplitView.IsPaneOpen = !AddCollectionSplitView.IsPaneOpen;
        }

        private void AddCollectionCancelButton_Click(object sender, RoutedEventArgs e)
        {
            AddCollectionSplitView.IsPaneOpen = !AddCollectionSplitView.IsPaneOpen;
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var suggestTagList= SuggestTags.Where(p => p.StartsWith(TagsSuggestBox.Text)).ToList();
            TagsSuggestBox.ItemsSource = suggestTagList;
        }

        private void TagsSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            ForTagsDataBind tag = new ForTagsDataBind() { Tag = TagsSuggestBox.Text };
            bool flag = true;
            foreach(var item in AddTagsList)
            {
                if (item == tag)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                AddTagsList.Add(tag);
            }            
            TagsSuggestBox.Text = "";
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var tag = (ForTagsDataBind)e.ClickedItem;
            AddTagsList.Remove(tag);
        }

        private void AddCollectionDoneButton_Click(object sender, RoutedEventArgs e)
        {
            CollectedMovie NewCollectedMovie = new CollectedMovie(movie);
            NewCollectedMovie.RatingSum = ((int)RatingSlider.Value).ToString();
            if(PictureRatingSlider.Value==0&& StoryRatingSlider.Value == 0&&
                MusicRatingSlider.Value == 0&& PerformanceRatingSlider.Value == 0)
            {
                NewCollectedMovie.RatingPicture = ((int)RatingSlider.Value).ToString();
                NewCollectedMovie.RatingStory = ((int)RatingSlider.Value).ToString();
                NewCollectedMovie.RatingMusic = ((int)RatingSlider.Value).ToString();
                NewCollectedMovie.RatingPerformance = ((int)RatingSlider.Value).ToString();
            }
            else
            {
                NewCollectedMovie.RatingPicture = ((int)PictureRatingSlider.Value).ToString();
                NewCollectedMovie.RatingStory = ((int)StoryRatingSlider.Value).ToString();
                NewCollectedMovie.RatingMusic = ((int)MusicRatingSlider.Value).ToString();
                NewCollectedMovie.RatingPerformance = ((int)PerformanceRatingSlider.Value).ToString();
            }

            StringBuilder tags = new StringBuilder(";");
            var TagList = CollectedMovieTagList.TagList;
            var NewTagList = new List<string>();
            foreach (var item in AddTagsList)
            {
                var flag = true;
                foreach(var t in TagList)
                {
                    if (t == item.Tag)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    NewTagList.Add(item.Tag);
                }

                tags.Append(item.Tag);
                tags.Append(";");
            }
            NewCollectedMovie.Tags = tags.ToString();
            if (NewTagList.Count() > 0)
            {
                CollectedMovieTagList.AddTags(NewTagList);
            }

            NewCollectedMovie.FilmCritic = FilmCriticTextBox.Text;

            NewCollectedMovie.CollectionDate = DateTime.Now.ToLocalTime().ToString();

            SQLiteOperation.InsertData(NewCollectedMovie);

            AddCollectionSplitView.IsPaneOpen = !AddCollectionSplitView.IsPaneOpen;
            CollectButton.Visibility = Visibility.Collapsed;
            CollectedButton.Visibility = Visibility.Visible;
        }

        private void RatingsSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            RatingSlider.Value = (PictureRatingSlider.Value + StoryRatingSlider.Value +
                MusicRatingSlider.Value + PerformanceRatingSlider.Value) / 4;
        }

        private void RatingSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            RatingNumberTextBlock.Text = ((int)RatingSlider.Value).ToString();
        }

        private void TagsSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            TagsSuggestBox.Text = (string)args.SelectedItem;
        }
    }
}
