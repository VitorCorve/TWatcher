using TWatcher.Services;


SystemWatcher watcher = new();

watcher.Updated += (t) =>
{
    Console.Clear();
    Console.WriteLine(t.ToString());
};

watcher.Start();

Console.ReadLine();