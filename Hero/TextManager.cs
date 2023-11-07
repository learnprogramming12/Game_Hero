using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class TextManager
    {
        public static string FontPath = "../../resources/font/";
        public static DoubleLinkedList<Text> _textList = new DoubleLinkedList<Text>();
        static Font _font = new Font(FontPath + "arial.ttf");
        public static void AddText(string strText, Vector2f pos, uint characterSize, Color color) 
        {
            Text text = new Text(strText, _font, characterSize);
            text.FillColor = color;
            text.Position = pos;

            _textList.AddLast(text);
        }
        public static void Draw(RenderWindow window)
        {
            for(int i = 0; i < _textList.Count;)
            {
                window.Draw(_textList[i]);
                _textList.Remove(_textList[i]);
            }
        }
        public static void Clear()
        {
            _textList.Clear();
        }
        public static void test()
        {
            CircleShape shape = new CircleShape();
            
        }
    }
}
