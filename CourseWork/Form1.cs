using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class Form1 : Form
    {
        private CustomType customType;
        public Form1()
        {
            InitializeComponent();
            customType = new CustomType();
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            if (long.TryParse(numberTextBox.Text, out long number))
            {
                customType.Add(number);
                numberTextBox.Clear();
            }
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            customType.SortDescending();
            sortedValuesTextBox.Text = customType.ToString();
        }
        private void calculateButton_Click(object sender, EventArgs e)
        {
            try
            {
                LongNumber firstNumber = new LongNumber(firstTextBox.Text);
                LongNumber secondNumber = new LongNumber(secondTextBox.Text);

                LongNumber result;
                switch (operationComboBox.Text)
                {
                    case "+":
                        result = firstNumber + secondNumber;
                        break;
                    case "-":
                        result = firstNumber - secondNumber;
                        break;
                    case "*":
                        result = firstNumber * secondNumber;
                        break;
                    case "/":
                        result = firstNumber / secondNumber;
                        break;
                    case "%":
                        result = firstNumber % secondNumber;
                        break;
                    default:
                        MessageBox.Show("Операция не распознана.");
                        return;
                }

                resultTextBox.Text = result.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка ввода чисел.");
            }
        }


    }
}
