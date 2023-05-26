public class Senario
{
    public STATE senarioValue;
    public TileStatus targettile;
    public TileStatus myPostile;
    public Senario(STATE senarioValue, TileStatus targettile, TileStatus myPostile)
    {
        this.senarioValue = senarioValue;
        this.targettile = targettile;
        this.myPostile = myPostile;
    }

    public Senario()
    {
        this.senarioValue = STATE.NONE;
        this.targettile = null;
        this.myPostile = null;
    }


}
