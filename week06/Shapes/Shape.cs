// Shape.cs
public class Shape
{
    private string _color;

    public Shape(string color)
    {
        _color = color;
    }

    public string GetColor()
    {
        return _color;
    }

    // Virtual method to be overridden by child classes
    public virtual double GetArea()
    {
        return 0;
    }
}
