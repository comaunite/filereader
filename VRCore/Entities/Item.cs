using VRCore.Entities.Base;

namespace VRCore.Entities
{
    public class Item : EntityBase
    {
        public string PoNumber { get; set; } = string.Empty;

        public string Isbn { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}
