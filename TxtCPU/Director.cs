using System;
using System.Collections.Generic;
using System.Configuration;
using TxtCPU.Repositories;

namespace TxtCPU
{
    public class Director
    {
        private IDictRepository dictRepository;
        private IWordsUtil wordsUtil;

        public Director(IDictRepository dictRepository, IWordsUtil wordsUtil)
        {
            this.dictRepository = dictRepository;
            this.wordsUtil = wordsUtil;
        }

        public void AppStart(string param)
        {
            string file = ConfigurationManager.AppSettings.Get("pathToFile");            

            switch (param)
            {
                case "load":
                    {
                        dictRepository.DeleteDB();
                        Dictionary<string, int> txtWords = wordsUtil.GetWordsFromStream(file);
                        dictRepository.LoadToDB(txtWords);
                        Console.WriteLine("Записи успешно добавлены");
                        break;
                    }
                case "update":
                    {
                        Dictionary<string, int> txtWords = wordsUtil.GetWordsFromStream(file);
                        Dictionary<string, int> dictWords = dictRepository.UnloadDB();
                        txtWords = wordsUtil.ConcatDict(txtWords, dictWords);
                        dictRepository.DeleteDB();
                        dictRepository.LoadToDB(txtWords);
                        Console.WriteLine("Записи успешно обновлены");
                        break;
                    }
                case "delete":
                    {
                        dictRepository.DeleteDB();
                        Console.WriteLine("Все записи удалены");
                        break;
                    }
                case "search":
                    {
                        dictRepository.Search();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Неизвестный параметр");
                        break;
                    }
            }
            
        }
    }
}
