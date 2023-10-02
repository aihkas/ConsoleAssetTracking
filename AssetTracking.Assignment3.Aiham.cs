using System;
using System.Collections.Generic;
using System.Linq;

public enum AssetType
{
    Laptop,
    Phone
}

public enum OfficeLocation
{
    USA,
    UK,
    India
}

public class Asset
{
    public AssetType Type { get; set; }
    public string Brand { get; set; }
    public string ModelName { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal PriceInUSD { get; set; }
    public OfficeLocation Office { get; set; }

    public Asset(AssetType type, string brand, string modelName, DateTime purchaseDate, decimal priceInUSD, OfficeLocation office)
    {
        Type = type;
        Brand = brand;
        ModelName = modelName;
        PurchaseDate = purchaseDate;
        PriceInUSD = priceInUSD;
        Office = office;
    }

    public string GetFormattedPrice()
    {
        switch (Office)
        {
            case OfficeLocation.USA: return $"${PriceInUSD}";
            case OfficeLocation.UK: return $"£{PriceInUSD * 0.75M}";  // example conversion rate
            case OfficeLocation.India: return $"₹{PriceInUSD * 75M}"; // example conversion rate
            default: return PriceInUSD.ToString();
        }
    }

    public string GetCurrencySymbol()
    {
        switch (Office)
        {
            case OfficeLocation.USA: return "USD";
            case OfficeLocation.UK: return "GBP";
            case OfficeLocation.India: return "INR";
            default: return "USD";
        }
    }
}

public class Program
{
    public static List<Asset> GetSampleData()
    {
        return new List<Asset>
        {
            new Asset(AssetType.Laptop, "Apple", "MacBook", DateTime.Now.AddYears(-1), 1200, OfficeLocation.USA),
            new Asset(AssetType.Phone, "Apple", "iPhone", DateTime.Now.AddYears(-3).AddMonths(2), 1000, OfficeLocation.UK),
            new Asset(AssetType.Phone, "Samsung", "Galaxy", DateTime.Now.AddYears(-2), 800, OfficeLocation.India)
        };
    }

    public static void Main()
    {
        List<Asset> assets = new List<Asset>();

        // Accept asset input from the user
        Console.WriteLine("Enter the number of assets you want to add: (Enter 0 to test with sample data)");
        int numAssets = int.Parse(Console.ReadLine());


        //Fill sample data if the user enters 0
        if (numAssets == 0)
        {
            assets.AddRange(GetSampleData());
        }

        //Get user input and validate on each step
        else
        {
            for (int i = 0; i < numAssets; i++)
        {
            Console.WriteLine($"Enter details for asset {i + 1}:");

            Console.WriteLine("Enter Asset Type (Laptop/Phone):");
            AssetType type;
            while (!Enum.TryParse(Console.ReadLine(), true, out type) || !Enum.IsDefined(typeof(AssetType), type))
            {
                Console.WriteLine("Invalid type. Please enter either 'Laptop' or 'Phone':");
            }

            Console.WriteLine("Enter Brand:");
            string brand = Console.ReadLine();

            Console.WriteLine("Enter Model:");
            string model = Console.ReadLine();

            Console.WriteLine("Enter Purchase Date (MM/dd/yyyy):");
            DateTime purchaseDate;
            while (!DateTime.TryParse(Console.ReadLine(), out purchaseDate))
            {
                Console.WriteLine("Invalid date format. Please enter in MM/dd/yyyy format:");
            }

            Console.WriteLine("Enter Price in USD:");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
            {
                Console.WriteLine("Invalid price. Please enter a valid positive price:");
            }

            Console.WriteLine("Enter Office Location (USA/UK/India):");
            OfficeLocation office;
            while (!Enum.TryParse(Console.ReadLine(), true, out office) || !Enum.IsDefined(typeof(OfficeLocation), office))
            {
                Console.WriteLine("Invalid location. Please enter either 'USA', 'UK', or 'India':");
            }

            assets.Add(new Asset(type, brand, model, purchaseDate, price, office));
        }
       }


        //sort
        assets = assets.OrderBy(a => a.Office)
                       .ThenBy(a => a.Type)
                       .ThenBy(a => a.PurchaseDate)
                       .ToList();

        //Print display table

        Console.WriteLine("\n\nAssets:");
        Console.WriteLine("Type    - Brand  - Model     - Purchase Date - Price in USD - Currency - Local Price Today");
        Console.WriteLine("--------------------------------------------------------------------------------------");

        foreach (var asset in assets)
        {
            Console.WriteLine($"{asset.Type,-8}  {asset.Brand,-6} {asset.ModelName,-9} {asset.PurchaseDate.ToShortDateString(),-13} ${asset.PriceInUSD,-12} {asset.GetCurrencySymbol(),-8} {asset.GetFormattedPrice()}");

        }
    }
}
