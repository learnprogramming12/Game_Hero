using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    enum PlayerState
    {
        InBattle = 0,
        Win,
        Lost,
    }
    internal class Player
    {
        private static Player _singleton;
        private static PlayerState _playerState = PlayerState.InBattle;
      //  private int _lifePoints = 10;
        private int _speed = 5;
        private int _delay;
        public static int Score = 0;
        private const int _ignoredFrames = 20;//for weapon fire, fire once at least every 20 frames. 
        private Sprite _sprite;
        private RenderWindow _window;
        private WeaponSystem _weaponSystem;
        private HealthSystem _healthSystem;
        public int LifePoints
        {
            get { return _healthSystem.RemainingLifePoints; }
            set
            {
                _healthSystem.RemainingLifePoints = value;
                if (_healthSystem.RemainingLifePoints <= 0)
                {
                    _playerState = PlayerState.Lost;
                    //set player invisible
                    Color spriteColor = _sprite.Color;
                    spriteColor.A = 0;
                    _sprite.Color = spriteColor;
                }               
            }
            /*            get { return _lifePoints; }
                        set
                        {
                            _lifePoints = value;
                            if (_lifePoints <= 0)
                            {
                                _playerState = PlayerState.Lost;
                                //set player invisible
                                Color spriteColor = _sprite.Color;
                                spriteColor.A = 0;
                                _sprite.Color = spriteColor;
                            }
                            _healthSystem.RemainingLifePoints = _lifePoints;
                        }*/
        }
        public Sprite Sprite
        {
            get { return _sprite; }
        }
        public Vector2f Position
        {
            get { return _sprite.Position; }
        }
        public static PlayerState PlayerState
        {
            get { return _playerState; }
            set { _playerState = value; }
        }
        private Player(/*int lifePoints, int speed, RenderWindow parent*/)
        {
/*            _lifePoints = lifePoints;
            _speed = speed;
            _window = parent;*/
            _delay = 0;

            _sprite = new Sprite();
            _sprite.Texture = TextureManager.Player;
            Vector2f requiredSize = new Vector2f(60, 70);
            _sprite.Scale = new Vector2f(requiredSize.X / _sprite.Texture.Size.X, requiredSize.Y / _sprite.Texture.Size.Y);
        }
        public static Player GetInstance()
        {
            if (_singleton == null)
                _singleton = new Player();

            return _singleton;
        }
        public void init()
        {
            _playerState = PlayerState.InBattle;
            Score = 0;
            _sprite.Position = new Vector2f(450f, 450f/*_window.Size.X / 2f, _window.Size.Y / 5f * 4*/);
            Color spriteColor = _sprite.Color;
            spriteColor.A = 255;
            _sprite.Color = spriteColor;
            _weaponSystem = new WeaponSystem(_sprite, WeaponSystem.FirepowerIntensity.SingleMissle);
            _healthSystem = new HealthSystem(10);
            _healthSystem.Init(true, new Vector2f(800, 850), new Vector2f(80, 10), Color.Green);
        }
        public WeaponSystem.FirepowerIntensity getWeaponType()
        {
            return _weaponSystem.FirePowerIntensity;
        }
        public void OnKeyEvent()
        {
            Vector2f position = new Vector2f(_sprite.Position.X, _sprite.Position.Y);

            bool moveLeft = Keyboard.IsKeyPressed(Keyboard.Key.Left);
            bool moveRight = Keyboard.IsKeyPressed(Keyboard.Key.Right);
            bool moveUp = Keyboard.IsKeyPressed(Keyboard.Key.Up);
            bool moveDown = Keyboard.IsKeyPressed(Keyboard.Key.Down);
            if (moveLeft)
            {
                if (position.X - _speed < 0)
                    position.X = 0;
                else
                    position.X -= _speed;
            }
            if (moveRight)
            {
                if (position.X + _sprite.GetGlobalBounds().Width + _speed > Game.WindowSize.X)
                    position.X = Game.WindowSize.X - _sprite.GetGlobalBounds().Width;
                else
                    position.X += _speed;
            }
            if (moveUp)
            {
                if (position.Y - _speed < 0)
                    position.Y = 0;
                else
                    position.Y -= _speed;
            }
            if (moveDown)
            {
                if (position.Y + _sprite.GetGlobalBounds().Height + _speed > Game.WindowSize.Y)
                    position.Y = Game.WindowSize.Y - _sprite.GetGlobalBounds().Height;
                else
                    position.Y += _speed;
            }
            _sprite.Position = position;

            bool fire = Keyboard.IsKeyPressed(Keyboard.Key.Space);
            if (fire)
            {
                _delay++;
                if (_delay < _ignoredFrames)
                    return;
                _delay -= _ignoredFrames;

                _weaponSystem.Fire();
            }
        }
        public void Update()
        {
            OnKeyEvent();

            DoubleLinkedList<Prize> prizeList = ManagerOfPrize.PrizeList;
            for(int i = 0; i < prizeList.Count;)
            {
                if (_sprite.GetGlobalBounds().Intersects(prizeList[i].GetGlobalBounds()))
                {
                    ManagerOfMusic.Prize.Play();

                    switch (prizeList[i].Type)
                    {
                        case PrizeType.DoubleProjectile:
                            _weaponSystem.UpgradeWeaponSystem(WeaponSystem.FirepowerIntensity.DoubleMissle);
                            break;
                        case PrizeType.FourProjectile:
                            _weaponSystem.UpgradeWeaponSystem(WeaponSystem.FirepowerIntensity.FourMissle);
                            break;
                        case PrizeType.LifePoints:
                            break;
                    }
                    prizeList.Remove(prizeList[i]);
                }
                else
                {
                    i++;
                }
            }
        }
        public void Draw(RenderWindow window)
        {
            _healthSystem.Draw(window);
            window.Draw(_sprite);
        }
    }
}
