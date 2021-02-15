using BillsPaymentSystem.App.Core.Contracts;
using BillsPaymentSystem.Data;
using System;

namespace BillsPaymentSystem.App.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandInterpeter commandInterpeter;

        public Engine(ICommandInterpeter commandInterpeter)
        {
            this.commandInterpeter = commandInterpeter;
        }

        public void Run()
        {
            while (true)
            {
                string[] inputParams = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
                {
                    string result = this.commandInterpeter.Read(inputParams, context);
                }

            }
        }
    }
}
