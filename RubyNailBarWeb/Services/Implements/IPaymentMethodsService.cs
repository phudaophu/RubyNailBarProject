using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Services.Implements
{
    public interface IPaymentMethodsService
    {
        PaymentMethod? GetPaymentMethodByIdService(int paymentMethodId);
        List<PaymentMethod> GetPaymentMethodsService();
    }
}