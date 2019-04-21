﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Arcanoid
{
    public class MetalBlock : Block
    {


        public MetalBlock(int x, int y, int width, int height) : base(x, y, width, height)
        {
            Health = 2;
        }

        public override void Draw(Graphics gr)
        {
            SolidBrush brush = new SolidBrush(Color.Brown);
            Image image = Properties.Resources.Metal;
            Rectangle rect = new Rectangle(Position.X, Position.Y, Width, Height);
            gr.DrawImage(image, rect);
        }
    }
}
