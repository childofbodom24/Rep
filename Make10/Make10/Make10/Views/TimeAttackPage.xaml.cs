using Make10.Interfaces;
using Make10.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Make10.Views
{
    public partial class TimeAttackPage : ContentPage
    {
        public TimeAttackPage(IResultService resultService)
        {
            InitializeComponent();
            //this.bg.Source = ImageSource.FromResource("Make10.Images.back.png");
            resultService.ResultUpdated += () =>
             {
                 this.DisplayAlert("title", "message", "OK");
             };
        }
        
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            //↓これをすると2回目以降本画面に遷移できない
            //var vm = this.BindingContext as TimeAttackPageViewModel;
        }

        protected override void OnAppearing()
        {
            this.ExecuteStartLabelAnimation();

            foreach (var l in this.resultRecordsItemsControl.ItemsPanel.Children.Select(c => (c as StackLayout).Children.ElementAt(2)).Cast<Label>())
            {
                l.PropertyChanged -= this.ExecuteTimeTextAnimation;
                l.PropertyChanged += this.ExecuteTimeTextAnimation;
            }
        }

        private async Task<bool> ConfirmQuit()
        {
            return await DisplayAlert("Confirmation", "Quit?", "Yes", "No");
        }

        private async void ExecuteStartLabelAnimation()
        {
            this.startLabel.IsVisible = true;
            this.startLabel.Opacity = 1;
            this.startLabel.Scale = 1;
            this.startLabel.FadeTo(0.0, 1500);
            await this.startLabel.ScaleTo(5, 1500);
            this.startLabel.IsVisible = false;
        }

        private async void ExecuteTimeTextAnimation(object sender, PropertyChangedEventArgs e)
        {
            var label = sender as Label;
            if (label != null && e.PropertyName == "Text")
            {
                //label.Scale = 0;
                //label.Opacity = 0.0;
                this.correctLabel.IsVisible = true;
                this.correctLabel.Scale = 4;
                this.correctLabel.Opacity = 1.0;
                //label.FadeTo(1.0, 1500);
                //label.ScaleTo(1.0, 1500);
                await Task.Delay(500);
                await this.correctLabel.FadeTo(0.0, 1000);
                this.correctLabel.IsVisible = false;
            }
        }
    }
}
