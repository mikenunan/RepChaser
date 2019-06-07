using RepChaser.Models;
using RepChaser.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace RepChaser.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        private readonly ItemDetailViewModel _viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new ExerciseSummaryItem(GuidFactory.NewGuidString(), new ExerciseDayRecord[] { })
            {
                Exercise = "Some exercise",
                Description = "Exercise description."
            };

            _viewModel = new ItemDetailViewModel(item);
            BindingContext = _viewModel;
        }
    }
}