using System.Windows.Controls;
using TogetherNotes.ViewModel;

namespace TogetherNotes.Forms
{
    public partial class Users : UserControl
    {
        public Users()
        {
            InitializeComponent();
            this.DataContext = new UsersVM();
        }
    }
}