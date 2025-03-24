using System.Windows.Controls;
using TogetherNotes.ViewModel;

namespace TogetherNotes.Forms
{
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
            this.DataContext = new HomeVM();
        }
    }
}