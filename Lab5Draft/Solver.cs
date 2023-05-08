namespace Lab5Draft;

public class Solver
{
    public List<Person> Surnames { get; set; }
    public List<Person> Professions { get; set; }

    public Solver(IEnumerable<Person> surnames, IEnumerable<Person> professions)
    {
        Surnames = surnames.Where(p => p.Type == NameType.Surname).ToList();
        Professions = professions.Where(p => p.Type == NameType.Profession).ToList();
    }

    private List<Person> SortByProperties(IEnumerable<Person> source)
    {
        var sortedPeople = source
            .Select(p => new
                {
                    Person = p, 
                    AssignedPropertiesCount =  new object?[]
                    {
                        p.Type, p.Title, p.HasSisters, p.Age, p.OlderThan
                    }.Count(x => x != null)
                })
            .OrderByDescending(p => p.AssignedPropertiesCount)
            .Select(p => p.Person)
            .ToList();
        return sortedPeople;
    }

    private bool AreMatch(Person profession, Person surname)
    {
        if (surname.HasSisters != profession.HasSisters
            && surname.HasSisters.HasValue
            && profession.HasSisters.HasValue)
        {
            return false;
        }

        if (surname.Age == 1 && profession.OlderThan != null
            || surname.OlderThan != null && profession.Age == 1
            || surname.OlderThan == profession
            || profession.OlderThan == surname)
        {
            return false;
        }

        return true;
    }

    public ISet<Match> GetMatches(int maxIterations = 100)
    {
        if (Surnames.Count != Professions.Count)
        {
            throw new InvalidOperationException("Lists must be equal length");
        }
        var matches = new HashSet<Match>();
        var sortedSurnames = SortByProperties(Surnames.Where(p => !p.IsMatched));
        var sortedProfessions = SortByProperties(Professions.Where(p => !p.IsMatched));
        var k = 0;
        while (sortedSurnames.Count > 0 
            && sortedProfessions.Count > 0 
            && k <= maxIterations)
        {
            for (var i = 0; i < sortedSurnames.Count; i++)
            {
                for (var j = 0; j < sortedProfessions.Count; j++)
                {
                    var surname = sortedSurnames[i];
                    var profession = sortedProfessions[j];
                    if (!AreMatch(profession, surname))
                        continue;
                    sortedSurnames.RemoveAt(i);
                    sortedProfessions.RemoveAt(j);
                    surname.IsMatched = true;
                    profession.IsMatched = true;
                    matches.Add(new Match()
                    {
                        Proffesion = profession,
                        Surname = surname
                    });
                    i--; // Decrement i since we just removed an element
                    break;
                }
            }

            k++;
        }

        if (matches.Count != Surnames.Count)
        {
            throw new InvalidOperationException("Cant find matches");
        }

        return matches;
    }
}