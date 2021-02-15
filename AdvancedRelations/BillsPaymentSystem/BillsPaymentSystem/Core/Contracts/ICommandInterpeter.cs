using BillsPaymentSystem.Data;

namespace BillsPaymentSystem.App.Core.Contracts
{
    public interface ICommandInterpeter
    {
        string Read(string[] args, BillsPaymentSystemContext context);
    }
}
