using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Solver;

using System.ComponentModel;

public class Person : INotifyPropertyChanged
{
    private string _title;
    private NameType _type;
    private bool? _hasSisters;
    private int _age;
    private Person _olderThan;
    public bool IsMatched { get; set; }

    public string Title
    {
        get { return _title; }
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    public NameType Type
    {
        get { return _type; }
        set
        {
            _type = value;
            OnPropertyChanged(nameof(Type));
        }
    }

    public bool? HasSisters
    {
        get { return _hasSisters; }
        set
        {
            _hasSisters = value;
            OnPropertyChanged(nameof(HasSisters));
        }
    }

    public int Age
    {
        get { return _age; }
        set
        {
            _age = value;
            OnPropertyChanged(nameof(Age));
        }
    }

    public Person OlderThan
    {
        get { return _olderThan; }
        set
        {
            _olderThan = value;
            OnPropertyChanged(nameof(OlderThan));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
