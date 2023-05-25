public class Senario
{
    public float senarioValue;
    public TileStatus targettile;
    public TileStatus myPostile;

    public Senario(float senarioValue, TileStatus targettile, TileStatus myPostile)
    {
        this.senarioValue = senarioValue;
        this.targettile = targettile;
        this.myPostile = myPostile;
    }

    public Senario()
    {
        this.senarioValue = -1000000;
        this.targettile = null;
        this.myPostile = null;
    }


}
