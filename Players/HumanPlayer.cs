using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// TODO: Implements play limitation
/// </summary>
public class HumanPlayer : Player
{
    int sx = -1;
    int sy = -1;
    int ex = -1;
    int ey = -1;

    public override async Task<State> Play(State state, bool whiteplay)
    {
        State newstate = State.Empty;
        while (newstate == State.Empty)
        while (sx == -1 ||
            (whiteplay && state[sx, sy].IsBlack()) ||
            (!whiteplay && state[sx, sy].IsWhite()))
            await Task.Delay(100);
        newstate = state.MoveCertify(sx, sy, ex, ey);



        sx = sy = ex = ey = -1;
        return newstate;
    }

    public void RegisterMove(int sx, int sy, int ex, int ey)
    {
        this.sx = sx;
        this.sy = sy;
        this.ex = ex;
        this.ey = ey;
    }
}