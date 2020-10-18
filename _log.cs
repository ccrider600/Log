using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Log
{
    public class _log
    {
        private StreamWriter _fs = null;
        private readonly LogOutType _type = LogOutType.All;
        private bool _on = true;
        private readonly string _logDir = null;
        private readonly string _logFile = null;

        public enum LogOutType
        {
            Default,
            Screen,
            Disk,
            All
        }

        public _log(string logDir, string logFile, bool on = true, LogOutType Default = LogOutType.All)
        {
            this._type = Default;
            this._logDir = logDir;
            this._logFile = logFile;
            this._on = on;

            // off = solo guarda archivo log (no abre)
            if (on)
            {
                _open();
            }
        }

        private void _write(string msg)
        {
            if (this._on && this._fs != null)
            {
                try
                {
                    this._fs.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}, {1}", DateTime.Now, msg));
                    //this._fs.Flush();
                }
                catch
                {
                }
            }
        }

        public void Write(string msg, LogOutType tipo = LogOutType.Default)
        {
            if (tipo == LogOutType.Default)
            {
                tipo = this._type;
            }
            if (tipo == LogOutType.All || tipo == LogOutType.Screen)
            {
                Console.Write(msg);
            }
            if (tipo == LogOutType.All || tipo == LogOutType.Disk)
            {
                _write(msg);
            }
        }

        public void WriteLine(string msg, LogOutType tipo = LogOutType.Default)
        {
            if (tipo == LogOutType.Default)
            {
                tipo = this._type;
            }
            if (tipo == LogOutType.All || tipo == LogOutType.Screen)
            {
                Console.WriteLine(msg);
            }
            if (tipo == LogOutType.All || tipo == LogOutType.Disk)
            {
                _write(msg);
            }
        }

        public void Newline(LogOutType tipo = LogOutType.Default)
        {
            this.WriteLine(string.Empty, tipo);
        }

        public void Close()
        {
            if (this._fs != null)
            {
                //this._fs.Flush();
                try
                {
                    this._fs.Close();
                }
                catch
                {
                }
                this._fs = null;
            }
        }

        private void _open()
        {
            try
            {
                if (this._fs != null)
                {
                    this._fs.Close();
                }
                if (!Directory.Exists(this._logDir))
                {
                    Directory.CreateDirectory(this._logDir);
                }
                this._fs = File.CreateText(Path.Combine(this._logDir, this._logFile + ".txt"));
                this._fs.AutoFlush = true;
            }
            catch
            {
            }
        }

        public void On()
        {
            this._on = true;
            if (this._fs == null)
            {
                this._open();
            }
        }

        public void Off()
        {
            this._on = false;
        }

    }
}
