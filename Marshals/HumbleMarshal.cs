using System.Linq;

public class HumbleMarshal : Marshal
{
    public HumbleMarshal(Major major)
    {
        this.Major = major;
    }

    public Major Major { get; private set; }

    public override State Play(State state, bool whiteplays)
    {
        var next = state.Next(whiteplays);
        double max = -1;
        State selected = State.Empty;
        foreach ((State st, double pr) in next.Zip(this.Major.Predict(next)))
        {
            if (pr > max)
            {
                selected = st;
                max = pr;
            }
        }
        return selected;
    }
}