using System.Collections.Generic;

/// <summary>
/// Represetns a avaliator of states of chess.
/// </summary>
public abstract class Major
{
    public abstract double Predict(State state);
    public abstract IEnumerable<double> Predict(IEnumerable<State> states);
}