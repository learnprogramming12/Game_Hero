using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    enum PrizeType
    {
        None,
        DoubleProjectile,
        FourProjectile,
        LifePoints,
    }
    class Prize
    {
        private PrizeType _prizeType;
        private static Vector2f _speed;//if player does not get it, it will be invalid after it goes out the game area.
        private int _value;//if the prize type is projectile, it is meaningless. referencing it can lead to unexpected behaviour.
        public PrizeType Type { get {  return _prizeType; } }
        public int Value { get { return _value; } }
        private Sprite _sprite;
        public Vector2f Position { set { _sprite.Position = value; } }
        public FloatRect GetGlobalBounds() { return _sprite.GetGlobalBounds(); }
       // public Sprite Sprite { get { return _sprite; } }
        public Prize(PrizeType prizeType, int value = int.MinValue) 
        { 
            _prizeType = prizeType;
            _value = value;
            _speed = Background.Speed;

            _sprite = new Sprite();           
            switch (prizeType)
            {
                case PrizeType.DoubleProjectile:
                    _sprite.Texture = TextureManager.DoubleMissles;
                    break;
                case PrizeType.FourProjectile:
                    _sprite.Texture = TextureManager.FourMissles;
                    break;
                case PrizeType.LifePoints:
                    break;
            }
            Vector2f requiredSize = new Vector2f(50, 50);
            _sprite.Scale = new Vector2f(requiredSize.X / _sprite.Texture.Size.X, requiredSize.Y / _sprite.Texture.Size.Y);
        }
        public void Update()
        {
            _sprite.Position = new Vector2f(_sprite.Position.X + _speed.X, _sprite.Position.Y + _speed.Y);
        }
        public void Draw(RenderWindow window)
        {
            window.Draw(_sprite);
        }
    }
    internal class ManagerOfPrize
    {
        public static DoubleLinkedList<Prize> PrizeList { get; } = new DoubleLinkedList<Prize>();
        public static void init()
        {
            PrizeList.Clear();
        }
        public static void Update()
        {
            FloatRect rectWindow = new FloatRect(0, 0, Game.WindowSize.X, Game.WindowSize.Y);
            for (int i = 0; i < PrizeList.Count;)
            {
                PrizeList[i].Update();
                if ((rectWindow.Intersects(PrizeList[i].GetGlobalBounds()) == false) || (PrizeList[i].Type == PrizeType.None))
                {
                    PrizeList.Remove(PrizeList[i]);
                    //Console.WriteLine("prize count after go out: " + PrizeList.Count);
                }
                else
                {
                    i++;
                }
            }
        }
        public static void Draw(RenderWindow window)
        {
            for (int i = 0; i < PrizeList.Count; i++)
            {
                PrizeList[i].Draw(window);
            }
        }
    }
}
