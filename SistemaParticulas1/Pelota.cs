using System.Drawing;

class Pelota
{
    public Size Size { get; set; }
    public Point Location { get; set; }
    public double Rebote { get; set; }
    public int MoveStepX { get; set; }
    public int MoveStepY { get; set; }
    public int SpeedX { get { return MoveStepX; } }
    public int SpeedY { get { return MoveStepY; } }
    public double Mass { get { return Size.Width * Size.Height; } }
    public bool life { get; set; }
    public Color colorP { get; set; }
    public Pelota(Size size, Point location)
    {
        this.Size = size;
        this.Location = location;
    }

    public bool IntersectsWith(Pelota otherPelota)
    {
        Rectangle rect1 = new Rectangle(Location, Size);
        Rectangle rect2 = new Rectangle(otherPelota.Location, otherPelota.Size);
        return rect1.IntersectsWith(rect2);
    }

    public void Update()
    {
        // No hay necesidad de actualizar la pelota en este momento
    }
}
