namespace Solver;

public class Match
{
    public Person Surname { get; init; }
    public Person Proffesion { get; set; }

    public override string ToString()
    {
        return $"{Surname.Title}: {Proffesion.Title}";
    }
}