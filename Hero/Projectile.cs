using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class Projectile
    {
        protected bool _valid;
        protected int _damagePower;
        protected Vector2f _speed;
        protected Sprite _sprite;
        public bool Valid { get { return _valid; } }
        public int DamagePower
        {
            get { return _damagePower; }
        }
        public Sprite Sprite { get { return _sprite; } }
        public Projectile(Vector2f position, int damagePower, Vector2f speed)
        {
            _sprite = new Sprite();
            _sprite.Position = position;
            _damagePower = damagePower;
            _speed = speed;
            _valid = true;
        }
        public virtual void Update()
        {
            _sprite.Position = new Vector2f(_sprite.Position.X + _speed.X, _sprite.Position.Y + _speed.Y);
            FloatRect rectWindow = new FloatRect(0, 0, Game.WindowSize.X, Game.WindowSize.Y);
            if (rectWindow.Intersects(_sprite.GetGlobalBounds()) == false)
                _valid = false;
        }
        public void Draw(RenderWindow window)
        {
            window.Draw(_sprite);
        }
    }
}
