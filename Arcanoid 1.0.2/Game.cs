﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Arcanoid
{
    class Game
    {
        private List<Block> blocks;
        private Platform platform;
        private Bullet bullet;
        private int Speed { get; set; }
        private int FormWidth { get; set; }
        private int FormHeight { get; set; }
        private bool GameStarted { get; set; }
        private bool Pause;
        private bool Following;

    

        public Game()
        {

        }

        public Game(Settings set)
        {
            FormWidth = set.FormWidth;
            FormHeight = set.FormHeight;
            Speed = set.Speed;
            platform = new Platform(set.FormWidth/2-2*set.GetBlockSize(), set.FormHeight-set.GetBlockSize()/2-50, set.GetBlockSize()*4, set.GetBlockSize()/2, Speed*4);
            bullet = new Bullet(platform.PosX + platform.Width / 2 - set.GetBlockSize() / 4, platform.PosY - set.GetBlockSize() / 2, set.GetBlockSize() / 2, set.GetBlockSize() / 2);
            blocks = new List<Block>();
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                for (int m = 0; m < set.GetBlocksRow(); m++)
                {

                    int index = i * set.GetBlocksRow() + m;
                    int X = 3 + m * (set.GetBlockSize() + 3);
                    int Y = 3 + i * (set.GetBlockSize() + 3);
                    
                    int RandomNumber = rnd.Next(0, 100);
                    if ((RandomNumber>=0)&&(RandomNumber<=20))
                        blocks.Add(new MetalBlock(X, Y, set.GetBlockSize(), set.GetBlockSize()));
                    else
                        blocks.Add(new WoodBlock(X, Y, set.GetBlockSize(), set.GetBlockSize()));

                }
            }
        }
        public void CheckBorder(Bullet bul)
        {
            CExtends extends = bul.GetExtends();
            
            if ((extends.LeftX <= 0)||(extends.RightX>=FormWidth-15)) bullet.InvertXSpeed();
            if (extends.UpperY <= 0) bullet.InvertYSpeed();

        }
        
        public bool CheckFault ()
        {
            CExtends extends = bullet.GetExtends();
            if (extends.UpperY >= FormHeight)
            {
                Lose();
                return true;
            }
            else return false;
        }
        public void CheckPlatfrom()
        {
            if (CExtends.IsIntersected(bullet.GetExtends(), platform.GetExtends()))
            {
                if (GameStarted) bullet.InvertYSpeed();
                if (Pause)
                {
                    bullet.Stop();
                    FollowPlatform();
                }
            }
        }
        public void FollowPlatform()
        {
            bullet.PosX = platform.PosX + platform.Width / 2 - bullet.Width / 2;
            bullet.PosY = platform.PosY - bullet.Height;
            Following = true;

        }
        public bool CheckCollision(Block block)
        {

            if (CExtends.IsIntersected(block.GetExtends(), bullet.GetExtends()))
            {
                CExtends BlockExtends = block.GetExtends();
                CExtends BulletExtends = bullet.GetExtends();
                Point bulpt = BulletExtends.Center;
                Point blockpt = BlockExtends.Center;
                int dx = blockpt.X - (bulpt.X - bullet.SpeedX);
                int dy = blockpt.Y - (bulpt.Y - bullet.SpeedY);
                float tga = 1;
                try
                {
                    tga = Math.Abs(dx) / Math.Abs(dy);
                }
                catch (DivideByZeroException)
                {

                }

                if (tga >= 1)
                {
                    bullet.InvertXSpeed();
                }
                //   if (tga==1)
                // {
                //     if (bul.XSpeedCorrected == false) bul.SpeedX = -bul.SpeedX;
                //    bul.XSpeedCorrected = true;
                //       if (bul.YSpeedCorrected == false) bul.SpeedY = -bul.SpeedY;
                //       bul.YSpeedCorrected = true;
                //    }
                if (tga < 1)
                {
                    bullet.InvertYSpeed();
                }
                return true;
            }
            else return false;
            
        }
        public delegate void Container();
        public event Container Win;
        public event Container Lose;
        public void ProcessFrame()
        {
            CheckBorder(bullet);
            CheckFault();
            CheckPlatfrom();
            bullet.Move();
            int index=0;
            bool allowdelete = false;
            foreach (Block b in blocks)
            {
                                
                if (CheckCollision(b))
                {
                    b.GetHit();
                    index = blocks.IndexOf(b);
                   if (b.Health == 0) allowdelete = true;                                     
                }
            }

            if (allowdelete)
            {
                if (blocks.Count > 1) blocks.RemoveAt(index);
                if (blocks.Count == 1)
                {
                    Win();
                }
            }

            allowdelete = false;
            
            
        }
        public void OnRightKey()
        {
            if (platform.GetExtends().RightX <= FormWidth) { platform.MoveRight(); }
        }
        public void OnLeftKey()
        {
            if (platform.GetExtends().LeftX>=0) { platform.MoveLeft(); }
        }
        public void OnSpaceKey()
        {
            if ((Pause == false) && GameStarted)
            {
                Pause = true;
                
            }
            if (GameStarted == false)
            {
                bullet.Start(Speed);
                bullet.Move();
                GameStarted = true;
            }
            
            if (Pause&&Following)
            {
                bullet.Start(Speed);
                bullet.Move();
                Pause = false;
                Following = false;
                
            }
            

        }
        public void DrawFrame(Graphics gr)
        {
            gr.Clear(Color.White);
            foreach (Block b in blocks)
            {
                b.Draw(gr);
            }
            platform.Draw(gr);
            bullet.Draw(gr);
        }
            
        

    }
}
