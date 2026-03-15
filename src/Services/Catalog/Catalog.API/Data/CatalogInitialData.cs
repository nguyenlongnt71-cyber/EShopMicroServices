using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;
            // Marten upsert will cater for existing records 
            session.Store<Product>(GetPreconfiguredProduct());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetPreconfiguredProduct() => new List<Product>
        {
            new Product()
    {
        Id = Guid.NewGuid(),
        Name = "Wireless Mouse",
        Category = new List<string> { "Electronics", "Accessories" },
        Description = "Ergonomic wireless mouse with USB receiver",
        ImageFile = "mouse.jpg",
        Price = 19.99m
    },
    new Product()
    {
        Id = Guid.NewGuid(),
        Name = "Mechanical Keyboard",
        Category = new List<string> { "Electronics", "Accessories" },
        Description = "RGB mechanical keyboard with blue switches",
        ImageFile = "keyboard.jpg",
        Price = 79.50m
    },
    new Product()
    {
        Id = Guid.NewGuid(),
        Name = "Gaming Headset",
        Category = new List<string> { "Electronics", "Audio" },
        Description = "Surround sound gaming headset with microphone",
        ImageFile = "headset.jpg",
        Price = 59.99m
    },
    new Product()
    {
        Id = Guid.NewGuid(),
        Name = "Laptop Stand",
        Category = new List<string> { "Office", "Accessories" },
        Description = "Adjustable aluminum laptop stand",
        ImageFile = "laptopstand.jpg",
        Price = 29.99m
    },
    new Product()
    {
        Id = Guid.NewGuid(),
        Name = "USB-C Hub",
        Category = new List<string> { "Electronics", "Adapters" },
        Description = "Multiport USB-C hub with HDMI and USB 3.0",
        ImageFile = "usbhub.jpg",
        Price = 34.90m
    }
        };
    }
}
