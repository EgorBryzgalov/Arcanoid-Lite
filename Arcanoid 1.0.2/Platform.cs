﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Arcanoid
{
    class Platform : ICollision
    {//написать методы движения и отрисовки(учесть границы формы)
        public int PosX { get; set; }
        public int PosY { get; private set; }
        Settings PlatformSettings { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        int Speed { get; set; }

        public Platform()
        {

        }
        public Platform (int x, int y, int width, int height)
        {
            PosX = x;
            PosY = y;
            Width = width;
            Height = height;

        }

        public Platform(Settings set)
        {
            PlatformSettings = set;
            PosY = set.FormHeight - Height-3-50;
            PosX = set.FormWidth / 2 - Width / 2;
            Width = 150;
            Height = 20;
            Speed = set.Speed*4;
            
        }

        public CExtends GetExtends()
        {
            CExtends ext = new CExtends(PosX, PosY, Width, Height);
            return ext;
        }
        public void MoveRight()
        {
           if (PosX<=PlatformSettings.FormWidth-Width) PosX += Speed;
        }
        public void MoveLeft()
        {
           if (PosX>=0) PosX -= Speed;
        }
        public void Draw(Graphics Gr)
        {
            SolidBrush brush = new SolidBrush(Color.Blue);
            Gr.FillRectangle(brush, new Rectangle(PosX, PosY, Width, Height));
        }

        public bool CheckCollision(ref Bullet bul)
        {
            if ((bul.PosX >= PosX-bul.Width) && (bul.PosX <= (PosX + Width)) && (bul.PosY >= (PosY - bul.Height)))
            {
                bul.SpeedY = -bul.SpeedY;
                return true;
            }
            else return false;
        }

        
    }
}
