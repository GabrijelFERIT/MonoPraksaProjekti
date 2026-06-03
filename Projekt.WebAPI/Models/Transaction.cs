namespace Projekt.WebAPI.Models
{
    public class Transaction
    {

        long Id { get; set; }

        long CustomerId { get; set; }

        long EmployerId { get; set; }

        long BillingId { get; set; }

        int NumberOfProducts { get; set; }

    }
}
