using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.Order_Specs;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<DeliveryMethod> _devliveryRepo;
        //private readonly IGenericRepository<Order> _orderRepo;

        public OrderService(IBasketRepository basketRepository,IUnitOfWork unitOfWork,
            IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket=await _basketRepository.GetBasketAsync(basketId);
            var orderItems = new List<OrderItem>();
            if(basket?.Items?.Count > 0)
            {
                var _productRepo = _unitOfWork.Repository<Product>();
                foreach (var item in basket.Items)
                {
                    var product = await _productRepo.GetByIdAsync(item.Id);
                    var ProductItemOrdered = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(ProductItemOrdered, product.Price, item.Quantity);

                    orderItems.Add(orderItem);
                }
            }
            
            var subTotal = orderItems.Sum(orderItem => orderItem.Price* orderItem.Quantity);

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            var orderRepo = _unitOfWork.Repository<Order>();

            var orderSpec = new OrderWithPaymentSpecifications(basket.PaymentIntentId);

            var existingOrder = await orderRepo.GetWithSpecAsync(orderSpec);
            if(existingOrder != null)
            {
                orderRepo.Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }

            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal,basket.PaymentIntentId);

            await orderRepo.AddAsync(order);

            var result= await _unitOfWork.CompleteAsync();

            if (result <= 0)
                return null;
            
            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryRepo= _unitOfWork.Repository<DeliveryMethod>();
            var deliveryMethods = deliveryRepo.GetAllAsync();
            return deliveryMethods;
        }

        public async Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repository<Order>();
            var spec = new OrderSpecifications(orderId, buyerEmail);
            var order =await orderRepo.GetWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repository<Order>();
            var spec = new OrderSpecifications(buyerEmail);
            var orders =await orderRepo.GetAllWithSpecAsync(spec);
            return orders;
        }
    }
}
