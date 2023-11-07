using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SFML.Window.Mouse;

namespace Hero
{
    public enum MessageBoxType
    {
        OK,
        OKCANCEL,
        YESNO,
        YESNOCANCEL,
    }
    public enum MessageBoxResult
    {
        OK,
        CANCEL,
        YES,
        NO,
        NONE,
    }
    internal class MessageBox
    {
        private RectangleShape _messageBoxShape;
        private Text _messageBoxText;
        private DoubleLinkedList<Button> _btnList = new DoubleLinkedList<Button>();
        private MessageBoxResult _result = MessageBoxResult.NONE;
        //private Button _button1 = null;
        //private Button _button2 = null;
        public MessageBox(string text, Vector2f position, MessageBoxType type) 
        {
            _messageBoxShape = new RectangleShape(new Vector2f(370, 220));
            _messageBoxShape.FillColor = Color.White;//new Color(128, 128, 128);
            _messageBoxShape.Position = position; 
            _messageBoxText = new Text(text, new Font(new Font(TextManager.FontPath + "arial.ttf")), 20);
            _messageBoxText.FillColor = Color.Black;
            _messageBoxText.Position = position + new Vector2f(20, 20);

            Vector2f btnSize = new Vector2f(100, 30);
            int btnSpace = 20;
            int btnNum = 3;//default
            string[] btnNames = { "Yes", "No", "Cancel"};
            switch (type)
            {
                case MessageBoxType.OK:
                    btnNum = 1;
                    btnNames[0] = "OK";
                    //btnStartX = _messageBoxShape.Size.X - btn
                    break;
                case MessageBoxType.OKCANCEL:
                    btnNum = 2;
                    btnNames[0] = "OK";
                    btnNames[1] = "Cancel";
                    break;
                case MessageBoxType.YESNO:
                    btnNum = 2;
                    btnNames[0] = "Yes";
                    btnNames[1] = "No";
                    break;
            }

            float btnRelativePosY = _messageBoxShape.Size.Y - btnSpace - btnSize.Y;
            float btnStartX = (_messageBoxShape.Size.X - (btnNum - 1) * btnSpace - btnNum * btnSize.X) / 2f;
            for(int i = 0; i < btnNum; i++)
            {
                Button btn = new Button(btnNames[i], position + new Vector2f(btnStartX + i * (btnSpace + btnSize.X), btnRelativePosY), btnSize);
                btn.Click += this.ClickHandler;
                _btnList.AddLast(btn);
            }
        }
        private void ClickHandler(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                case "OK":
                    _result = MessageBoxResult.OK;
                    break;
                case "Cancel":
                    _result = MessageBoxResult.CANCEL;
                    break;
                case "Yes":
                    _result = MessageBoxResult.YES;
                    break;
                case "No":
                    _result = MessageBoxResult.NO;
                    break;
            }
        }
        public MessageBoxResult Result(Vector2f clickPos)
        {
            _result = MessageBoxResult.NONE;
            for(int i = 0; i <  _btnList.Count; i++)
            {
                _btnList[i].HandleClick(clickPos);
            }

            return _result;
        }
        public void Draw(RenderWindow window)
        {
            window.Draw(_messageBoxShape);
            for(int i = 0; i < _btnList.Count; i++)
            {
                _btnList[i].Draw(window);
            }
            window.Draw(_messageBoxText);
        }
    }
}
