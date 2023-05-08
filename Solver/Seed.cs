namespace Solver;

public static class Seed
{
    public static List<Person> GetDefaults()
    {
        var locksmith = new Person()
        {
            Title = "Locksmith",
            Type = NameType.Profession,
            HasSisters = false,
            Age = 1
        };

        var turner = new Person()
        {
            Title = "Turner",
            Type = NameType.Profession
        };

        var welder = new Person()
        {
            Title = "Welder",
            Type = NameType.Profession
        };

        var borusenko = new Person()
        {
            Title = "Borusenko",
            Type = NameType.Surname,
            HasSisters = true
        };

        var semkiv = new Person()
        {
            Title = "Semkiv",
            Type = NameType.Surname,
            OlderThan = turner
        };

        var ivanov = new Person()
        {
            Title = "Ivanov",
            Type = NameType.Surname
        };
        return new List<Person>() { locksmith, turner, welder, ivanov, borusenko, semkiv };
    }
}