namespace OnlineStore.API.ViewModels
{
    public class ImportProductVM
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int ShipmentId { get; set; }
        public string ProductPhotoURL { get; set; }
    }
}
