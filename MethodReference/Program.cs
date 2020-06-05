using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodReference
{
    class Program
    {
        int callCounter = 1;
        static void Main(string[] args)
        {
            string name = "Vignesh";

            Program oProgram = new Program();

            oProgram.WriteMyName(name);

            string Result = oProgram.ExecuteFunction(() => new Program().ReturnMyName("Pandian"));

            Console.WriteLine(Result);

            Result = oProgram.Execute<string>(() => oProgram.ReturnMyName("Generic"));

            Console.WriteLine(Result);

            Result = oProgram.TryExecute<string>(() => oProgram.RetrunNameAfterMultipeTries("Atlast executed"), 3);

            Console.WriteLine(Result);

            Console.Read();
        }

        public void WriteMyName(string name)
        {
            Console.WriteLine(name);
        }

        public string ReturnMyName(string name)
        {
            return name;
        }

        public string RetrunNameAfterMultipeTries(string name)
        {
            if (callCounter >= 3)
            {
                return name;
            }
            else
            {
                callCounter++;
                throw new Exception("I'm not satisfied with the result");
            }
        }

        public string ExecuteFunction(Func<string> func)
        {
            return func();
        }

        public T Execute<T>(Func<T> func)
        {
            return func();
        }

        /// <summary>
        /// Executes the function for the specified no of times
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">Function name</param>
        /// <param name="howManyTimes">"n" of times</param>
        /// <returns></returns>
        public T TryExecute<T>(Func<T> func, int howManyTimes)
        {
            T result = default(T);

            int attemptMade = 0;

            bool isSuccess = false;

            do
            {
                try
                {
                    attemptMade++;

                    Console.WriteLine("Attempt :" + attemptMade);

                    result = func();

                    isSuccess = true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (!isSuccess && attemptMade < howManyTimes);

            return result;
        }

    }
}
