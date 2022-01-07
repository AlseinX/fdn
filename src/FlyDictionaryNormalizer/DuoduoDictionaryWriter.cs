namespace FlyDictionaryNormalizer;

internal readonly record struct DuoduoDictionaryWriter(TextWriter Output) : IDictionaryWriter
{
    public void WriteAppend(string code, string word)
    {
        Output.Write(word);
        Output.Write('\t');
        Output.WriteLine(code);
    }

    public void WritePrepend(string code, string word)
    {
        Output.Write(word);
        Output.Write('\t');
        Output.Write(code);
        Output.WriteLine("#固");
    }

    public void WriteReorder(string code, IEnumerable<(string, bool)> words)
    {
        foreach (var (word, isInsert) in words)
        {
            if (isInsert)
            {
                WriteAppend(code, word);
            }
            else
            {
                Output.Write(word);
                Output.Write('\t');
                Output.Write(code);
                Output.WriteLine("#删");
                Output.Write("$ddcmd(");
                Output.Write(word);
                Output.Write(',');
                Output.Write(word);
                Output.Write(")\t");
                Output.WriteLine(code);
            }
        }
    }
}