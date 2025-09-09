using RubyNailBarWeb.Models;
using RubyNailBarWeb.Repositories;
using RubyNailBarWeb.Services.Implements;

namespace RubyNailBarWeb.Services
{
    public class PaymentMethodsService : IPaymentMethodsService
    {

        private readonly PaymentMethodRepository _paymentMethodRepository;
        public PaymentMethodsService(PaymentMethodRepository paymentMethodRepository)
        {
            this._paymentMethodRepository = paymentMethodRepository;
        }

        public List<PaymentMethod> GetPaymentMethodsService()
        {
            return _paymentMethodRepository.GetPaymentMethods();
        }

        public PaymentMethod? GetPaymentMethodByIdService(int paymentMethodId)
        {
            return _paymentMethodRepository.GetPaymentMethodById(paymentMethodId);
        }


    }
}
