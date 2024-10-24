using BarberBoss.Application.UseCases.Invoice;
using BarberBoss.Communication.Enums;
using BarberBoss.Exception;
using CommonTestsUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Invoice.Register;

public class RegisterInvoiceValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new InvoiceValidator();
        var request = RequestInvoiceJsonBuilder.Build();

        var result = validator.Validate(request);

        Assert.NotNull(result);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        var validator = new InvoiceValidator();
        var request = RequestInvoiceJsonBuilder.Build();
        request.Title = title;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
    }

    [Fact]
    public void Error_Date_Future()
    {
        var validator = new InvoiceValidator();
        var request = RequestInvoiceJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.BILLING_CANNOT_FOR_THE_FUTURE));
    }

    [Fact]
    public void Error_PaymentType_Invalid()
    {
        var validator = new InvoiceValidator();
        var request = RequestInvoiceJsonBuilder.Build();
        request.PaymentType = (PaymentType)700;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-7)]
    [InlineData(-10)]
    public void Error_Amount_Invalid(decimal amount)
    {
        var request = RequestInvoiceJsonBuilder.Build();
        var validator = new InvoiceValidator();
        request.Amount = amount;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }
}
