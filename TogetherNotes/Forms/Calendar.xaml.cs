using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using TogetherNotes.Utils;
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