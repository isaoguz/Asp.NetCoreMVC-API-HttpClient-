namespace NLayer.Core.Models
{
    //Buna baseentity vermeye gerek yok zaten Productı kullanıyor.
    public class ProductFeature
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
