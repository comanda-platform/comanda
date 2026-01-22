using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class AccountResponseMapper
{
    public static AccountResponse ToResponse(Domain.Entities.Account account)
        => new(
            account.PublicId,
            account.Name,
            account.HasCreditLine,
            account.CreditLimit);
}
