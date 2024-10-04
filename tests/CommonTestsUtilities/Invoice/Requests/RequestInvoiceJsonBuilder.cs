using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Invoice.Requests;
using Bogus;

namespace CommonTestsUtilities.Invoice.Requests;

public class RequestInvoiceJsonBuilder
{
    public static RequestInvoiceJson Build()
    {
        return new Faker<RequestInvoiceJson>()
            .RuleFor(request => request.Title, faker => faker.Commerce.ProductName())
            .RuleFor(request => request.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(request => request.Date, faker => faker.Date.Past())
            .RuleFor(request => request.PaymentType, faker => faker.PickRandom<PaymentType>())
            .RuleFor(request => request.Amount, faker => faker.Random.Decimal(min: 1, max: 1000));
            
    }
}
