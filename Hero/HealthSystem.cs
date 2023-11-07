using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class HealthSystem
    {
        private int _lifePoints;
        private int _remainingLifePoints;
        private bool _displayBar = false;
        //private Vector2f _lifePointsBarPosition;
        private Vector2f _lifePointsBarSize;
        private RectangleShape _remainingLifePointsBar;
        private RectangleShape _lostLifePointsBar;

        public int RemainingLifePoints
        {
            get { return _remainingLifePoints; }
            set 
            {
                _remainingLifePoints = value;
                if(_remainingLifePoints < 0)
                    _remainingLifePoints = 0;
                else if(_remainingLifePoints > _lifePoints)
                    _remainingLifePoints = _lifePoints;

                if (_displayBar)
                    UpdateLifePointsBar();
            }
        }
        public HealthSystem(int lifePoints) 
        { 
            _lifePoints = lifePoints;
            _remainingLifePoints = lifePoints;          
        }
        public void Init(bool displayBar, Vector2f position, Vector2f size, Color displayColor)
        {
            _displayBar = displayBar;
            if (_displayBar)
            {
                _lifePointsBarSize = size;

                _remainingLifePointsBar = new RectangleShape(size);
                _remainingLifePointsBar.FillColor = displayColor;
                _remainingLifePointsBar.Position = position;

                _lostLifePointsBar = new RectangleShape(size);
                _lostLifePointsBar = new RectangleShape(size);
                _lostLifePointsBar.FillColor = new Color(128, 128, 128);//gray
                _lostLifePointsBar.Position = position;
            }
            /*            RectangleShape rectangle = new RectangleShape(new Vector2f(100, 100));
            rectangle.Position = new Vector2f(1, 1);
            rectangle.FillColor = Color.Transparent;
            rectangle.OutlineColor = Color.Red;
            rectangle.OutlineThickness = 1.0f;*/
        }
        private void UpdateLifePointsBar()
        {
            float remainingPercentage = (float)_remainingLifePoints / _lifePoints;
            if (remainingPercentage <= 0)
            {
                _remainingLifePointsBar.FillColor = Color.Transparent;
                return;
            }
            _remainingLifePointsBar.Size = new Vector2f(remainingPercentage * _lifePointsBarSize.X, _lifePointsBarSize.Y);
        }
        public void Update(Vector2f pos)
        {
            if(_displayBar)
            {
                _lostLifePointsBar.Position = pos;
                _remainingLifePointsBar.Position = pos;
            }
        }
        public void Draw(RenderWindow window)
        {
            if(_displayBar)
            {
                window.Draw(_lostLifePointsBar);
                window.Draw(_remainingLifePointsBar);
            }
        }
    }
}
