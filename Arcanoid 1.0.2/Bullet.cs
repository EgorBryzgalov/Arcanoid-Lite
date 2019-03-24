﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Arcanoid_1._0._2
{
    public class Bullet 
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        Settings BulletSettings { get; set; }
        public bool XSpeedCorrected { get; set; }
        public bool YSpeedCorrected { get; set; }
        public int Width { get; set; }
        public int Heigh { get; set; }

        public Bullet(Settings set)
        {
            BulletSettings = set;
            SpeedY = 0;
            SpeedX = 0;
            Width = set.GetBlockSize() / 2;
            Heigh = set.GetBlockSize() / 2;
            PosY = set.FormHeight - 20 - 3 - Heigh-50; ;
            PosX = set.FormWidth / 2 - Width/2;
        }
        
        public void Start()
        {
            SpeedX = BulletSettings.Speed;
            SpeedY = -BulletSettings.Speed;
        }
        public void Move()
        {
            PosX += SpeedX;
            PosY += SpeedY;
            XSpeedCorrected = false;
            YSpeedCorrected = false;
        }
        public void CheckBorder()
        {
            if (PosX >= (BulletSettings.FormWidth-Width))
            {
                PosX = BulletSettings.FormWidth - (PosX - BulletSettings.FormWidth);
                SpeedX = -SpeedX;
            }

            if (PosX <= 0)
            {
                PosX = -PosX;
                SpeedX = -SpeedX;
            }
            if (PosY <= 0)
            {
                PosY = -PosY;
                SpeedY = -SpeedY;
            }

        }
        public Point GetCenter()
        {
            Point center=new Point();
            center.X = PosX + BulletSettings.GetBlockSize()/4;
            center.Y = PosY + BulletSettings.GetBlockSize()/4;
            return center;
        }
        public void Draw(Graphics Gr)
        {
            SolidBrush brush = new SolidBrush(Color.Blue);
            Gr.FillRectangle(brush, new Rectangle(this.PosX, this.PosY, BulletSettings.GetBlockSize()/2, BulletSettings.GetBlockSize()/2));
        }
        public bool CheckFault(bool platform)
        {
            if ((PosY>=BulletSettings.FormHeight-Heigh)&&(platform == false)) { return true; }
            else { return false; }
        }

    }

}
