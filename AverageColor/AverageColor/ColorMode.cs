using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AverageColor
{
    public class ColorMode
    {
        public Color color = new Color();
        public List<Color> colorList = new List<Color>();
        public int mode = 0;

        public ColorMode(Color _color)
        {
            color = _color;
        }
        public ColorMode(Color _color, Color listColor)
        {
            color = _color;
            colorList.Add(listColor);
        }

        public ColorMode(Color _color, int _mode)
        {
            color = _color;
            mode = _mode;
        }

        public int CompareByMode(int x, int y)
        {
            return (x - y) / Math.Abs(x - y);
        }
    }
}
