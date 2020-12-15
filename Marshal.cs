/// <summary>
/// Represents a decider of the best plays.
/// </summary>
public abstract class Marshal
{
    public abstract State Play(State state, bool whiteplays);
}