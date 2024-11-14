using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HydroTaskpane2.Custom_Controls
{
    public partial class SetMaterialWeight : Form
    {
        public double weight;

        public SetMaterialWeight()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(WeightTextBox.Text))
            {
                MessageBox.Show("Please provide a value.");
            }
            else
            {
                if (Double.TryParse(WeightTextBox.Text, out weight))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Only numbers allowed. Please try again.");
                }
            }
        }

        private void WeightTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // allow one decimal point
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aborting...Weight is set to 0 kg. Weight computation might be wrong.");
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
