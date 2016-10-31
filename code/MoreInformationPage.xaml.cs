using MovieCollection.data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
    public sealed partial class MoreInformationPage : Page
    {
        private CollectedMovie movie;
        private ObservableCollection<ForTagsDataBind> AddTagsList;
        private List<string> SuggestTags;
        public MoreInformationPage()
        {
            this.InitializeComponent();
            AddTagsList = new ObservableCollection<ForTagsDataBind>();
            SuggestTags = CollectedMovieTagList.TagList;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            movie = (CollectedMovie)e.Parameter;

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

            string divider = "     ";
            RatingTextBlock.Text = "Rating:" + movie.RatingSum + divider + "Picture:" + movie.RatingPicture + divider
                + "Story:" + movie.RatingStory + divider + "Music:" + movie.RatingMusic + divider
                + "Performance:" + movie.RatingPerformance;
            TagsTextBlock.Text = "Tag:" + movie.Tags.Replace(";", " / ");
            FlimCriticTextBlock.Text = movie.FilmCritic;

            RatingSlider.Value = Convert.ToInt32(movie.RatingSum);
            PictureRatingSlider.Value = Convert.ToInt32(movie.RatingPicture);
            StoryRatingSlider.Value = Convert.ToInt32(movie.RatingStory);
            MusicRatingSlider.Value = Convert.ToInt32(movie.RatingMusic);
            PerformanceRatingSlider.Value = Convert.ToInt32(movie.RatingPerformance);
            getTags();
            FilmCriticTextBox.Text = movie.FilmCritic;
            RatingNumberTextBlock.Text = ((int)RatingSlider.Value).ToString();
        }

        private void getTags()
        {
            StringBuilder tag = new StringBuilder();
            string tags = movie.Tags;
            bool flag = false;
            foreach(var ch in tags)
            {
                if (ch == ';' && flag)
                {
                    var data = new ForTagsDataBind() { Tag = tag.ToString() };
                    AddTagsList.Add(data);
                    tag.Clear();
                }
                else if (!flag)
                {
                    flag = true;
                }
                else
                {
                    tag.Append(ch);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack) Frame.GoBack();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditSplitView.IsPaneOpen = !EditSplitView.IsPaneOpen;
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            
            var suggestTagList = SuggestTags.Where(p => p.StartsWith(TagsSuggestBox.Text)).ToList();
            TagsSuggestBox.ItemsSource = suggestTagList;
        }

        private void TagsSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var tag = new ForTagsDataBind() { Tag = TagsSuggestBox.Text };
            bool flag = true;
            foreach (var item in AddTagsList)
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

        private void EditCancelButton_Click(object sender, RoutedEventArgs e)
        {
            EditSplitView.IsPaneOpen = !EditSplitView.IsPaneOpen;
        }

        private void EditDoneButton_Click(object sender, RoutedEventArgs e)
        {
            movie.RatingSum = ((int)RatingSlider.Value).ToString();
            if (PictureRatingSlider.Value == 0 && StoryRatingSlider.Value == 0 &&
                MusicRatingSlider.Value == 0 && PerformanceRatingSlider.Value == 0)
            {
                movie.RatingPicture = ((int)RatingSlider.Value).ToString();
                movie.RatingStory = ((int)RatingSlider.Value).ToString();
                movie.RatingMusic = ((int)RatingSlider.Value).ToString();
                movie.RatingPerformance = ((int)RatingSlider.Value).ToString();
            }
            else
            {
                movie.RatingPicture = ((int)PictureRatingSlider.Value).ToString();
                movie.RatingStory = ((int)StoryRatingSlider.Value).ToString();
                movie.RatingMusic = ((int)MusicRatingSlider.Value).ToString();
                movie.RatingPerformance = ((int)PerformanceRatingSlider.Value).ToString();
            }

            StringBuilder tags = new StringBuilder(";");
            var TagList = CollectedMovieTagList.TagList;
            var NewTagList = new List<string>();
            foreach (var item in AddTagsList)
            {
                var flag = true;
                foreach (var t in TagList)
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
            movie.Tags = tags.ToString();
            if (NewTagList.Count() > 0)
            {
                CollectedMovieTagList.AddTags(NewTagList);
            }

            movie.FilmCritic = FilmCriticTextBox.Text;
            
            SQLiteOperation.UpdateData(movie);

            string divider = "     ";
            RatingTextBlock.Text = "Rating:" + movie.RatingSum + divider + "Picture:" + movie.RatingPicture + divider
                + "Story:" + movie.RatingStory + divider + "Music:" + movie.RatingMusic + divider
                + "Performance:" + movie.RatingPerformance;
            TagsTextBlock.Text = "Tag:" + movie.Tags.Replace(";", " / ");
            FlimCriticTextBlock.Text = movie.FilmCritic;

            EditSplitView.IsPaneOpen = !EditSplitView.IsPaneOpen;
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

        private async void ImdbPageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Uri targetUri = new Uri("http://www.imdb.com/title/"+movie.imdbID);
                await Launcher.LaunchUriAsync(targetUri);
            }
            catch { }
        }
    }
}
