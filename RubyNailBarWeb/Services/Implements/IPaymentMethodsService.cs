using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Services.Implements
{
    public interface IPaymentMethodsService
    {
        List<PaymentMethod> GetPendingPaymentsService();
        PaymentMethod? GetPaymentMethodByIdService(int paymentMethodId);
        List<PaymentMethod> GetPaymentMethodsService();
    }
}