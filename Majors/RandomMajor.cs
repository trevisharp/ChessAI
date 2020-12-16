using System;
using System.Linq;
using System.Collections.Generic;

public class RandomMajor : Major
{
    private Random rand = new Random(DateTime.Now.Millisecond);

    public override double Predict(State state)
        => rand.NextDouble();

    public override IEnumerable<double> Predict(IEnumerable<State> states)
        => states.Select(s => rand.NextDouble());
}