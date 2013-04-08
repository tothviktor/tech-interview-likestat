namespace Proctology.Interview.CSharp.LikeStatistics
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.TypeSelection.Items.Add("xml");
            this.TypeSelection.Items.Add("bin");
            this.TypeSelection.Items.Add("csv");

            this.TypeSelection.SelectedIndex = 0;
            this.DatabasePath.Text = ConfigurationManager.AppSettings["DatabasePath"];
        } // MainWindow()

        private void UpdateScreen(IEnumerable<PersonNameWithLikeCount> firstTen, long p)
        {
            this.TopLoved.ItemsSource = firstTen;
            this.ProcessingTime.Content = string.Format("Processed in {0}ms", p);
        } // Button_Click

        private string GetSelectedSourceType()
        {
            return this.TypeSelection.SelectedItem as string;
        } // GetSelectedSourceType()  

        private string GetDatabasePath()
        {
            return this.DatabasePath.Text;
        } // GetDatabasePath()
    } // class MainWindow
} // namespace Proctology.Interview.CSharp.LikeStatistics
