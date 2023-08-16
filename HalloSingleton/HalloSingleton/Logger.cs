namespace HalloSingleton
{
    internal class Logger
    {
        private static Logger _instance;
        private static object _syncLock = new object();

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                    lock (_syncLock)
                    {
                        if (_instance == null)
                            _instance = new Logger();
                    }

                return _instance;
            }
        }

        private Logger()
        {
            Info("Neuer Logger");
        }

        public void Info(string msg)
        {
            Console.WriteLine($"[INFO] {DateTime.Now:g} {msg}");
        }

        public void Error(string msg)
        {
            Console.WriteLine($"[ERROR] {DateTime.Now:g} {msg}");
        }
    }
}
