using System.Collections.Generic;

namespace TxtCPU
{
    public interface IWordsUtil
    {
        Dictionary<string, int> GetWordsFromStream(string file);
        Dictionary<string, int> ConcatDict(Dictionary<string, int> d1, Dictionary<string, int> d2);
    }
}