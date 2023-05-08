namespace Solver;

public interface ISolutionStrategy
{
    ISet<Match> GetMatches(int maxIterations = 100);
    ISolutionStrategy SetSurnames(IEnumerable<Person> surnames);
    ISolutionStrategy SetProfessions(IEnumerable<Person> professions);
}
