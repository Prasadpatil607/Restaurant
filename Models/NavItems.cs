namespace RestaurantDemo.Models
{
    public class NavItems
    {
        public int Id { get; set; }
        public string NavItem { get; set; }
       public int OrderNumber { get; set; }
        public int ParentId { get; set; }
        public bool IsHeader { get; set; }
        public bool IsFooter { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
