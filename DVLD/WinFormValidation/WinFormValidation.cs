using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    internal class WinFormValidation
    {
        public static void ValidateTextBoxeIsNullOrEmpty(ErrorProvider errorProvider, TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                errorProvider.SetError(textBox, "This field is required.");
            }
            else
            {
                errorProvider.SetError(textBox, "");
            }
        }

        public static void ValidateRichTextBoxIsNullOrEmpty(ErrorProvider errorProvider, RichTextBox richTextBox)
        {
            if (string.IsNullOrWhiteSpace(richTextBox.Text))
            {
                errorProvider.SetError(richTextBox, "This field is required.");
            }
            else
            {
                errorProvider.SetError(richTextBox, "");
            }
        }

        public static void ValidateComboBoxIsNullOrEmpty(ErrorProvider errorProvider, ComboBox comboBox)
        {
            if (comboBox.SelectedIndex == 0|| comboBox.SelectedIndex == -1 || string.IsNullOrEmpty(comboBox.Text))
            {
                errorProvider.SetError(comboBox, "Please select an option.");
            }
            else
            {
                errorProvider.SetError(comboBox, "");
            }
        }

        public static void inputControl_Validating_IsNullOrEmpty(ErrorProvider errorProvider1, object sender, CancelEventArgs e)
        {
            if (sender is TextBox)
                WinFormValidation.ValidateTextBoxeIsNullOrEmpty(errorProvider1, sender as TextBox);
            else if (sender is RichTextBox)
                WinFormValidation.ValidateRichTextBoxIsNullOrEmpty(errorProvider1, sender as RichTextBox);
            else if (sender is ComboBox)
                WinFormValidation.ValidateComboBoxIsNullOrEmpty(errorProvider1, sender as ComboBox);
        }


        public static void ValidatingEmailInTextBox(ErrorProvider errorProvider, TextBox textBox)
        {
            if (!string.IsNullOrEmpty(textBox.Text))
            {

                if (!textBox.Text.Contains("@") || !textBox.Text.Contains("."))
                {
                    errorProvider.SetError(textBox, "this is not a valid email address.");
                }
                else
                {
                    errorProvider.SetError(textBox, "");
                }
            }
            else
            {
                errorProvider.SetError(textBox, "");
            }
        }

        public static bool HasValidationErrors(Control Container, ErrorProvider errorProvider)
        {
        
            foreach (Control crtl in Container.Controls)
            {
                if(crtl.HasChildren &&HasValidationErrors (crtl, errorProvider))
                    return true;
                else if (!string.IsNullOrEmpty(errorProvider.GetError(crtl)))
                    return true;
            }
            return false;
        }
    }
}
