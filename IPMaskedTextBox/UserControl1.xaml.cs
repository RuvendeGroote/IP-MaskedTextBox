using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Media;

namespace IPmaskedtextbox
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class IPMaskedTextBox : UserControl
    {
        private const string errorMessage = "Please specify a value between 0 and 255.";

        public IPMaskedTextBox()
        {
            InitializeComponent();
        }


        public IPMaskedTextBox(byte[] bytesToFill)
        {
            InitializeComponent();
            firstByte.Text = Convert.ToString(bytesToFill[0]);
            secondByte.Text = Convert.ToString(bytesToFill[1]);
            thirdByte.Text = Convert.ToString(bytesToFill[2]);
            fourthByte.Text = Convert.ToString(Convert.ToString(bytesToFill[3]));
        }

        public string FirstByte { get { return firstByte.Text; } set { firstByte.Text = Convert.ToString(value);} }

        public string SecondByte { get { return secondByte.Text; } set { secondByte.Text = Convert.ToString(value); } }

        public string ThirdByte { get { return thirdByte.Text; } set { thirdByte.Text = Convert.ToString(value); } }

        public string FourthByte { get { return fourthByte.Text; } set { fourthByte.Text = Convert.ToString(value); } }

        public bool FirstFocus { get { return firstByte.IsFocused; } }
        public bool SecondFocus { get { return secondByte.IsKeyboardFocused; } }
        public bool ThirdFocus { get { return thirdByte.IsKeyboardFocused; } }
        public bool FourthFocus { get { return fourthByte.IsKeyboardFocused; } }

        public byte[] GetByteArray()
        {
            byte[] userInput = new byte[4];

            userInput[0] = Convert.ToByte(firstByte.Text);
            userInput[1] = Convert.ToByte(secondByte.Text);
            userInput[2] = Convert.ToByte(thirdByte.Text);
            userInput[3] = Convert.ToByte(fourthByte.Text);

            return userInput;
        }

        
        //arrow, backspace and decimal keys actions. 
        private void firstByte_PreviewKeyDown(object sender, KeyEventArgs e)
        {      

            if (((e.Key == Key.Right || e.Key == Key.OemPeriod || e.Key == Key.Decimal) 
                && (firstByte.CaretIndex == firstByte.Text.Length || firstByte.Text == ""))
                || e.Key == Key.Space)
            {
                secondByte.Focus();
                secondByte.CaretIndex = 0;
                e.Handled = true;
            }                 
        }

        private void secondByte_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Left || e.Key == Key.Back) 
                && (secondByte.CaretIndex == 0 || secondByte.Text == ""))
                
            {
                firstByte.Focus();
                if (firstByte.Text != "")
                {
                    firstByte.CaretIndex = firstByte.Text.Length;
                }
                e.Handled = true;
            }
            else if (((e.Key == Key.Right || e.Key == Key.OemPeriod || e.Key == Key.Decimal) 
                && (secondByte.CaretIndex == secondByte.Text.Length || secondByte.Text == ""))
                || e.Key == Key.Space)
            {
                thirdByte.Focus();
                thirdByte.CaretIndex = 0;
                e.Handled = true;    
            }
        }

        private void thirdByte_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Left || e.Key == Key.Back) 
                && (thirdByte.CaretIndex == 0 || thirdByte.Text == ""))
            {
                secondByte.Focus();
                if (secondByte.Text != "")
                {
                    secondByte.CaretIndex = secondByte.Text.Length;
                }
                e.Handled = true;
            }

            else if (((e.Key == Key.Right || e.Key == Key.OemPeriod || e.Key == Key.Decimal) 
                && (thirdByte.CaretIndex == thirdByte.Text.Length || thirdByte.Text == ""))
                || e.Key == Key.Space)
            {
                fourthByte.Focus();
                fourthByte.CaretIndex = 0;
                e.Handled = true;
            }
        }

        private void fourthByte_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Left || e.Key == Key.Back) && (fourthByte.CaretIndex == 0 || fourthByte.Text == ""))
            {
                thirdByte.Focus();
                if (thirdByte.Text != "")
                {
                    thirdByte.CaretIndex = thirdByte.Text.Length;
                }
                e.Handled = true;
            }
            else if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        
        //checks whether input is digit
        private void firstByte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            handlePreviewTextInput(e);

            if (firstByte.SelectionLength > 0)
            {
                firstByte.Clear();
            }

            else if (firstByte.Text.Length == 3)
            {
                SystemSounds.Beep.Play();
                secondByte.SelectAll();
                secondByte.Focus();
                e.Handled = true;
            }
        }

        private void secondByte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            handlePreviewTextInput(e);

            if (secondByte.SelectionLength > 0)
            {
                secondByte.Clear();
            }

            else if (secondByte.Text.Length == 3)
            {
                SystemSounds.Beep.Play();
                thirdByte.SelectAll();
                thirdByte.Focus();
                e.Handled = true;
            }
        }

        private void thirdByte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            handlePreviewTextInput(e);

            if (thirdByte.SelectionLength > 0)
            {
                thirdByte.Clear();
            }

            else if (thirdByte.Text.Length == 3)
            {
                SystemSounds.Beep.Play();
                fourthByte.SelectAll();
                fourthByte.Focus();
                fourthByte.CaretIndex = 0;
                e.Handled = true;
            }
        }

        private void fourthByte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            handlePreviewTextInput(e);

            if (fourthByte.SelectionLength > 0)
            {
                fourthByte.Clear();
            }

            else if (fourthByte.Text.Length == 3)
            {
                SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }

        //logic for PreviewTextInput
        private void handlePreviewTextInput(TextCompositionEventArgs e)
        {
            if (!char.IsDigit(Convert.ToChar(e.Text)))
            {
                e.Handled = true;
                SystemSounds.Beep.Play();
            }
        }


        //checks whether textbox content > 255 when 3 characters have been entered.
        //clears if > 255, switches to next textbox otherwise 
        private void firstByte_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (firstByte.Text.Length == 3)
            {
                try
                {
                    Convert.ToByte(firstByte.Text);

                }
                catch (Exception exception) when (exception is FormatException || exception is OverflowException)
                {
                    firstByte.Clear();
                    firstByte.Focus();
                    SystemSounds.Beep.Play();
                    MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (firstByte.CaretIndex != 2)
                {
                    secondByte.SelectAll();
                    secondByte.Focus();
                }
            }
        }

        private void secondByte_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (secondByte.Text.Length == 3)
            {
                try
                {
                    Convert.ToByte(secondByte.Text);
                }
                catch (Exception exception) when (exception is FormatException || exception is OverflowException)
                {
                    secondByte.Clear();
                    secondByte.Focus();
                    SystemSounds.Beep.Play();
                    MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (secondByte.CaretIndex != 2)
                {
                    thirdByte.SelectAll();
                    thirdByte.Focus();
                }
            }
        }

        private void thirdByte_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (thirdByte.Text.Length == 3)
            {
                try
                {
                    Convert.ToByte(thirdByte.Text);
                }
                catch (Exception exception) when (exception is FormatException || exception is OverflowException)
                {
                    thirdByte.Clear();
                    thirdByte.Focus();
                    SystemSounds.Beep.Play();
                    MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (thirdByte.CaretIndex != 2)
                {
                    fourthByte.SelectAll();
                    fourthByte.Focus();
                }
            }
        }

        private void fourthByte_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (fourthByte.Text.Length == 3)
            {
                try
                {
                    Convert.ToByte(fourthByte.Text);
                }
                catch (Exception exception) when (exception is FormatException || exception is OverflowException)
                {
                    fourthByte.Clear();
                    fourthByte.Focus();
                    SystemSounds.Beep.Play();
                    MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                    return;
                }
            }
        }
    }
}
