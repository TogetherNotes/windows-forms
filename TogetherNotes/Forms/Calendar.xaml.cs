using System.Windows.Controls;
using TogetherNotes.ViewModel;

namespace TogetherNotes.Forms
{
    public partial class Calendar : UserControl
    {
        public Calendar()
        {
            InitializeComponent();
            DataContext = new CalendarVM();
            
        }
    }
}