using ColorGalleryApp.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ColorGalleryApp
{
    public partial class MainPage : ContentPage
    {
        // ObservableCollection automatically notifies the UI of change
        private ObservableCollection<ColorItem> _colors;

        public MainPage()
        {
            InitializeComponent();

            LoadColors();
        }

        /// <summary>
        /// Loads the color collection with predefined colors
        /// </summary>
        private void LoadColors()
        {
            // Create and populate the ObservableCollection
            _colors = new ObservableCollection<ColorItem>
            {
                new ColorItem("Ocean Blue", "#0EA5E9"),
                new ColorItem("Sunset Orange", "#F97316"),
                new ColorItem("Forest Green", "#22C55E"),
                new ColorItem("Royal Purple", "#A855F7"),
                new ColorItem("Ruby Red", "#EF4444"),
                new ColorItem("Golden Yellow", "#EAB308"),
                new ColorItem("Rose Pink", "#EC4899"),
                new ColorItem("Midnight Navy", "#1E3A8A"),
                new ColorItem("Emerald", "#10B981"),
                new ColorItem("Coral", "#FF6B6B")
            };

            // Bind the collection to the CollectionView
            ColorsCollectionView.ItemsSource = _colors;

            // Update the total count
            UpdateTotalCount();
        }

        /// <summary>
        /// Updates the total colors count in the footer
        /// </summary>
        private void UpdateTotalCount()
        {
            TotalColorsLabel.Text = _colors.Count.ToString();
        }

        /// <summary>
        /// Handles color selection - shows preview
        /// </summary>
        private async void OnColorSelected(object sender, SelectionChangedEventArgs e)
        {
            // Check if something was selected
            if (e.CurrentSelection.Count == 0)
                return;

            // Get the selected color
            var selectedColor = e.CurrentSelection[0] as ColorItem;

            if (selectedColor != null)
            {
                // Store original background
                var originalColor = this.BackgroundColor;

                // Change background to selected color
                this.BackgroundColor = selectedColor.ColorValue;

                // Show information alert
                bool result = await DisplayAlertAsync(
                    $"✨ {selectedColor.Name}",
                    $"Hex Code: {selectedColor.Hex}\n\n" +
                    $"RGB: {GetRGBFromHex(selectedColor.Hex)}\n\n" +
                    $"Background changed to preview the color!",
                    "Keep Color",
                    "Restore");

                // If user clicks "Restore", change back
                if (!result)
                {
                    this.BackgroundColor = originalColor;
                }
            }

            // Deselect the item for better UX
            ColorsCollectionView.SelectedItem = null;
        }

        /// <summary>
        /// Converts hex color to RGB string for display
        /// </summary>
        private string GetRGBFromHex(string hex)
        {
            try
            {
                var color = Color.FromArgb(hex);
                int r = (int)(color.Red * 255);
                int g = (int)(color.Green * 255);
                int b = (int)(color.Blue * 255);
                return $"R:{r} G:{g} B:{b}";
            }
            catch
            {
                return "N/A";
            }
        }
    }
}