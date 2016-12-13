using System.Windows.Forms;

namespace Mass_helper.AdditionalForms
{
    public partial class NotesForm : Form
    {
        public NotesForm(string note)
        {
            InitializeComponent();
            
            NotetextBox.Text = note;
            OKButton.Click += (sender, args) => { Close(); };
            OKButton.Select();
        }
    }
}
