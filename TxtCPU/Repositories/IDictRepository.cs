using System.Collections.Generic;

namespace TxtCPU.Repositories
{
    public interface IDictRepository
    {
        void DeleteDB();
        void LoadToDB(Dictionary<string, int> DictWords);
        Dictionary<string, int> UnloadDB();
        void Search();
    }
}