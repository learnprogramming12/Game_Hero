using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Hero
{
    class Animation
    {
        private Sprite _sprite;
        private int _count = 0;
        private bool _shouldDestroy = false;
        private DoubleLinkedList<Texture> _animation;

        public bool ShouldDestroy { get { return _shouldDestroy; }}

        public Animation(DoubleLinkedList<Texture> textures, Vector2f position)
        {
            this._sprite = new Sprite();
            this._sprite.Position = position;
            this._sprite.Texture = textures[0];
            this._animation = textures;
        }

        public void Update()
        {
            if (_count >= _animation.Count - 1) 
            {
                _shouldDestroy = true;
            }
            else 
            {
                _count += 1;
                _sprite.Texture = _animation[_count];
            }
        }

        public void Draw(RenderTarget window)
        {
            window.Draw(_sprite);
        }
    }
}