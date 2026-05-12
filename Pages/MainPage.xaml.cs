using KeyGenerator.Models;
using KeyGenerator.PageModels;

namespace KeyGenerator.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void OnGenerateClicked(object sender, EventArgs e)
        {
            // Reset
            ResultCard.IsVisible = false;
            ErrorLabel.IsVisible = false;

            var mac = MacEntry.Text?.Trim();

            // Validation : champ vide
            if (string.IsNullOrWhiteSpace(mac))
            {
                ShowError("Veuillez saisir une adresse MAC.");
                return;
            }

            // Normalisation : on accepte A1B2C3D4E5F6 ou A1:B2:C3 ou A1-B2-C3
            var normalized = mac.ToUpper().Replace(":", "").Replace("-", "");

            // Validation : doit faire exactement 12 caractères hex
            if (normalized.Length != 12 || !normalized.All(c => "0123456789ABCDEF".Contains(c)))
            {
                ShowError("Format invalide. L'adresse MAC doit contenir 12 caractères hexadécimaux.\nEx: A1B2C3D4E5F6");
                return;
            }

            // Génération
            var key = LicenseManager.GenerateKey(normalized);

            // Affichage
            KeyLabel.Text = key;
            ResultCard.IsVisible = true;
        }

        private async void OnCopyClicked(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(KeyLabel.Text);
            await DisplayAlert("Copié ✅", $"Clé copiée :\n{KeyLabel.Text}", "OK");
        }

        private void ShowError(string msg)
        {
            ErrorLabel.Text = msg;
            ErrorLabel.IsVisible = true;
        }
    }
}