using Comanda.Domain.Extensions;

namespace Comanda.Domain.Helpers
{
    public class PublicIdHelper
    {
        public static string Generate() => Ulid.NewUlid().ToBase58();
    }
}







