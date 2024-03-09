using VRCore.Entities.Base;

namespace VRCore.Entities
{
    public class Box : EntityBase
    {
        public string BoxIdentifier { get; set; }

        public string SupplierIdentifier { get; set; }

        public IList<Item> Items { get; set; }

        public Box()
        {
            BoxIdentifier = string.Empty;
            SupplierIdentifier = string.Empty;
            Items = new List<Item>();
        }
    }
}
