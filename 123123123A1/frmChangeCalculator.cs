using System;
using System.Windows.Forms;

namespace _123123123A1
{
    public partial class frmChangeCalculator : Form
    {
        public frmChangeCalculator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the calculate button is clicked, three things take place:
        /// 1) we determine if the inputs are valid and numeric
        /// 2) we determine if the paid amount is greater than the total
        /// 3) we round to the nearest nickel and determine the correct coin change and amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            String TotalAmount = txtTotal.Text;
            String PaidAmount = txtPaid.Text;

            Decimal total, paid, change;
            if(!Decimal.TryParse(TotalAmount,out total))
            {
                MessageBox.Show("Total is not a valid decimal value", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Decimal.TryParse(PaidAmount, out paid))
            {
                MessageBox.Show("Paid is not a valid decimal value", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
          
            //ensure the customer paid enough to cover the total
            if(paid<total)
            {
                Decimal RemainingBalance = total - paid;
                MessageBox.Show("Payment is insufficient, remaining balance is: " + String.Format("{0:c}", RemainingBalance),"Insufficient Payment", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //determine how much change we owe.
            change = paid - total;

            //work with the change in cents, instead of as a decimal
            int changeInCents = Convert.ToInt32(Math.Round(change * 100));

            // 200, 100, 25, 10, 5
            int toonies, loonies, quarters, dimes, nickles;


            // total = $2.00, paid = $2.98 => change = $0.98, change => $1.00
            //change = 98 
            // change % 5 = 3
            // 
            // change = 97
            // change % 5 = 2
            
            if (changeInCents % 5 > 2)
            {
                int roundingAmount = 5 - (changeInCents % 5);
                changeInCents += roundingAmount;
            }
            else
            { //97 = 2 or 96 =1
                int roundingAmount = changeInCents % 5;
                changeInCents -= roundingAmount;
            }
            change = changeInCents / 100.0m;
            txtChange.Text = String.Format("{0:c}", change);

            //395 cents
            toonies = changeInCents / 200;
            changeInCents %= 200;
            loonies = changeInCents / 100;
            changeInCents %= 100;
            quarters = changeInCents / 25;
            changeInCents %= 25;
            dimes = changeInCents / 10;
            changeInCents %= 10;
            nickles = changeInCents / 5;
            changeInCents %= 5; //this should result in a zero if everything is working correctly.

            if (changeInCents != 0)
            {
                MessageBox.Show("Change in cents is non-zero after calculating the nickels.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtToonies.Text = toonies.ToString();
            txtLoonies.Text = loonies.ToString();
            txtQuarters.Text = quarters.ToString();
            txtDimes.Text = dimes.ToString();
            txtNickels.Text = nickles.ToString();
        }

        /// <summary>
        /// When the clear button is clicked, we should clear all the textboxes on the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            //Clear all the textboxes
            txtToonies.Text = "";
            txtLoonies.Text = "";
            txtQuarters.Text = "";
            txtDimes.Text = "";
            txtNickels.Text = "";
            txtChange.Text = "";
            txtPaid.Text = "";
            txtTotal.Text = "";
        }
    }
}
