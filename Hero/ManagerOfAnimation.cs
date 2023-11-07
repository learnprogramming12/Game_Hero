using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.ComponentModel;

namespace Hero

{
    class ManagerOfAnimation
    {
        private static DoubleLinkedList<Animation> _animations = new DoubleLinkedList<Animation>();

        //public static DoubleLinkedList<Animation> Animations { get { return _animations; } }
        public static void init()
        {
            _animations.Clear();
        }
        public static void Add(Animation animation) { _animations.AddLast(animation); }
        public static void Update()
        {
            for (int i = 0; i < _animations.Count;)
            {
                if (_animations[i].ShouldDestroy) 
                {
                    _animations.Remove(_animations[i]);
                }
                else
                {
                    _animations[i].Update();
                    i++;
                }
            }
        }

        public static void Draw(RenderTarget window) 
        {
            for (int i = 0; i < _animations.Count; i++)
            {
                _animations[i].Draw(window);
            }
        }
    }
}