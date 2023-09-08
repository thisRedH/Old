#region Using
using System.IO;
// System
using System.Globalization;
using System;
// Newtensoft
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
// This
using static Rando.Commands;
using Blueprints.json;
#endregion Using

namespace Rando
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Clear(0, i);
            }
            // Title / Logo
            //Console.WriteLine("\n | RANDOM GAMEPICKER |\n");
            Logo();
            Console.Title = "   Random Gamepicker   |   by RedH";
            // Config
            Config config = _LoadConfig();
            // Language
            _LanguageHandler(config);
            // Directory
            string dir = _DirTest(config.configurations[0].dir);
            string[] inDir = _FilesandFolder(config, dir);
            // Picker
            _Randopicker(inDir);
        }

        static void Logo()
        {
            int _with = (Console.WindowWidth / 2) - 42;
            int _start = 0;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(_with, _start + 1);
            Console.WriteLine(@" ___              _               ___                         _       _             ");
            Console.SetCursorPosition(_with, _start + 2);
            Console.WriteLine(@"| . \ ___ ._ _  _| | ___ ._ _ _  /  _>  ___ ._ _ _  ___  ___ <_> ___ | |__ ___  _ _ ");
            Console.SetCursorPosition(_with, _start + 3);
            Console.WriteLine(@"|   /<_> || ' |/ . |/ . \| ' ' | | <_/\<_> || ' ' |/ ._>| . \| |/ | '| / // ._>| '_>");
            Console.SetCursorPosition(_with, _start + 4);
            Console.WriteLine(@"|_\_\<___||_|_|\___|\___/|_|_|_| `____/<___||_|_|_|\___.|  _/|_|\_|_.|_\_\\___.|_|  ");
            Console.SetCursorPosition(_with, _start + 5);
            Console.WriteLine(@"                                                        |_|                         ");
            Console.ResetColor();
        }

        static Config _LoadConfig()
        {
            string _dirConfig = @".\config.json";
            Config? _config;
            try
            {
                _config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(_dirConfig));
            }
            catch
            {
                var _newconfig = new Config()
                {
                    version = "2.1",
                    language = "",
                    configurations = new List<ConfigChild_configurations> { new ConfigChild_configurations() { dir = "", mode = "filesandfolder" } }
                };
                File.WriteAllText(_dirConfig, JsonConvert.SerializeObject(_newconfig, Formatting.Indented));
                _config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(_dirConfig));
            }

            return _config;
        }

        static void _ChangeConfig(string _Token, string _TextChange)
        {
            string _dirConfig = @".\config.json";
            JObject _jObject = JsonConvert.DeserializeObject(File.ReadAllText(_dirConfig)) as JObject;
            JToken _jToken = _jObject.SelectToken(_Token);
            _jToken.Replace(_TextChange);
            File.WriteAllText(_dirConfig, _jObject.ToString());
        }

        static void _LanguageHandler(Config _config)
        {
            if (_config.language != "")
            {
                bool _valid = false;
                string _lang = _config.language;
                while (!_valid)
                {
                    try
                    {
                        Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(_lang);
                        _valid = true;
                    }
                    catch (System.Globalization.CultureNotFoundException)
                    {
                        Warning(String.Format("There isnt a Language like {0}", _lang), String.Format("({0})", Path.GetFullPath(@".\config.json")));
                        _lang = Input("new Language");
                    }
                    catch (System.Exception ex)
                    {
                        Warning("Unkown Warning (Language)", ex.Message);
                        Console.ReadKey();
                    }
                }
                _ChangeConfig("language", _lang);
            }
        }

        static string _DirTest(string _path)
        {
            bool _valid = false;
            while (!_valid)
            {
                if (Directory.Exists(_path))
                {
                    if (Directory.EnumerateFileSystemEntries(_path).Any())
                    {
                        _valid = true;
                    }
                    else
                    {
                        Warning(Txt("warn_PathEmpty"));
                        _path = Input(Txt("inp_PathNew"));
                        _path = _path.Trim(new Char[] {'"'});
                    }
                }
                else
                {
                    Warning(Txt("warn_PathInvalid"));
                    _path = Input(Txt("inp_PathNew"));
                    _path = _path.Trim(new Char[] {'"'});
                }
            }
            Clear();
            // Write path into config
            _ChangeConfig("configurations[0].dir", _path);
            return _path;
        }

        static string[] _FilesandFolder(Config _config, string _dir)
        {
            string _mode = _config.configurations[0].mode;
            string[] _inDir = { };
            bool _valid = false;

            while (!_valid)
            {
                // Filesandfolder
                if (_mode == "filesandfolder")
                {
                    string[] filesinDir = Directory.GetFiles(_dir);
                    string[] folderinDir = Directory.GetDirectories(_dir);

                    List<string> _stuffinDir = new List<string>();
                    foreach (string file in filesinDir)
                    {
                        _stuffinDir.Add(file);
                    }
                    foreach (string folder in folderinDir)
                    {
                        _stuffinDir.Add(folder);
                    }

                    _inDir = _stuffinDir.ToArray();

                    _valid = true;
                }
                // Files
                else if (_mode == "files")
                {
                    _inDir = Directory.GetFiles(_dir);
                    _valid = true;
                }
                // Folder
                else if (_mode == "folder")
                {
                    _inDir = Directory.GetDirectories(_dir);
                    _valid = true;
                }
                // No Mode Selected
                else if (_mode == null || _mode == "")
                {
                    Warning(Txt("warn_ModeNoSelected"), "(config.json/configurations/mode)");          // TODO: -------------------------------------------------------------
                    _mode = Input(Txt("inp_ModeNew"));
                }
                // Wrong Mode Selected
                else
                {
                    Warning(Txt("warn_ModeWrong"), "(config.json/configurations/mode)");
                    _mode = Input(Txt("inp_ModeNew"));
                }
            }
            _ChangeConfig("configurations[0].mode", _mode);
            Clear();
            return _inDir;
        }

        static void _Randopicker(string[] _filesinDir)
        {
            Random _rnd = new Random();
            string _file = _filesinDir[_rnd.Next(_filesinDir.Length)];
            Console.ForegroundColor = ConsoleColor.Yellow;
            normalWrite(String.Format("- {0} -", Path.GetFileNameWithoutExtension(_file)));
            Console.ResetColor();

            _ReRoll(_filesinDir);
        }

        static void _ReRoll(string[] _filesinDir)
        {
            ConsoleKeyInfo _ynkey = Input(String.Format("{0} ({1})", Txt("inpk_RollAgain"), Txt("inpk_YN")), true);

            // Reroll
            if (_ynkey.Key == ConsoleKey.Y)
            {
                Clear();
                _Randopicker(_filesinDir);
            }
            // Close
            else if (_ynkey.Key == ConsoleKey.N)
            {
                Environment.Exit(0);
            }
            // Warning
            else
            {
                Warning(String.Format("{0} {1}", _ynkey.Key, Txt("warn_RerollKey")));
                _ReRoll(_filesinDir);
            }
            Clear();
        }
    }
}