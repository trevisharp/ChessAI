using System.Threading.Tasks;

/// <summary>
/// Represents a Chess Player
/// </summary>
public abstract class Player
{
    public abstract Task<State> Play(State state, bool whiteplay);
}