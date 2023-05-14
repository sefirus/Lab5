using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Solver;

namespace Gui;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    private ObservableCollection<Person> _persons = new ObservableCollection<Person>();
    private Person? _selectedPerson;

    public ObservableCollection<Person> Persons
    {
        get => _persons;
        set
        {
            _persons = value;
            OnPropertyChanged();
        }
    }

    public Person? SelectedPerson
    {
        get => _selectedPerson;
        set
        {
            _selectedPerson = value;
            OnPropertyChanged();
        }
    }

    public MainWindow()
    {
        Persons = new ObservableCollection<Person>();
        InitializeComponent();
        DataContext = this;
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        var newPerson = new Person { Title = "Нова", Type = NameType.Surname };
        Persons.Add(newPerson);
        PersonListView.SelectedItem = newPerson;
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedPerson == null)
        {
            return;
        }
        Persons.Remove(SelectedPerson);
        SelectedPerson = null;
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        OnPropertyChanged();
        if (SelectedPerson == null)
        {
            return;
        }
        int.TryParse(AgeTextBox.Text, out int age);
        SelectedPerson.Title = TitleTextBox.Text;
        SelectedPerson.Type = (NameType)TypeComboBox.SelectedValue;
        SelectedPerson.HasSisters = HasSistersCheckBox.IsChecked;
        SelectedPerson.Age = age;
        SelectedPerson.OlderThan = OlderThanComboBox.SelectionBoxItem as Person;
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedPerson == null)
        {
            return;
        }
        TitleTextBox.Text = SelectedPerson.Title;
        TypeComboBox.SelectedValue = SelectedPerson.Type;
        HasSistersCheckBox.IsChecked = SelectedPerson.HasSisters;
        AgeTextBox.Text = SelectedPerson.Age.ToString();
        OlderThanComboBox.SelectedValue = SelectedPerson.OlderThan;
    }

    private void SolveButton_Click(object sender, RoutedEventArgs e)
    { 
        var matches = new MatchingStrategy()
            .SetSurnames(Persons.Where(p => p.Type == NameType.Surname))
            .SetProfessions(Persons.Where(p => p.Type == NameType.Profession))
            .GetMatches()
            .ToList();

        var resultsWindow = new ResultsWindow(matches);
        resultsWindow.ShowDialog();
        
    }

    private void PersonListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (PersonListView.SelectedItem is not Person selectedPerson)
        {
            SelectedPerson = null;
            return;
        }
        SelectedPerson = selectedPerson;
        TitleTextBox.Text = SelectedPerson.Title;
        TypeComboBox.SelectedValue = SelectedPerson.Type;
        HasSistersCheckBox.IsChecked = SelectedPerson.HasSisters;
        AgeTextBox.Text = SelectedPerson.Age.ToString();
        if (SelectedPerson.OlderThan != null)
        {
            OlderThanComboBox.SelectedItem = SelectedPerson.OlderThan;
        }
        else
        {
            OlderThanComboBox.SelectedItem = null;
        }
    }

    private void SeedButton_Click(object sender, RoutedEventArgs e)
    {
        foreach (var person in Seed.GetDefaults())
        {
            Persons.Add(person);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}