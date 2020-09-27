using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TxtCPU.Repositories;

namespace TxtCPU
{
    class Program
    {
        static Container containerInject;

        public static void Main(string[] args)
        {
            containerInject = new Container();
            WriteInjections();

            var director = containerInject.GetInstance<Director>();
            if (args.Length > 0)
            {
                director.AppStart(args[0]);
            }
            else director.AppStart("search");
        }

        public static void WriteInjections()
        {
            containerInject.Register<IDictRepository, DictRepository>();
            containerInject.Register<IWordsUtil, WordsUtil>();
            containerInject.Register<Director>();       

            containerInject.Verify();
        }
    }

  
}
