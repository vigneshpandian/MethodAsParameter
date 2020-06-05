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
        #region "Variable declarations"

        int callCounter = 1;

        #endregion

        #region "Main method"
        static void Main(string[] args)
        {
            string name = "Vignesh";

            Program oProgram = new Program();

            //prints name
            oProgram.WriteMyName(name);

            //prints name using func
            string Result = oProgram.ExecuteFunction(() => new Program().ReturnMyName("Pandian"));

            Console.WriteLine(Result);

            //prints name using func<t>
            Result = oProgram.Execute<string>(() => oProgram.ReturnMyName("Generic"));

            Console.WriteLine(Result);

            //attempts to prints name using func<t> for 3 times

            Result = oProgram.TryExecute<string>(() => oProgram.RetrunNameAfterMultipeTries("Atlast executed"), 3);

            Console.WriteLine(Result);

            oProgram.callCounter = 1; //resets counter to test the below method

            //attempts to print name using action as func doesn't accept void methods

            oProgram.TryExecute(() => oProgram.PrintNameAfterMultipleTimes("Void method atlast executed"), 3);

            Console.Read(); //just to read the console
        }

        #endregion

        #region "Methods"

        #region "Test Methods"
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

        public void PrintNameAfterMultipleTimes(string name)
        {
            if (callCounter >= 3)
            {
                Console.WriteLine( name);
            }
            else
            {
                callCounter++;
                throw new Exception("I'm not satisfied with the result");
            }
        }

        #endregion

        #region "Func & Actions"

        /// <summary>
        /// Executes the passed function
        /// </summary>
        public string ExecuteFunction(Func<string> func)
        {
            return func();
        }

        /// <summary>
        /// Executes the passed function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Executes the function for the specified no of times
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">Function which returns void</param>
        /// <param name="howManyTimes">"n" of times</param>
        public void TryExecute(Action action, int howManyTimes)
        {
            bool isSuccess = false;

            int attemptMade = 0;

            do
            {
                try
                {
                    attemptMade++;

                    Console.WriteLine("Attempt :" + attemptMade);

                    action();

                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (!isSuccess && attemptMade < howManyTimes);
        }

        #endregion

        #endregion
    }
}
