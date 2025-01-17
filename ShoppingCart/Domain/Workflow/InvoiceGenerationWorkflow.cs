using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using ShoppingCart.Domain.Models;

public class InvoiceGenerationWorkflow
{
    private const string InvoiceDirectory = "Invoices";

    public void GenerateInvoice(CheckedOutShoppingCart checkedOutCart)
    {
        if (checkedOutCart == null)
            throw new ArgumentNullException(nameof(checkedOutCart), "CheckedOutShoppingCart cannot be null.");

        if (!Directory.Exists(InvoiceDirectory))
        {
            Directory.CreateDirectory(InvoiceDirectory);
        }

        var invoiceData = new
        {
            InvoiceId = Guid.NewGuid(),
            OrderId = checkedOutCart.Id,
            UserId = checkedOutCart.UserId,
            IssuedDate = DateTime.UtcNow,
            TotalAmount = checkedOutCart.TotalAmount.Amount,
            ShippingAddress = $"{checkedOutCart.ShippingAddress.Street}, {checkedOutCart.ShippingAddress.City}, {checkedOutCart.ShippingAddress.Country}",
            Items = checkedOutCart.Items.Select(item => new
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                TotalPrice = item.TotalPrice.Amount
            })
        };

        var invoiceJson = JsonSerializer.Serialize(invoiceData, new JsonSerializerOptions { WriteIndented = true });

        var fileName = Path.Combine(InvoiceDirectory, $"Invoice_{checkedOutCart.Id}.json");
        File.WriteAllText(fileName, invoiceJson);

        Console.WriteLine($"Invoice generated and saved to {fileName}");
    }
}
