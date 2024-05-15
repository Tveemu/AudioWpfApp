using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Globalization;

namespace AudioWpfApp
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, int> ensembleIndices = new Dictionary<string, int>
        {
            {"KM", 1},
            {"KN", 1},
            {"KQ", 0},
            {"KV", 0},
            {"KZ", 0},
            {"KS", 0}
        };

        // Using auto-properties with backing fields for ObservableCollection
        public ObservableCollection<Performer> Performers { get; set; }
        public ObservableCollection<Author> Authors { get; set; }

        private Point _startPoint;

        private string filePath;
        private const string PerformerDataFormat = "myFormat";
        private const string AuthorDataFormat = "myAuthorFormat";

        public class Performer
        {
            public string PerformerName { get; set; }
            public string PerformerInstrument { get; set; }
        }

        public class Author
        {
            public string AuthorName { get; set; }
            public string AuthorRole { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            PopulateGenreComboBox();
            SetButtonTooltips();
            InitializeEnsembleButtons();
            Performers = new ObservableCollection<Performer>();
            PerformerDataGrid.ItemsSource = Performers;

            Authors = new ObservableCollection<Author>();
            AuthorDataGrid.ItemsSource = Authors;
        }

        private void PopulateGenreComboBox()
        {
            var genreCodes = new List<string>
            {
                "L1A", "L1AS", "L1B", "L1BS", "L1C", "L1CS", "L1D", "L1DS", "L1E", "L1ES", "L1F", "L1FS",
                "L1L", "L1LS", "L1U", "L1V", "L1VS", "L1X", "L2A", "L2AA", "L2B", "L2BB", "L2L", "L2N",
                "L2X", "L3A", "L3L", "L3U", "L3X", "L4AA", "L4L", "L5AA", "L5B", "L5L", "L6A", "L6B",
                "L6D", "L6L", "L6T", "L6X", "L9A", "L9B", "L9C", "L9D", "L9F", "L9G", "L9X", "L9T", "KMUS", "VMUS"
            };

            foreach (string code in genreCodes)
            {
                var item = new ComboBoxItem
                {
                    Content = code,
                    ToolTip = TranslateGenre(code) // Assuming TranslateGenre returns the full description
                };
                otherGenresComboBox.Items.Add(item);
            }
        }

        private void SetButtonTooltips()
        {
            KIButton.ToolTip = TranslateEnsemble("KI");

            L4AButton.ToolTip = TranslateGenre("L4A");
            L5AButton.ToolTip = TranslateGenre("L5A");
            L6CButton.ToolTip = TranslateGenre("L6C");
            L6VButton.ToolTip = TranslateGenre("L6V");
            L9EButton.ToolTip = TranslateGenre("L9E");
            L9TButton.ToolTip = TranslateGenre("L9T");
        }

        private void InitializeEnsembleButtons()
        {
            // Ensure all ensemble buttons are initialized properly
            foreach (var key in ensembleIndices.Keys.ToList())
            {
                UpdateEnsembleButtonText(key);
            }
        }

        private void CustomIDTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Append "DIGI-" only if the TextBox is empty
            if (string.IsNullOrEmpty(CustomIDTextBox.Text))
            {
                CustomIDTextBox.Text = "DIGI-";
            }
        }

        private void AddPerformer_Click(object sender, RoutedEventArgs e)
        {
            Performers.Add(new Performer());
        }

        private void AddAuthor_Click(object sender, RoutedEventArgs e)
        {
            Authors.Add(new Author());
        }

        private void DeletePerformerButton_Click(object sender, RoutedEventArgs e)
        {
            // Use the sender argument to get the button that was clicked
            if (sender is Button button)
            {
                // Get the DataContext of the button, which is the Performer instance it's bound to
                if (button.DataContext is Performer performerToDelete && Performers.Contains(performerToDelete))
                {
                    // Remove the performer from the collection
                    Performers.Remove(performerToDelete);
                }
            }
        }

        private void DeleteAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            // Use the sender argument to get the button that was clicked
            if (sender is Button button)
            {
                // Get the DataContext of the button, which is the Author instance it's bound to
                if (button.DataContext is Author authorToDelete && Authors.Contains(authorToDelete))
                {
                    // Remove the author from the collection
                    Authors.Remove(authorToDelete);
                }
            }
        }

        // Handler for mouse button press
        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(null);
        }

        // Handler for mouse move
        private void DataGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;

            var diff = _startPoint - e.GetPosition(null);
            if (Math.Abs(diff.X) < SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(diff.Y) < SystemParameters.MinimumVerticalDragDistance) return;

            var grid = sender as DataGrid;
            var row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);
            if (row == null) return;

            object item = grid.ItemContainerGenerator.ItemFromContainer(row);
            string dataFormat = grid.Equals(PerformerDataGrid) ? PerformerDataFormat : grid.Equals(AuthorDataGrid) ? AuthorDataFormat : null;
            if (item != null && dataFormat != null)
            {
                var dragData = new DataObject(dataFormat, item);
                DragDrop.DoDragDrop(row, dragData, DragDropEffects.Move);
            }
        }

        // Handler for drop
        private void DataGrid_Drop(object sender, DragEventArgs e)
        {
            DataGrid targetGrid = sender as DataGrid;

            // Check the format and source of the drag action for Performers
            if (e.Data.GetDataPresent("myFormat"))
            {
                if (targetGrid != PerformerDataGrid)
                {
                    // If the target grid is not the PerformerDataGrid, cancel the drag action.
                    e.Handled = true;
                    return;
                }

                Performer performer = e.Data.GetData("myFormat") as Performer;
                MoveItemInCollection(PerformerDataGrid, Performers, performer, e.GetPosition(targetGrid));
            }
            // Check the format and source of the drag action for Authors
            else if (e.Data.GetDataPresent("myAuthorFormat"))
            {
                if (targetGrid != AuthorDataGrid)
                {
                    // If the target grid is not the AuthorDataGrid, cancel the drag action.
                    e.Handled = true;
                    return;
                }

                Author author = e.Data.GetData("myAuthorFormat") as Author;
                MoveItemInCollection(AuthorDataGrid, Authors, author, e.GetPosition(targetGrid));
            }

            foreach (var item in PerformerDataGrid.Items)
            {
                var row = (DataGridRow)PerformerDataGrid.ItemContainerGenerator.ContainerFromItem(item);
                if (row != null)
                {
                    row.Background = new SolidColorBrush(Colors.Transparent); // Reset to default
                }
            }

            foreach (var item in AuthorDataGrid.Items)
            {
                var row = (DataGridRow)AuthorDataGrid.ItemContainerGenerator.ContainerFromItem(item);
                if (row != null)
                {
                    row.Background = new SolidColorBrush(Colors.Transparent); // Reset to default
                }
            }
        }

        private void DataGridRow_DragEnter(object sender, DragEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            if (row != null)
            {
                // Change the background color to indicate a drag is over this row
                row.Background = new SolidColorBrush(Colors.LightSkyBlue);
            }
        }

        private void DataGridRow_DragLeave(object sender, DragEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            if (row != null)
            {
                // Reset the background color when the drag leaves the row
                row.Background = new SolidColorBrush(Colors.Transparent); // Or any default color
            }
        }

        // This is a generic method to move items within a collection based on drop position.
        private void MoveItemInCollection<T>(DataGrid grid, ObservableCollection<T> collection, T item, Point dropPosition) where T : class
        {
            var hitTestResult = VisualTreeHelper.HitTest(grid, dropPosition);
            var dropTargetRow = FindAncestor<DataGridRow>(hitTestResult.VisualHit);
            if (dropTargetRow != null)
            {
                int targetIndex = grid.ItemContainerGenerator.IndexFromContainer(dropTargetRow);
                int originalIndex = collection.IndexOf(item);

                if (targetIndex >= 0 && targetIndex < collection.Count && originalIndex != targetIndex && originalIndex >= 0)
                {
                    collection.Move(originalIndex, targetIndex);
                }
            }
        }

        // Utility method to find ancestor of a given type
        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void LanguageButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast the sender to a Button
            var button = sender as Button;
            if (button != null)
            {
                string language = button.Content.ToString();

                // Split the existing text into lines to check for duplicates
                var languagesInBox = LanguageTextBox.Text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                // Check if the language is already in the TextBox
                if (!languagesInBox.Contains(language))
                {
                    // If the TextBox already contains text, append a newline before the language
                    if (!string.IsNullOrEmpty(LanguageTextBox.Text))
                    {
                        LanguageTextBox.Text += "\n";
                    }

                    // Append the button's content (language) to the TextBox
                    LanguageTextBox.Text += language;
                }
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Parent is ContextMenu contextMenu)
            {
                // Assuming the context menu is directly attached to a TextBox
                if (contextMenu.PlacementTarget is TextBox textBox)
                {
                    // Split the TextBox content into lines
                    var lines = textBox.Text.Split(new[] { "\n" }, StringSplitOptions.None).ToList();

                    // Get the current line number from the caret position
                    int index = textBox.GetLineIndexFromCharacterIndex(textBox.CaretIndex);

                    // Remove the line if the index is valid
                    if (index >= 0 && index < lines.Count)
                    {
                        lines.RemoveAt(index);
                        textBox.Text = string.Join("\n", lines);
                    }
                }
            }
        }

        private void OtherLanguagesComboBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            comboBox.IsDropDownOpen = true;

            string text = (comboBox.Text + e.Text).ToLowerInvariant();

            // Check if the typed text is a part of any item
            bool itemExists = comboBox.Items.Cast<ComboBoxItem>().Any(item => item.Content.ToString().ToLowerInvariant().StartsWith(text));
        }

        private void OtherLanguagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string language = selectedItem.Content.ToString();
                if (!LanguageTextBox.Text.Split('\n').Contains(language))
                {
                    if (!string.IsNullOrEmpty(LanguageTextBox.Text))
                    {
                        LanguageTextBox.Text += $"\n{language}";
                    }
                    else
                    {
                        LanguageTextBox.Text = language;
                    }
                }
                comboBox.SelectedIndex = -1;
            }
        }

        private void OtherLanguagesComboBox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            var text = comboBox.Text;

            // Check if the text is exactly the content of any item
            ComboBoxItem item = comboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(i => i.Content.ToString().Equals(text, StringComparison.InvariantCultureIgnoreCase));

            if (item != null)
            {
                comboBox.SelectedItem = item;
                string language = item.Content.ToString();
                if (!LanguageTextBox.Text.Split('\n').Contains(language))
                {
                    if (!string.IsNullOrEmpty(LanguageTextBox.Text))
                    {
                        LanguageTextBox.Text += $"\n{language}";
                    }
                    else
                    {
                        LanguageTextBox.Text = language;
                    }
                }
                comboBox.SelectedIndex = -1;
            }
            else
            {
                comboBox.Text = ""; // Reset the text if no match
            }
        }

        private void OtherGenresComboBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            comboBox.IsDropDownOpen = true;

            string text = (comboBox.Text + e.Text).ToLowerInvariant();

            // Check if the typed text is a part of any item
            bool itemExists = comboBox.Items.Cast<ComboBoxItem>().Any(item => item.Content.ToString().ToLowerInvariant().StartsWith(text));
        }

        private void OtherGenresComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string genre = selectedItem.Content.ToString();
                if (!GenreTextBox.Text.Split('\n').Contains(genre))
                {
                    if (!string.IsNullOrEmpty(GenreTextBox.Text))
                    {
                        GenreTextBox.Text += $"\n{genre}";
                    }
                    else
                    {
                        GenreTextBox.Text = genre;
                    }
                }
                comboBox.SelectedIndex = -1;
            }
        }

        private void OtherGenresComboBox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            var text = comboBox.Text;

            // Check if the text is exactly the content of any item
            ComboBoxItem item = comboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(i => i.Content.ToString().Equals(text, StringComparison.InvariantCultureIgnoreCase));

            if (item != null)
            {
                comboBox.SelectedItem = item;
                string genre = item.Content.ToString();
                if (!GenreTextBox.Text.Split('\n').Contains(genre))
                {
                    if (!string.IsNullOrEmpty(GenreTextBox.Text))
                    {
                        GenreTextBox.Text += $"\n{genre}";
                    }
                    else
                    {
                        GenreTextBox.Text = genre;
                    }
                }
                comboBox.SelectedIndex = -1;
            }
            else
            {
                comboBox.Text = ""; // Reset the text if no match
            }
        }


        private void EnsembleButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast the sender to a Button
            var button = sender as Button;
            if (button != null)
            {
                string ensemble = button.Content.ToString();

                // Split the existing text into lines to check for duplicates
                var ensemblesInBox = EnsembleTextBox.Text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                // Check if the ensemble is already in the TextBox
                if (!ensemblesInBox.Contains(ensemble))
                {
                    // If the TextBox already contains text, append a newline before the ensemble
                    if (!string.IsNullOrEmpty(EnsembleTextBox.Text))
                    {
                        EnsembleTextBox.Text += "\n";
                    }
                    // Append the button's content (ensemble) to the TextBox
                    EnsembleTextBox.Text += ensemble;
                }
            }
        }

        private void UpdateEnsembleButtonText(string ensemblePrefix)
        {
            // Handling special case for "KQ" to skip "KQ1"
            int index = ensembleIndices[ensemblePrefix];
            string buttonContent;
            if (ensemblePrefix == "KQ" && index == 1)
            {
                buttonContent = ensemblePrefix + "2"; // Start from "KQ2" if it's initially "KQ1"
                ensembleIndices[ensemblePrefix] = 2; // Adjust the index to reflect this change
            }
            else
            {
                buttonContent = index > 0 ? $"{ensemblePrefix}{index}" : ensemblePrefix;
            }

            // Assuming all buttons are named with their prefixes, like KMButton, KNButton, etc.
            var button = this.FindName($"{ensemblePrefix}Button") as Button;
            if (button != null)
            {
                button.Content = buttonContent;
                button.ToolTip = TranslateEnsemble(buttonContent);
            }
        }


        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            // Determine the ensemble prefix from the sender's Name or Tag
            var button = sender as Button;
            var prefix = button.Tag.ToString();

            if (ensembleIndices[prefix] < 9)
            {
                ensembleIndices[prefix]++;
                if (prefix == "KQ" && ensembleIndices[prefix] == 1)
                {
                    ensembleIndices[prefix]++; // Skip "KQ1"
                }
                UpdateEnsembleButtonText(prefix);
            }
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            // Determine the ensemble prefix from the sender's Name or Tag
            var button = sender as Button;
            var prefix = button.Tag.ToString();

            if (ensembleIndices[prefix] > 0)
            {
                if (prefix == "KQ" && ensembleIndices[prefix] == 2)
                {
                    ensembleIndices[prefix]--; // Skip "KQ1" when decrementing
                }
                ensembleIndices[prefix]--;
                UpdateEnsembleButtonText(prefix);
            }
        }

        private void GenreButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast the sender to a Button
            var button = sender as Button;
            if (button != null)
            {
                string genre = button.Content.ToString();

                // Split the existing text into lines to check for duplicates
                var genresInBox = GenreTextBox.Text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                // Check if the genre is already in the TextBox
                if (!genresInBox.Contains(genre))
                {
                    // If the TextBox already contains text, append a newline before the genre
                    if (!string.IsNullOrEmpty(GenreTextBox.Text))
                    {
                        GenreTextBox.Text += "\n";
                    }

                    // Append the button's content (genre) to the TextBox
                    GenreTextBox.Text += genre;
                }
            }
        }

        private void ClearAllFields()
        {
            // Clear TextBox fields
            AlbumNameTextBox.Text = string.Empty;
            TrackNameTextBox.Text = string.Empty;
            CustomIDTextBox.Text = string.Empty;
            TrackNumberTextBox.Text = string.Empty;
            PublicationYearTextBox.Text = string.Empty;
            CatalogueNumberTextBox.Text = string.Empty;
            ISRCTextBox.Text = string.Empty;
            RecordLabelTextBox.Text = string.Empty;
            LanguageTextBox.Text = string.Empty;
            EnsembleTextBox.Text = string.Empty;
            GenreTextBox.Text = string.Empty;

            // Reset ComboBox selections
            RecordingCountryComboBox.SelectedIndex = -1; // This deselects any selected item

            // Clear DataGrids
            Performers.Clear();
            Authors.Clear();

            // Uncheck CheckBoxes
            RecordingCountryEstimatedCheckBox.IsChecked = false;
            FinnishContentCheckBox.IsChecked = false;
            YleMasterCheckBox.IsChecked = false;

            // Reset ensemble indices to default states
            ResetEnsembleIndices();

            // Update the text on ensemble buttons
            InitializeEnsembleButtons();
        }

        private void ResetEnsembleIndices()
        {
            // Set default values for the ensemble indices
            ensembleIndices["KM"] = 1;
            ensembleIndices["KN"] = 1;
            ensembleIndices["KQ"] = 0; 
            ensembleIndices["KV"] = 0;
            ensembleIndices["KZ"] = 0;
            ensembleIndices["KS"] = 0;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearAllFields();
        }

        private static string ProcessIsrcValue(string value)
        {
            if (value.Contains(":"))
            {
                string[] parts = value.Split(':');
                value = parts[parts.Length - 1]; // take the last part after splitting by ':'
            }
            return value;
        }

        private void Window_DragOver(object sender, DragEventArgs e)
        {
            // Check if the drag content is a file
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy; // Show copy cursor for file drop
            }
            else if (e.Data.GetDataPresent(PerformerDataFormat) || e.Data.GetDataPresent(AuthorDataFormat))
            {
                // If the source of the drag event is internal (reordering DataGrid rows)
                e.Effects = DragDropEffects.Move; // Allow row reordering
            }
            else
            {
                e.Effects = DragDropEffects.None; // Show no-drop cursor for non-recognized items
            }
            e.Handled = true;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            // Check if the dropped item is a file
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Retrieve the file paths
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                // Process the first file if there is any
                if (files.Length > 0)
                {
                    string filePath = files[0];
                    HandleFileDrop(filePath);
                }
            }
        }


        private void HandleFileDrop(string droppedFilePath)
        {
            ClearAllFields();
            filePath = string.Empty;
            filePath = droppedFilePath;

            // Prepare the mediainfo command
            var startInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/mediainfo/MediaInfo.exe"),
                Arguments = $"--Full \"{droppedFilePath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8
            };

            // Run the command
            var process = Process.Start(startInfo);

            // Read the output of the command
            string output = process.StandardOutput.ReadToEnd();

            // Wait for the command to finish
            process.WaitForExit();

            string isrcOrVendorValue = null;

            // Iterate over each line in the output
            foreach (var line in output.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                {
                    // Split the line into field and value
                    var parts = line.Split(new[] { ':' }, 2, StringSplitOptions.None);

                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        switch (key)
                        {
                            case "Track name":
                                TrackNameTextBox.Text = ConvertToLowerCase(value);
                                break;
                            case "Performer":
                                var performerNames = value.Split(new[] { "&", ",", " and " }, StringSplitOptions.RemoveEmptyEntries)
                                                          .Select(p => p.Trim())
                                                          .Distinct() // This ensures unique names only
                                                          .ToList();

                                Performers.Clear(); // Clear existing entries to avoid duplicates

                                foreach (var performerName in performerNames)
                                {
                                    Performers.Add(new Performer { PerformerName = performerName });
                                }
                                break;
                            case "Track name/Position":
                                TrackNumberTextBox.Text = value;
                                break;
                            case "Composer":
                                var composerNames = value.Split(new[] { "&", ",", " and " }, StringSplitOptions.RemoveEmptyEntries)
                                                         .Select(composer => composer.Trim())
                                                         .Distinct() // This ensures unique names only
                                                         .ToList();

                                Authors.Clear(); // Clear existing entries to avoid duplicates

                                foreach (var composerName in composerNames)
                                {
                                    Authors.Add(new Author { AuthorName = composerName });
                                }
                                break;

                            case "Vendor":
                            case "ISRC":
                                if (isrcOrVendorValue == null)
                                {
                                    isrcOrVendorValue = ProcessIsrcValue(value);
                                    ISRCTextBox.Text = isrcOrVendorValue;
                                }
                                break;
                            case "Recorded date":
                                if (!string.IsNullOrWhiteSpace(value) && value.Length >= 4)
                                {
                                    PublicationYearTextBox.Text = value.Substring(0, 4);
                                }
                                break;
                            case "Album":
                                AlbumNameTextBox.Text = value;
                                break;
                            case "Copyright":
                                RecordLabelTextBox.Text = value;
                                break;
                        }

                        CatalogueNumberTextBox.Text = "Ei kataloginumeroa";
                    }
                }
            }
        }

        private string ConvertToLowerCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Normalize the string by converting it to lowercase.
            input = input.ToLower();

            // List of articles to check for at the beginning of the title
            var articles = new HashSet<string> { "the", "a", "an" };

            // Split the input into words
            var words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Check if the first word is an article
            if (words.Length > 1 && articles.Contains(words[0]))
            {
                // Remove the first word (article) and rebuild the string without the article at the beginning
                string titleWithoutArticle = string.Join(" ", words.Skip(1));

                // Capitalize the first letter of the main title and append the article at the end with proper capitalization
                input = char.ToUpper(titleWithoutArticle[0]) + titleWithoutArticle.Substring(1) + ", " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words[0]);
            }
            else
            {
                // If no article or only one word, just capitalize the first letter
                input = char.ToUpper(input[0]) + input.Substring(1);
            }

            return input;
        }



        // ------JSON creation section------

        private JObject LoadJsonTemplate()
        {
            // Define the path to the template JSON file
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "template.json");

            // Read the file into a string
            string jsonText = File.ReadAllText(filePath);

            // Parse the string into a JObject
            JObject jsonObject = JObject.Parse(jsonText);

            return jsonObject;
        }

        private void UpdateJsonTemplate(JObject jsonTemplate)
        {
            string customId = CustomIDTextBox.Text;
            string trackNumberRaw = TrackNumberTextBox.Text;

            // Format track number with leading zeros only for concatenation with CustomID in the custom section
            string trackNumberFormatted = int.TryParse(trackNumberRaw, out int num) ? num.ToString("D2") : "01";

            // Concatenate customId with formatted track number for the custom section
            string formattedCustomId = $"{customId}-{trackNumberFormatted}";

            // Get the value of the FinnishContentCheckBox - false by default
            bool isFinnishContent = FinnishContentCheckBox.IsChecked ?? false;

            // Get the value of the YleMasterCheckBox - false by default
            bool isYleMaster = YleMasterCheckBox.IsChecked ?? false;

            // Get values from LanguageTextBox, turn them into an array and trim it
            string[] languages = LanguageTextBox.Text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                             .Select(lang => lang.Trim())  // Trim any leading or trailing whitespace
                                             .Where(lang => !string.IsNullOrWhiteSpace(lang))  // Ensure no empty strings are added
                                             .ToArray();
            JArray languageArray = new JArray(languages);

            // Get values from GenreTextBox, turn them into an array and get full descriptive values from the dictionary
            string[] genres = GenreTextBox.Text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var translatedGenres = genres.Select(TranslateGenre).ToList();
            JArray genreArray = new JArray(translatedGenres);

            // Get values from EnsembleTextBox, turn them into an array and get full descriptive values from the dictionary
            string[] ensembles = EnsembleTextBox.Text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var translatedEnsembles = ensembles.Select(TranslateEnsemble).ToList();
            JArray ensembleArray = new JArray(translatedEnsembles);

            // Get the value of the RecordingCountryEstimatedCheckBox - false by default
            bool isRecordingCountryEstimated = RecordingCountryEstimatedCheckBox.IsChecked ?? false;

            // Get the value of the RecordingCountryComboBox
            string selectedCountry = "";
            if (RecordingCountryComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                selectedCountry = selectedItem.Content.ToString();
            }

            // Get and format the value of the AlbumNameTextBox
            string formattedAlbumTitle = string.IsNullOrWhiteSpace(AlbumNameTextBox.Text) ? " " : AlbumNameTextBox.Text.Trim();

            // Update JSON template: Track section
            jsonTemplate["tracks"][0]["track_number"] = trackNumberRaw;  // Track number
            jsonTemplate["tracks"][0]["title"] = TrackNameTextBox.Text;  // Track title
            jsonTemplate["tracks"][0]["isrc"] = ISRCTextBox.Text;  // ISRC
            jsonTemplate["tracks"][0]["custom"]["finnish_content"] = isFinnishContent ? "yes" : "no"; // Finnish content - Fennica
            jsonTemplate["tracks"][0]["custom"]["yle_master"] = isYleMaster ? "yes" : "no"; // Yle master
            jsonTemplate["tracks"][0]["custom"]["CustomID"] = formattedCustomId; // Track customID
            jsonTemplate["tracks"][0]["custom"]["languages"] = languageArray; // Language
            jsonTemplate["tracks"][0]["custom"]["genre"] = genreArray; // Track genre
            jsonTemplate["tracks"][0]["custom"]["ensemble"] = ensembleArray; // Ensemble
            jsonTemplate["tracks"][0]["custom"]["publication_year"] = PublicationYearTextBox.Text; // Publication year
            jsonTemplate["tracks"][0]["custom"]["theme"] = KeywordTextBox.Text; // Theme - Keywords
            jsonTemplate["tracks"][0]["custom"]["origin"] = KulttuuriTextBox.Text; // Origin - Culture
            jsonTemplate["tracks"][0]["custom"]["recording_country"] = selectedCountry; // Recording country
            jsonTemplate["tracks"][0]["custom"]["recording_country_estimated"] = isRecordingCountryEstimated ? "yes" : "no"; // Recording country estimated
            UpdateArtistMasterOwnerships(jsonTemplate); // Track artist/performer
            UpdatePublishingArtistOwnerships(jsonTemplate); // Author

            // Update JSON template: Album section
            jsonTemplate["tracks"][0]["album"]["custom"]["CustomID"] = customId;  // Album customID
            jsonTemplate["tracks"][0]["album"]["title"] = formattedAlbumTitle; // Album title
            jsonTemplate["tracks"][0]["album"]["album_ref"] = CatalogueNumberTextBox.Text; // Album reference - Catalogue number
            jsonTemplate["tracks"][0]["album"]["custom"]["finnish_content"] = isFinnishContent ? "yes" : "no"; // Finnish content - Fennica
            jsonTemplate["tracks"][0]["album"]["custom"]["yle_master"] = isYleMaster ? "yes" : "no"; // Yle master
            jsonTemplate["tracks"][0]["album"]["custom"]["recording_country"] = selectedCountry; // Recording country
            jsonTemplate["tracks"][0]["album"]["custom"]["recording_country_estimated"] = isRecordingCountryEstimated ? "yes" : "no"; // Recording country estimated
            jsonTemplate["tracks"][0]["album"]["custom"]["publication_year"] = PublicationYearTextBox.Text; // Publication year
            jsonTemplate["tracks"][0]["album"]["custom"]["WorkflowStatus"] = "Täydet_tiedot"; // Workflow status
            jsonTemplate["tracks"][0]["album"]["master_ownerships"][0]["label"]["label_name"] = RecordLabelTextBox.Text; // Record label
            UpdateAlbumArtistMasterOwnerships(jsonTemplate); // Album artist/performer
            UpdateAlbumGenres(jsonTemplate, genres); // Album genre
        }

        private string TranslateEnsemble(string ensembleCode)
        {
            return CustomDescriptionDictionaries.GetEnsembleDescription(ensembleCode);
        }

        private string TranslateGenre(string genreCode)
        {
            return CustomDescriptionDictionaries.GetGenreDescription(genreCode);
        }

        private string GetBaseGenre(string detailedGenreCode)
        {
            return detailedGenreCode.Length > 2 ? detailedGenreCode.Substring(0, 2) : detailedGenreCode;
        }

        private void UpdateAlbumGenres(JObject jsonTemplate, IEnumerable<string> detailedGenres)
        {
            // Extract base genres and ensure uniqueness
            var baseGenres = new HashSet<string>();

            foreach (var detailedGenre in detailedGenres)
            {
                // Check for special cases and handle them differently
                if (detailedGenre == "KMUS" || detailedGenre == "VMUS")
                {
                    // Add the full description directly for these special cases
                    baseGenres.Add(TranslateGenre(detailedGenre));
                }
                else
                {
                    // For other cases, use the base genre
                    baseGenres.Add(TranslateGenre(GetBaseGenre(detailedGenre)));
                }
            }

            // Access the existing genre array in the album custom section or initialize it if it doesn't exist
            JArray genreArray = (JArray)jsonTemplate.SelectToken("tracks[0].album.custom.genre") ?? new JArray();
            genreArray.Clear();  // Clear the existing genres to avoid duplicates or incorrect data

            // Populate the genre array with new entries
            foreach (var genre in baseGenres)
            {
                genreArray.Add(genre);  // Assuming you just want to add the translated genre directly as strings
            }

            // Make sure the updated genre array is assigned back if it was newly created
            jsonTemplate["tracks"][0]["album"]["custom"]["genre"] = genreArray;
        }



        private void SaveJsonToFile(JObject jsonTemplate)
        {
            // Set up a save file dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FileName = "output.json"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                // Write the updated JSON object to the selected file
                File.WriteAllText(saveFileDialog.FileName, jsonTemplate.ToString());
            }
        }

        private void UpdateArtistMasterOwnerships(JObject jsonTemplate)
        {
            JArray artistArray = new JArray();

            foreach (Performer performer in Performers)
            {
                if (string.IsNullOrWhiteSpace(performer.PerformerName))
                    continue;  // Skip entries with empty or null names

                JObject artist = new JObject(
                    new JProperty("artist", new JObject(
                        new JProperty("full_name", performer.PerformerName.Trim())
                    )),
                    new JProperty("rights_type", new JObject(
                        new JProperty("key", "musician"),
                        new JProperty("type", "master-artist")
                    ))
                );

                JArray instrumentsArray = new JArray();

                // Check if there are instruments and split by comma
                if (!string.IsNullOrWhiteSpace(performer.PerformerInstrument))
                {
                    string[] instruments = performer.PerformerInstrument.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var instrument in instruments)
                    {
                        string trimmedInstrument = instrument.Trim();
                        instrumentsArray.Add(new JObject(
                            new JProperty("key", trimmedInstrument),
                            new JProperty("names", new JArray(
                                new JObject(
                                    new JProperty("value", trimmedInstrument)
                                )
                            ))
                        ));
                    }
                }

                artist["instruments"] = instrumentsArray;
                artistArray.Add(artist);
            }

            jsonTemplate["tracks"][0]["artists_master_ownerships"] = artistArray;
        }




        private void UpdateAlbumArtistMasterOwnerships(JObject jsonTemplate)
        {
            JArray artistArray = new JArray();

            // Iterate through each performer in the ObservableCollection
            foreach (Performer performer in Performers)
            {
                if (string.IsNullOrWhiteSpace(performer.PerformerName))
                {
                    continue;  // Skip adding entries with empty or null names
                }

                // Create the artist JSON object for the album section
                JObject artist = new JObject(
                    new JProperty("artist", new JObject(
                        new JProperty("full_name", performer.PerformerName.Trim())
                    )),
                    new JProperty("rights_type", new JObject(
                        new JProperty("key", "musician"),
                        new JProperty("type", "master-artist")
                    )),
                    new JProperty("instruments", new JArray())  // Empty array for instruments
                );

                artistArray.Add(artist);
            }

            // Assign the created array of artist objects to the JSON structure for the album section
            jsonTemplate["tracks"][0]["album"]["artists_master_ownerships"] = artistArray;
        }

        private void UpdatePublishingArtistOwnerships(JObject jsonTemplate)
        {
            JArray publishingArtistArray = new JArray();

            foreach (Author author in Authors)
            {
                if (string.IsNullOrWhiteSpace(author.AuthorName))
                    continue;  // Skip empty or null names

                var (roleKey, additionalNotes) = DeterminePublishingRoleKey(author.AuthorRole);

                JObject publishingArtist = new JObject(
                    new JProperty("artist", new JObject(
                        new JProperty("full_name", author.AuthorName.Trim())
                    )),
                    new JProperty("rights_type", new JObject(
                        new JProperty("key", roleKey),
                        new JProperty("type", "publishing-artist")
                    ))
                );

                if (!string.IsNullOrWhiteSpace(additionalNotes))
                {
                    publishingArtist["additional_notes"] = additionalNotes;  // Add additional notes if there are any
                }

                publishingArtistArray.Add(publishingArtist);
            }

            jsonTemplate["tracks"][0]["artists_publishing_ownerships"] = publishingArtistArray;
        }


        private (string roleKey, string additionalNotes) DeterminePublishingRoleKey(string authorRole)
        {
            string trimmedRole = string.IsNullOrWhiteSpace(authorRole) ? "" : authorRole.Trim().ToLower();
            switch (trimmedRole)
            {
                case "säv": return ("composer", "");
                case "sov": return ("arranger", "");
                case "san": return ("lyricist", "");
                default: return ("author", trimmedRole);  // Return 'author' and keep the original role as additional notes
            }
        }

        private bool ValidateInput()
        {
            // Clear previous error messages
            CustomIDErrorMessageTextBlock.Text = "";

            // Validate CustomID
            Regex customIDRegex = new Regex(@"^DIGI-\d+$");
            if (!customIDRegex.IsMatch(CustomIDTextBox.Text))
            {
                CustomIDErrorMessageTextBlock.Text = "Kartuntatunnuksen täytyy olla muotoa DIGI-12345";
                return false;
            }

            // If all validations pass
            return true;
        }

        private void CreateJSONButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return; // Stop processing if the input is not valid
            }

            try
            {
                // Load the template JSON
                JObject jsonTemplate = LoadJsonTemplate();

                // Update the template with data from the form
                UpdateJsonTemplate(jsonTemplate);

                // Attempt to save the updated JSON
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                    FileName = "output.json"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, jsonTemplate.ToString());
                    MessageBox.Show("JSON luotu!");
                    CustomIDErrorMessageTextBlock.Text = ""; // Clear the error message if all goes well
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
