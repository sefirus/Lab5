namespace Lab5Draft;

public class Person
{
    public NameType Type { get; set; }
    public string Title { get; set; }
    public bool ?HasSisters { get; set; }
    private int? _age;
    public int? Age
    {
        get => _age;
        set => _age = value is >= 1 and <= 3 
            ? value 
            : throw new ArgumentException();
    }
    public Person? OlderThan { get; set; }
    public bool IsMatched { get; set; }
}