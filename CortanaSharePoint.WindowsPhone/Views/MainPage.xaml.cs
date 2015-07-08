using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechRecognition;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CortanaSharePoint.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                var storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync
                    (new Uri("ms-appx:///VoiceCommandDefinition_SharePointCellenza.xml"));

                await Windows.Media.SpeechRecognition.VoiceCommandManager.InstallCommandSetsFromStorageFileAsync(storageFile);
            }
        }

        /// <summary>
        /// Install the voice commands
        /// </summary>
        private async void InstallVoiceCommands()
        {
            string error = string.Empty;
            try
            {
                const string Wp81VCDPath = "ms-appx:///VoiceCommandDefinition_SharePointCellenza.xml";
                Uri vcdUri = new Uri(Wp81VCDPath);
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(vcdUri);
                await VoiceCommandManager.InstallCommandSetsFromStorageFileAsync(file);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            if (string.IsNullOrEmpty(error) == false)
            {
                MessageDialog dialog = new MessageDialog("Error during voice command installation: " + error);
                await dialog.ShowAsync();
            }
        }

        private void lnkCortanaListItems_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CortanaListItems), null);
        }

        #region AppBar events
        private void AppBarBtnSettings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings), null);
        }

        private void lnkCortanaCalendarItems_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CortanaCalendar), null);
        }
        private void AppBarBtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CortanaSearch), null);
        }
        #endregion
    }
}
