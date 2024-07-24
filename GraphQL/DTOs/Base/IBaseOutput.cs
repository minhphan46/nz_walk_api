using System.ComponentModel.DataAnnotations;

namespace NZWalks.GraphQL.DTOs.Base
{
    [InterfaceType("BaseOutput")]
    public interface IBaseOutput
    {
        [Key]
        public Guid Id { get; set; }
    }
}
