using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TxtCPU.Repositories
{
    public class DictRepository : IDictRepository
    {
        public void LoadToDB(Dictionary<string, int> DictWords)
        {
            try
            {
                using (DictionContext db = new DictionContext())
                {
                    foreach (KeyValuePair<string, int> kvp in DictWords)
                    {
                        Diction row = new Diction { Word = kvp.Key, Repeat = kvp.Value };
                        db.Dict.Add(row);
                    }
                    db.SaveChanges();                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void DeleteDB()
        {
            try
            {
                using (DictionContext db = new DictionContext())
                {
                    var listAtDb = db.Dict.ToList();

                    db.Dict.RemoveRange(listAtDb);
                    db.SaveChanges();                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public Dictionary<string, int> UnloadDB()
        {
            Dictionary<string, int> wordsUnload = new Dictionary<string, int>();

            try
            {
                using (DictionContext db = new DictionContext())
                {
                    wordsUnload = db.Dict.ToDictionary(t => t.Word, t => t.Repeat);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return wordsUnload;
        }

        public void Search()
        {
            try
            { 
                while (true)
                {
                    string inquiry = string.Empty;
                    ConsoleKeyInfo keyinfo = Console.ReadKey();
                    while (keyinfo.Key != ConsoleKey.Enter)
                    {
                        if (keyinfo.Key == ConsoleKey.Escape) return;

                        inquiry += keyinfo.KeyChar;
                        keyinfo = Console.ReadKey();
                    }                    
                    if (String.IsNullOrEmpty(inquiry)) break;
                    Console.WriteLine("> " + inquiry);
                    int i = 0;
                    foreach (Diction word in InquiryResalt(inquiry))
                    {
                        if (i < 5)
                        {
                            Console.WriteLine("- " + word.Word);
                            i++;
                        }
                        else break;
                    }
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }            
        }

        private Diction[] InquiryResalt(string inquiry)
        {

            try
            {
                int index = inquiry.Length;
                using (DictionContext db = new DictionContext())
                {
                    return db.Dict.Where(x => x.Word.Substring(0, index) == inquiry)
                                     .OrderByDescending(x => x.Repeat)
                                     .ThenBy(x => x.Word).ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }
    }
}
