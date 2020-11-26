using System.Threading.Tasks;

public abstract class Player
{
    public abstract Task<State> Play(State state, bool whiteplay);
}