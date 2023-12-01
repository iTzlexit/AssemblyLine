namespace AssemblyLine.ApplicationLayer.Operations.Interfaces
{
    public interface IGetTheDefaultOrderOfOperationUseCase
    {
        Task<int?> ExecuteAsyc(int assemblyId); 
    }
}