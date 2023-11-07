using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hero
{
    internal class Button
    {
        private RectangleShape _buttonShape;
        private Text _buttonText;
        public event EventHandler Click;

        public Vector2f Size { get { return _buttonShape.Size; } }
        public string Text { get { return _buttonText.DisplayedString; } }
        public Button(string  text, Vector2f position, Vector2f size)
        {
            _buttonShape = new RectangleShape(size);
            _buttonShape.Position = position;
            _buttonShape.FillColor = Color.White;
            _buttonShape.OutlineColor = Color.Black;
            _buttonShape.OutlineThickness = 1.0f;

            _buttonText = new Text(text, new Font(new Font(TextManager.FontPath + "arial.ttf")), 20);
            FloatRect textBounds = _buttonText.GetLocalBounds();
            _buttonText.Position = position + (_buttonShape.Size - new Vector2f(textBounds.Width, textBounds.Height)) / 2f;
            _buttonText.FillColor = Color.Black;

        }
        public void HandleClick(Vector2f clickPos)
        {
            if(_buttonShape.GetGlobalBounds().Contains(clickPos.X, clickPos.Y))
            {
                Click?.Invoke(this, EventArgs.Empty);
            }
        }
        public void Draw(RenderWindow window)
        {
            window.Draw(_buttonShape);
            window.Draw(_buttonText);
        }
    }
}
