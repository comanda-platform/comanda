namespace Comanda.Application.UseCases
{
    public class UseCaseBase(string entityTypePrintName)
    {
        public required string EntityTypePrintName { get; set; } = entityTypePrintName;
    }
}







