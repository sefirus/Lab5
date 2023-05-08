using System.Collections.Generic;
using System.Windows;
using Solver;

namespace Gui;

public partial class ResultsWindow : Window
{
    public ResultsWindow()
    {
        InitializeComponent();
    }

    public ResultsWindow(List<Match> matches) : this()
    {
        ResultsListBox.ItemsSource = matches;
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}
