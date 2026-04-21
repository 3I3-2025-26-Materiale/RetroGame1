using System;
using System.Windows.Forms;

namespace RetroGameFramework
{
    internal class BaseGameLogic
    {
        private GameConfig _gameConfig;
        public GameConfig GameConfig { get { return _gameConfig; } }
        
        // CREATE GAME MATRIX
        static void Main(string[] args)
        {
            GameConfig GameConfig = new GameConfig();
            BaseGameLogic GameLogic = new GameLogic(GameConfig);
            GameLogic.InitGameConfig(GameConfig);
            int[,] PixelsMatrix = new int[GameConfig.PixelsMatrixWidth, GameConfig.PixelsMatrixHeight];

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form gameForm = new GameForm(GameConfig, PixelsMatrix);

            bool continueGame = true;

            GameLogic.StartGame(PixelsMatrix);
            GameLogic.LoopGame(PixelsMatrix);
            gameForm.Invalidate();
            gameForm.Update();

            System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000 / GameConfig.FrameRate;
            gameTimer.Tick += (s, e) =>
            {
                if (continueGame)
                {
                    gameForm.Invoke(new Action(() =>
                    {
                        // This is done in the gameForm owner thread (the main thread)
                        // through a delegated call. Multithread logic cannot be called
                        // https://visualstudiomagazine.com/articles/2010/11/18/multithreading-in-winforms.aspx
                        gameForm.Invalidate();
                        gameForm.Update();
                    }));
                    GameLogic.LoopGame(PixelsMatrix);
                }
                else
                {
                    GameLogic.EndGame();
                    gameTimer.Stop();
                }
            };
            gameTimer.Start();

            Application.Run(gameForm); // This runs the form with the main thread as owner
        }

        public BaseGameLogic(GameConfig GameConfig)
        {
            _gameConfig = GameConfig;
        }

        private void InitGameConfig(GameConfig GameConfig)
        {
            OnInitGameConfig(GameConfig);
            _gameConfig = GameConfig;
        }
        protected virtual void OnInitGameConfig(GameConfig GameConfig) { }

        private void StartGame(int[,] pixels)
        {
            OnStartGame(pixels);
        }
        protected virtual void OnStartGame(int[,] pixels) { }

        private void LoopGame(int[,] pixels)
        {
            OnClear(pixels);
            OnLoopGame();
            OnDraw(pixels);
        }

        protected virtual void OnClear(int[,] pixels) { }
        protected virtual void OnLoopGame() { }
        protected virtual void OnDraw(int[,] pixels) { }

        private void EndGame()
        {
            OnEndGame();
        }
        protected virtual void OnEndGame() { }

    }
}
