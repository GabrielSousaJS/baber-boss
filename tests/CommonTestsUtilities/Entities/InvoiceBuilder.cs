using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Enums;
using Bogus;

namespace CommonTestsUtilities.Entities;

public class InvoiceBuilder
{
    public static List<Invoice> Collection(User user, uint count = 2)
    {
        var list = new List<Invoice>();

        if (count == 0)
            count = 1;

        var expenseId = 1;

        for (int i = 0; i < count; i++) {   
            var invoice = Build(user);

            invoice.Id = expenseId++;
            list.Add(invoice);
        }

        return list;
    }

    public static Invoice Build(User user)
    {
        return new Faker<Invoice>()
            .RuleFor(invoice => invoice.Title, faker => faker.Commerce.ProductName())
            .RuleFor(invoice => invoice.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(invoice => invoice.Date, faker => faker.Date.Past())
            .RuleFor(invoice => invoice.Amount, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(invoice => invoice.PaymentType, faker => faker.PickRandom<PaymentType>())
            .RuleFor(Invoice => Invoice.UserId, _ => user.Id);
    }
}
