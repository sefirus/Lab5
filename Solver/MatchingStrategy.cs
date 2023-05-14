namespace Solver;

public class MatchingStrategy : ISolutionStrategy
{
    private List<Person> Surnames { get; set; }
    private List<Person> Professions { get; set; }
    
    public ISolutionStrategy SetSurnames(IEnumerable<Person> surnames)
    {
        Surnames = surnames.Where(p => p.Type == NameType.Surname).ToList();
        return this;
    }

    public ISolutionStrategy SetProfessions(IEnumerable<Person> professions)
    {
        Professions = professions.Where(p => p.Type == NameType.Profession).ToList();
        return this;
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
        if (Surnames is null || Professions is null || Surnames.Count != Professions.Count)
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
