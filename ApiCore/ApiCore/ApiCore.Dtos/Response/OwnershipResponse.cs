using ApiCore.DomainModel;

namespace ApiCore.Dtos.Response
{
    public class OwnershipResponse
    {
        public virtual int Id { get; set; }
        public virtual Address Address { get; set; }
    }
}
