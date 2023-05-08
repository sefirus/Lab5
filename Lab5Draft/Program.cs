using Solver;

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

new MatchingStrategy()
    .SetSurnames(new []{borusenko, ivanov, semkiv})
    .SetProfessions(new []{locksmith, turner, welder})
    .GetMatches()
    .ToList()
    .ForEach(Console.WriteLine);
    
    //Borusenko: Turner
    //Semkiv: Welder
    //Ivanov: Locksmith

