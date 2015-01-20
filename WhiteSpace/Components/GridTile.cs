using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.Temp;
using Microsoft.Xna.Framework;

namespace WhiteSpace.Components
{
    public class GridTile : StandardComponent
    {
        public int x, y;

        public GridTile()
        {

        }

        public GridTile(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
