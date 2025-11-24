using System.Threading.Tasks;

namespace TipCalculatorMAUIApp
{
    public partial class MainPage : ContentPage
    {
        // Store the current bill amount
        private decimal billAmount;

        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles changes to the bill amount entry
        /// </summary>
        private void OnBillAmountChanged(object sender, TextChangedEventArgs e)
        {
            // Reset result when input changes
            ResetResult();
        }

        /// <summary>
        /// Handles tip percentage button clicks
        /// </summary>

        private async void OnTipPercentageClicked(object sender, EventArgs e)
        {

            if (sender is not Button button)
            {
                return;
            }

            // Validate and parse the bill amount
            if (!TryParseBillAmount(out decimal amount))
            {
                ShowInvalidInputMessage();
                return;
            }

            // Extract percentaje from button text (remove % symbol)
            string percentageText = button.Text.Replace("%", "");

            if (!decimal.TryParse(percentageText, out decimal percentage))
            {
                DisplayAlertAsync("Error", "Invalid percentage value", "OK");
                return;
            }

            // Calculate tip and total
            CalculateAndDisplayTip(amount, percentage);
        }

        /// <summary>
        /// Calculates the tip and updates the display labels
        /// </summary>
        private async void CalculateAndDisplayTip(decimal amount, decimal percentage)
        {
            // Claculate tip amount
            decimal tipAmount = amount * (percentage / 100);

            //claculate total amount
            decimal totalAmount = amount + tipAmount;

            // Update labels with formatted currency
            TipAmountLabel.Text = $"Tip Amount: {tipAmount:C2}";
            TotalAmountLabel.Text = $"Total: {totalAmount:C2}";
        }

        /// <summary>
        /// Shows an error message for invalid input
        /// </summary>
        private async void ShowInvalidInputMessage()
        {
            await DisplayAlertAsync("Invalid Input", "Please enter a valid bill amount greater than 0", "OK");
        }

        /// <summary>
        /// Attempts to parse the bill amount from the entry
        /// </summary>
        private bool TryParseBillAmount(out decimal amount)
        {
            string input = BillAmountEntry.Text?.Trim() ?? string.Empty;

            // Try to parse the input as decimal
            return decimal.TryParse(input, out amount) && amount > 0;
        }

        /// <summary>
        /// Resets the result labels to default values
        /// </summary>
        private async Task ResetResult()
        {
            TipAmountLabel.Text = "Tip Amount : £0.00";
            TotalAmountLabel.Text = "Total: £0.00";
        }
    }
}