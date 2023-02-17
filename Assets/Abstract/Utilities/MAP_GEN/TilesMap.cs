public class TilesMap
{
    public int Width { get; set; }
    public int Height { get; set; }

    public Tile[,] Map {get;}

    public TilesMap(int width, int height){
        Width = width;
        Height = height;

        Map = new Tile[width, height];

        for(int i = 0; i < height; i++){
            for(int j = 0; j < width; j++){
                Map[i, j] = new Tile();
            }
        }
    }
}