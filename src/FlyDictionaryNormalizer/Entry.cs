using FlyDictionaryNormalizer;
using YamlDotNet.RepresentationModel;

internal static class Entry
{
    public static void Handle<T>(TextReader input, T writer)
        where T : struct, IDictionaryWriter
    {
        var yaml = new YamlStream();
        yaml.Load(input);

        if (yaml.Documents[0].RootNode is not YamlMappingNode dict)
        {
            throw new FormatException();
        }

        foreach (var (codeNode, word) in dict)
        {
            if (codeNode is not YamlScalarNode { Value: { } code })
            {
                throw new FormatException();
            }

            switch (word)
            {
                case YamlScalarNode { Value: { } value }:
                    WriteSingle(writer, code, value);
                    break;

                case YamlSequenceNode words:
                    var (c, o) = ParsePrefix(code, '$');
                    if (o)
                    {
                        writer.WriteReorder(c, words.Select(node => node is YamlScalarNode { Value: { } value } ?
                            ParsePrefix(value, '+') :
                            throw new FormatException()));
                    }
                    else
                    {
                        foreach (var w in words)
                        {
                            if (w is YamlScalarNode { Value: { } value })
                            {
                                WriteSingle(writer, code, value);
                            }
                        }
                    }
                    break;

                default:
                    throw new FormatException();
            }
        }
    }

    private static void WriteSingle<T>(T writer, string code, string value) where T : struct, IDictionaryWriter
    {
        var (w, i) = ParsePrefix(value, '$');
        if (i)
        {
            writer.WritePrepend(code, w);
        }
        else
        {
            writer.WriteAppend(code, w);
        }
    }

    private static (string, bool) ParsePrefix(string input, char separator)
    => input.StartsWith(separator)
        ? (input[1..], input[1] != separator)
        : (input, false);
}