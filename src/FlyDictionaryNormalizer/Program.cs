using FlyDictionaryNormalizer;

var target = 0;
var inputs = new List<string>();

foreach (var arg in args)
{
    switch (arg)
    {
        case "-d":
        case "--duoduo":
            target = 0;
            break;

        case "-r":
        case "--rime":
            target = 1;
            break;

        default:
            inputs.Add(arg);
            break;
    }
}

foreach (var input in inputs)
{
    using var reader = new StreamReader(input);
    switch (target)
    {
        case 0:
            {
                using var writer = new StreamWriter(Path.ChangeExtension(input, "txt"));
                Entry.Handle(reader, new DuoduoDictionaryWriter(writer));
                break;
            }

        case 1:
            {
                var basePath = Path.GetDirectoryName(Path.GetFullPath(input));
                if (basePath is null)
                {
                    throw new ArgumentNullException();
                }
                var topStream = File.OpenWrite(Path.Combine(basePath, "flypy_top.txt"));
                var userStream = File.OpenWrite(Path.Combine(basePath, "flypy_user.txt"));
                using var top = new StreamWriter(topStream);
                using var user = new StreamWriter(userStream);
                Entry.Handle(reader, new RimeDictionaryWriter(top, user));
            }
            break;
    }
}
