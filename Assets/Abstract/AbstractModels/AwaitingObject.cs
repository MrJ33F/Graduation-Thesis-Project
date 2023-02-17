namespace MapGenerator.BaseModels
{
    /// <summary>
    /// Date despre obiectul care asteapta/urmeaza a fi generat.
    /// </summary>
    public class AwaitingObject : AbstractObjectModel
    {
        public Vector2Float Position { get; set; }
        public float Scale { get; set; } = 1;
    }
}