using BillsPaymentSystem.App.Core;
using BillsPaymentSystem.App.Core.Contracts;
using BillsPaymentSystem.Data;
using System;

namespace BillsPaymentSystem.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            //using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
            //{
            //    DbInitializer.Seed(context);
            //}

            ICommandInterpeter commandInterpeter = new CommandInterpeter();

            IEngine engine = new Engine(commandInterpeter);
            engine.Run();
        }
    }
}
