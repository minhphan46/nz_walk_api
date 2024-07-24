using System.ComponentModel.DataAnnotations;

namespace NZWalks.Entities.Base
{
    [InterfaceType("Base")]
    public interface IBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
