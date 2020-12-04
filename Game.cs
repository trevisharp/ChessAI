using System.Threading.Tasks;

/// <summary>
/// TODO: Implements Draw
/// </summary>
public class Game
{
    public State Current { get; set; } = State.Classic;
    public Player WhitePlayer { get; set; }
    public Player BlackPlayer { get; set; }
    public bool WhitePlays { get; set; } = true;
    public bool WhiteWin { get; set; } = false;
    public bool BlackWin { get; set; } = false;

    /// <summary>
    /// Método assíncrono que realiza uma jogada do proxímo jogador.
    /// Retorna verdadeiro no caso de o jogo ainda deva continuar.
    /// </summary>
    public async Task<bool> Play()
    {
        if (WhitePlays)
        {
            Current = await WhitePlayer.Play(Current, true);
            if (Current == State.Empty)
            {
                BlackWin = true;
                return false;
            }
        }
        else
        {
            Current = await BlackPlayer.Play(Current, false);
            if (Current == State.Empty)
            {
                WhiteWin = true;
                return false;
            }
        }
        WhitePlays = !WhitePlays;
        return true;
    }
}