namespace Projekt.WebAPI.Models
{
    public class BillingInformation
    {

        long Id { get; set; }

        long CreditCardNumber { get; set; }

        short SecurityCode { get; set; }

        short ExpirationMonth { get; set; }

        short ExpirationYear { get; set; }

        Customer Customer { get; set; } = new Customer();

        long CustomerId { get; set; }

    }
}
