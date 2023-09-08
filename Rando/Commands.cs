using static System.Console;
using System.Reflection;
using System.Resources;

namespace Rando
{
    public static class Commands
    {
        public static int _posXNormalTxt = 9;
        public static int _posXInputTxt = _posXNormalTxt + 4;
        public static int _posXWarningTxt = _posXInputTxt + 2;

        public static void InMiddle(int TextLength, int XPos)
        {
            int _with = (WindowWidth / 2) - (TextLength / 2);
            SetCursorPosition(_with, XPos);
        }

        public static void InMiddle(int TextLength, int XPos, int extra)
        {
            int _with = (WindowWidth / 2) + (TextLength / 2) + extra;
            SetCursorPosition(_with, XPos);
        }


        public static string Txt(string Tag)
        {
            ResourceManager rm = new ResourceManager("Rando.Resource.Strings", Assembly.GetExecutingAssembly());
            string _txt;
            _txt = rm.GetString(Tag);
            if (string.IsNullOrWhiteSpace(_txt))
            {
                _txt = "[?null]";
            }
            return _txt;
        }

        public static void Warning(string message)
        {
            Clear();
            string _warnMessage = String.Format("{0}: {1}!", Txt("warn"), message);
            InMiddle(_warnMessage.Length, _posXWarningTxt);
            ForegroundColor = ConsoleColor.Red;
            WriteLine(_warnMessage);
            ResetColor();
        }

        public static void Warning(string message, string location)
        {
            Clear();
            string _warnMessage = String.Format("{0}: {1}!      {2}", Txt("warn"), message, location);
            InMiddle(_warnMessage.Length, _posXWarningTxt);
            ForegroundColor = ConsoleColor.Red;
            WriteLine(_warnMessage);
            ResetColor();
        }

        public static void Clear()
        {
            SetCursorPosition(0, _posXWarningTxt);
            Write(new string(' ', WindowWidth));
        }

        public static void Clear(int y, int x)
        {
            SetCursorPosition(y, x);
            Write(new string(' ', WindowWidth));
        }

        public static void normalWrite(string message)
        {
            Clear(0, _posXNormalTxt);
            InMiddle(message.Length, _posXNormalTxt);
            WriteLine("{0}", message);
        }

        public static string Input(string message)
        {
            string? _output;
            Clear(0, _posXInputTxt);
            InMiddle(message.Length + 1, _posXInputTxt);
            WriteLine("{0}:", message);
            InMiddle(message.Length + 1, _posXInputTxt, 1);
            _output = ReadLine();
            Clear(0, _posXInputTxt);
            return _output;
        }

        public static ConsoleKeyInfo Input(string message, bool key)
        {
            ConsoleKeyInfo _output;
            Clear(0, _posXInputTxt);
            InMiddle(message.Length + 1, _posXInputTxt);
            WriteLine("{0}:", message);
            InMiddle(message.Length + 1, _posXInputTxt, 1);
            _output = Console.ReadKey();
            Clear(0, _posXInputTxt);
            return _output;
        }

    }
}