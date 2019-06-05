﻿using RepChaser.Models;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace RepChaser.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public ExerciseSummaryItem Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new ExerciseSummaryItem(GuidFactory.NewGuidString(), new ExerciseDayRecord[] { })
            {
                Exercise = "Item name",
                //Description = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}