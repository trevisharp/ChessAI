using System.Threading.Tasks;

/// <summary>
/// Represents a Artificial Inteligence Player
/// </summary>
public class AIPlayer : Player
{
    public Marshal Marshal { get; set; }

    public override async Task<State> Play(State state, bool whiteplay)
        => await Task.Run(() => this.Marshal.Play(state, whiteplay));
}