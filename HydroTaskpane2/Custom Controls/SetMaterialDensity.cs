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
    public partial class SetMaterialDensity : Form
    {
        public int density;

        public SetMaterialDensity(string material)
        {
            InitializeComponent();

            label1.Text = "Density for material " + material + " unknown.\n\nPlease provide a value for weight computation:";
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DensityTextBox.Text))
            {
                MessageBox.Show("Please provide a value.");
            }
            else
            {
                if (Int32.TryParse(DensityTextBox.Text, out density))
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

        private void DensityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aborting...falling back to default density. Weight computation might be wrong.");
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
