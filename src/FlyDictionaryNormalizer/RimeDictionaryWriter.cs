namespace FlyDictionaryNormalizer;

internal readonly record struct RimeDictionaryWriter(TextWriter Top, TextWriter User) : IDictionaryWriter
{
    public void WriteAppend(string code, string word)
    {
        User.Write(word);
        User.Write('\t');
        User.WriteLine(code);
    }

    public void WritePrepend(string code, string word)
    {
        Top.Write(word);
        Top.Write('\t');
        Top.Write(code);
        Top.WriteLine("\t1000");
    }

    public void WriteReorder(string code, IEnumerable<(string, bool)> words)
    {
        var i = 1000;
        foreach (var (word, _) in words)
        {
            Top.Write(word);
            Top.Write('\t');
            Top.Write(code);
            Top.Write('\t');
            Top.WriteLine(i);
            i--;
        }
    }
}
