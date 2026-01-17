using VRCore.Entities.Base;

namespace VRCore.Entities
{
    public class Box : EntityBase
    {
        public string BoxIdentifier { get; set; } = string.Empty;

        public string SupplierIdentifier { get; set; } = string.Empty;

        public IList<Item> Items { get; set; } = [ ];
    }
}
