using SFML.System;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class Background
    {
        private Sprite _background1;
        private Sprite _background2;
        private static Vector2f _speed = new Vector2f(0, 0.5f);
        public static Vector2f Speed
        {
            get { return _speed;}}
        public Background() 
        {
            _background1 = new Sprite();
            _background1.Texture = TextureManager.Background;
            _background1.Position = new Vector2f(_background1.GetLocalBounds().Width / 2f, _background1.GetLocalBounds().Height / 2f);
            _background1.Origin = new Vector2f(_background1.GetLocalBounds().Width / 2f, _background1.GetLocalBounds().Height / 2f);
            _background1.Rotation = 180;

            _background2 = new Sprite();
            _background2.Texture = TextureManager.Background;
            _background2.Position = new Vector2f(0, 0 - _background2.GetLocalBounds().Height);

        }
        public void Update()
        {
            _background1.Position = new Vector2f(_background1.Position.X, _background1.Position.Y + _speed.Y);
            if (_background1.GetGlobalBounds().Top >= Game.WindowSize.Y)
            {
                _background1.Position = new Vector2f(_background1.Position.X, _background2.Position.Y - _background1.GetLocalBounds().Height / 2f + 1);
            }
            _background2.Position = new Vector2f(_background2.Position.X, _background2.Position.Y + _speed.Y);
            if (_background2.Position.Y >= Game.WindowSize.Y)
            {
                _background2.Position = new Vector2f(_background2.Position.X, _background1.GetGlobalBounds().Top - _background2.GetLocalBounds().Height + 1);
            }
        }
        public void Draw(RenderWindow window)
        {
            window.Draw(_background1);
            window.Draw(_background2);
        }
    }
}
