using System.Drawing;

namespace NetML
{
    public interface IDrawable
    {
        void Draw(Graphics g);
        Rectangle DrawableBounds();
        Rectangle CollisionBounds();
    }
}
