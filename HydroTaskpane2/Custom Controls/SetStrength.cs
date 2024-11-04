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
    public partial class SetStrength : Form
    {
        public double tensile;
        public double yield;

        public SetStrength()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tensileTextBox.Text) || string.IsNullOrWhiteSpace(yieldTextBox.Text))
            {
                MessageBox.Show("Please provide missing value");
            }
            else
            {
                if (Double.TryParse(tensileTextBox.Text, out tensile) && Double.TryParse(yieldTextBox.Text, out yield))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    bool tensileLetterInString = tensileTextBox.Text.Any(x => char.IsLetter(x));
                    bool yieldLetterInString = yieldTextBox.Text.Any(x => char.IsLetter(x));

                    if (tensileLetterInString)
                    {
                        tensileTextBox.Text = "";
                    }
                    else if (yieldLetterInString)
                    {
                        yieldTextBox.Text = "";
                    }
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aborting...Strengths are set to 0 N/mm². Strength computation might be wrong.");

            tensile = 0;
            yield = 0;

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tensileTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void yieldTextBox_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
