using VRCore.Entities.Base;

namespace VRCore.Entities
{
    public class Item : EntityBase
    {
        public string PoNumber { get; set; }

        public string Isbn { get; set; }

        public int Quantity { get; set; }

        public Item()
        {
            PoNumber = string.Empty;
            Isbn = string.Empty;
        }
    }
}
