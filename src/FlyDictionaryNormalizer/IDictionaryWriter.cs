namespace FlyDictionaryNormalizer;

internal interface IDictionaryWriter
{
    void WriteAppend(string code, string word);

    void WritePrepend(string code, string word);

    void WriteReorder(string code, IEnumerable<(string, bool)> words);
}