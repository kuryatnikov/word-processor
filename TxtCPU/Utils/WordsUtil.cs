using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace TxtCPU
{
    public class WordsUtil : IWordsUtil
    {
        public Dictionary<string, int> GetWordsFromStream(string file)
        {
            Dictionary<string, int> wordsForLoad = new Dictionary<string, int>();

            try
            {
                Dictionary<string, int> words = new Dictionary<string, int>();

                StreamReader streamReader = new StreamReader(file, Encoding.Default);
                while (!streamReader.EndOfStream)
                {
                    string str = streamReader.ReadLine();
                    if (str != "")
                    {
                        string[] strmas = str.Split(' ');
                        foreach (string word in strmas)
                        {
                            string wordTr = word.Trim(new char[] { '.', ',', '?', '!', ':', ';', '"' }).ToLower();

                            if (wordTr.Length > 2 && word.Length < 15)
                            {
                                if (!words.ContainsKey(wordTr))
                                {
                                    words.Add(wordTr, 1);
                                }
                                else words[wordTr]++;
                            }
                        }
                    }
                }
                streamReader.Close();

                foreach (KeyValuePair<string, int> kvp in words)
                {
                    if (kvp.Value > 2)
                    {
                        wordsForLoad.Add(kvp.Key, kvp.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return wordsForLoad;
        }
        public Dictionary<string, int> ConcatDict(Dictionary<string, int> d1, Dictionary<string, int> d2)
        {
            try
            {                
                {
                    d2 = d1.Concat(d2)
                            .GroupBy(kvp => kvp.Key, (key, kvps) => new { Key = key, Value = kvps.Sum(kvp => kvp.Value) })
                            .ToDictionary(g => g.Key, g => g.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return d2;
        }

    }
}
