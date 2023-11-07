using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Hero
{
    internal class Game
    {
        private static uint _frameLimit = 60;
        public static readonly Vector2u WindowSize = new Vector2u(900, 900);
        private RenderWindow _window;
        private Background _background;
        private Player _player; 
        private ManagerOfPassingLevels _formationManager;
        private MessageBox _summaryDialog;
        public static Random Random = new Random(); //Many places will use the random, and if each of many new instances keeps a random, they
                                                    //probably generate the same random value, so that is not random, and that will generate weired behaviour
        public static uint FrameLimit { get { return _frameLimit; } }
        public Game() 
        {
            VideoMode videoMode = new VideoMode(WindowSize.X, WindowSize.Y);
            _window = new RenderWindow(videoMode, "Hero", Styles.Titlebar | Styles.Close);
            _window.SetVerticalSyncEnabled(true);
            _window.SetFramerateLimit(_frameLimit);

            TextureManager.Load();
            ManagerOfMusic.Load();

            _background = new Background();

            _player = Player.GetInstance(/*100000, 5, _window*/);
/*            _player.Sprite.Texture = TextureManager.Player;
            _player.Sprite.Position = new Vector2f(_window.Size.X / 2f, _window.Size.Y / 5f * 4);
            Vector2f requiredSize = new Vector2f(50, 60);
            _player.Sprite.Scale = new Vector2f(requiredSize.X / _player.Sprite.Texture.Size.X, requiredSize.Y / _player.Sprite.Texture.Size.Y);*/
            
            _formationManager = new ManagerOfPassingLevels(_window);

            _window.Closed += OnClosed;
            _window.MouseButtonReleased += OnMouseButtonReleased;

            init();
        }
        private void init()
        {
            _player.init();
            _formationManager.init();
            ManagerOfAnimation.init();
            ManagerOfPrize.init();
            ManagerOfEnemyFiredProjectile.init();
            ManagerOfPlayerFiredProjectile.init();
        }
        public void Run()
        {

            while (_window.IsOpen) 
            {
             //   Console.WriteLine($"{++i}");
                _window.DispatchEvents();
                Update();
                Draw();
            }
        }
        private void Update()
        {
            if (Player.PlayerState == PlayerState.InBattle)
            {
                _background.Update();
                _player.Update();
                ManagerOfPlayerFiredProjectile.Update();
                ManagerOfEnemyFiredProjectile.Update(_player);
                _formationManager.Update(_player);
                ManagerOfPrize.Update();
                ManagerOfAnimation.Update();
            }
            else if(Player.PlayerState == PlayerState.Win)
            { 
                if(_summaryDialog == null)
                    _summaryDialog = new MessageBox("Congratulations, you won.\nDo you want to start a new game?", new Vector2f(_window.Size.X / 2f - 200, _window.Size.Y / 2f - 200), MessageBoxType.YESNO);
                //TextManager.AddText("Congratulations, you won.", new Vector2f(_window.Size.X / 2f - 100, _window.Size.Y / 2f), 30, Color.Green);
            }
            else if(Player.PlayerState == PlayerState.Lost)
            {
                if(_summaryDialog == null)
                    _summaryDialog = new MessageBox("Sorry, you lost.\nDo you want to start a new game?", new Vector2f(_window.Size.X / 2f - 200, _window.Size.Y / 2f - 200), MessageBoxType.YESNO);
                //TextManager.AddText("Sorry, you lost.", new Vector2f(_window.Size.X / 2f - 100, _window.Size.Y / 2f), 30, Color.Green);
            }
            TextManager.AddText($"Score: {Player.Score}", new Vector2f(10, _window.Size.Y - 30 - 10), 30, Color.White);
        }
        private void Draw()
        {
            _window.Clear(new Color(173, 216, 230));

            _background.Draw(_window);
            _player.Draw(_window);

            _formationManager.Draw(_window);
            ManagerOfAnimation.Draw(_window);
            TextManager.Draw(_window);
            ManagerOfPrize.Draw(_window); 
            ManagerOfPlayerFiredProjectile.Draw(_window);
            ManagerOfEnemyFiredProjectile.Draw(_window);
            if (_summaryDialog != null)
                _summaryDialog.Draw(_window);

            _window.Display();
        }

        private void OnClosed(Object sender, EventArgs e)
        {
            _window.Close();
        }
        private void OnMouseButtonReleased(Object sender, MouseButtonEventArgs e)
        {
            if(_summaryDialog != null)
            {
                if (MessageBoxResult.YES == _summaryDialog.Result(new Vector2f(e.X, e.Y)))
                {
                    Console.WriteLine("Yes button clicked");
                    _summaryDialog = null;
                    init();
                }
                else if (MessageBoxResult.NO == _summaryDialog.Result(new Vector2f(e.X, e.Y)))
                {
                    Console.WriteLine("No button clicked");
                    _window.Close();
                }
            }
        }
    }
}
