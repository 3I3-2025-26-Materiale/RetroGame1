using System;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace RetroGameFramework
{
    internal class GameLogic : BaseGameLogic
    {
        public GameLogic(GameConfig GameConfig) : base(GameConfig) { }

        // GameConfig is a variable already accessible in methods to retrieve the game configs
        // bool IsPaused() is a function already accessible in methods to check if the game is paused
        // void SetPaused(bool) is a function already accessible in methods to set the game in pause and to resume it

        // GAME DATA
        // Declare here game-specific data that should survive the frame

        static int point_x = 0;
        static int point_y = 0;

        static int point_mov_x = 0;
        static int point_mov_y = 0;

        // Initialization call, used to customize GameConfig data (used to customize the engine behaviour)
        protected override void OnInitGameConfig(GameConfig GameConfig)
        {
            GameConfig.Title = "Demo Game";

            GameConfig.PixelsMatrixWidth = 128;
            GameConfig.PixelsMatrixHeight = 96;
            GameConfig.PixelSize = 5;

        }

        // Called once per frame, BEFORE the OnLoopGame event.
        protected override void OnClear(int[,] pixels)
        {
            for (int x = 0; x < pixels.GetLength(0); x++)
            {
                for (int y = 0;  y < pixels.GetLength(1); y++)
                {
                    pixels[x, y] = 0;
                }
            }
        }

        // Called once per frame.
        // Here the actual logic happens.
        protected override void OnLoopGame(float deltaTime)
        {
            if (FrameCount == 0)
            {
                point_x = GameConfig.PixelsMatrixWidth / 2;
                point_y = GameConfig.PixelsMatrixHeight / 2;

                point_mov_x = 1;
                point_mov_y = 1;
            }
            else
            {
                if (point_mov_x > 0 && point_x >= GameConfig.PixelsMatrixWidth - 1)
                {
                    point_mov_x *= -1;
                }
                else if (point_mov_x < 0 && point_x <= 0)
                {
                    point_mov_x *= -1;
                }

                if (point_mov_y > 0 && point_y >= GameConfig.PixelsMatrixHeight - 1)
                {
                    point_mov_y *= -1;
                }
                else if (point_mov_y < 0 && point_y <= 0)
                {
                    point_mov_y *= -1;
                }

                point_x += point_mov_x;
                point_y += point_mov_y;

                
            }
        }

        // Called once per frame, AFTER the OnLoopGame event.
        protected override void OnDraw(int[,] pixels)
        {
            int width = pixels.GetLength(0);
            int height = pixels.GetLength(1);

            if (point_x >= 0 && point_x < width
                && point_y >= 0 && point_y < height)
            {
                pixels[point_x, point_y] = 1;
            }
        }
        
        // Called at the end of the last frame of the game.
        // Its main purpose it's to dispose resources, as the game will end immediately after this call.
        protected override void OnEndGame()
        {

        }

        private void UpdateBallPosition()
        {
            
        }

        private void DrawBall(int[,] pixels, int color)
        {
            
        }

        // Called the first frame a key is pressed, and not called anymore unless the key is released
        protected override void OnKeyDown(Keys KeyCode)
        {
            if (KeyCode == Keys.Up)
            {
                point_mov_y = -1;
            }
            else if (KeyCode == Keys.Down)
            {
                point_mov_y = 1;
            }
            else if (KeyCode == Keys.Left)
            {
                point_mov_y = -1;
            }
            else if (KeyCode == Keys.Right)
            {
                point_mov_y = 1;
            }
            else if (KeyCode == Keys.P)
            {
                SetPaused(!IsPaused());
            }
        }

        // Called if a key has been released (even in the same frame it has been released)
        protected override void OnKeyUp(Keys KeyCode)
        {
        
        }

        // Called during the frame a key is pressed and in all the following frames until it's released (excluding the frame it's released)
        protected override void OnKeyPress(Keys KeyCode)
        {
        
        }

    }
}
