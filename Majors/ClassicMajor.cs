using System.Collections.Generic;

public class ClassicMajor : Major
{
    public override double Predict(State state)
        => state.WhitePower / (state.WhitePower / state.BlackPower);

    public override IEnumerable<double> Predict(IEnumerable<State> states)
    {
        foreach (var state in states)
            yield return state.WhitePower / (state.WhitePower / state.BlackPower);
    }
}