using RepChaser.Models;

namespace RepChaser.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ExerciseSummaryItem Item { get; set; }

        public ItemDetailViewModel(ExerciseSummaryItem item = null)
        {
            Title = item?.Exercise;
            Item = item;
        }
    }
}
