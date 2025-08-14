using OrderManagement.BLL.DTO;
using OrderManagement.BLL.Interfaces;
using OrderManagement.DATA.Repositories.Repositories;
using OrderManagement.DATA.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagement.DATA.Entities;
using OrderManagement.BLL.DTO.Product;
using OrderManagement.BLL.DTO.Order;

namespace OrderManagement.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly IDiscountService _discountService;

        public OrderService(IOrderRepository orderRepository, IProductService productService, IDiscountService discountService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
            _discountService = discountService;
        }

        public async Task<ResponseOrderDto> CreateOrder(OrderDto orderDto)
        {
            Order newOrder = new Order();
            var totalPrice = await TotalPrice(orderDto.Items);
            var items = await AddItemsToOrder(orderDto, newOrder);

            newOrder.OrderDate = DateTime.UtcNow;
            newOrder.TotalPrice = totalPrice;
            newOrder.TotalItems = items.Count;
            newOrder.ItemsInOrder = items;

            await _orderRepository.CreateOrder(newOrder);

            return new ResponseOrderDto
            {
                Id = newOrder.Id,
                OrderDate = newOrder.OrderDate,
                TotalPrice = newOrder.TotalPrice,
                TotalItems = newOrder.TotalItems,
                Items = newOrder.ItemsInOrder.Select(item => new OrderItemDto
                {
                    ItemName = item.Product.Name,
                    Quantity = item.Quantity
                }).ToList()
            };
        }

        public async Task<ResponseOrderDto> DeleteOrderById(int id)
        {
            var order = await _orderRepository.GetOrderById(id);

            if(order == null)
            {
                throw new Exception("Order was not found");
            }

            var responseOrderDto = new ResponseOrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                TotalItems = order.TotalItems,
                Items = order.ItemsInOrder.Select(item => new OrderItemDto
                {
                    ItemName = item.Product.Name,
                    Quantity = item.Quantity
                }).ToList()
            };

            await _orderRepository.DeleteOrder(order);

            return responseOrderDto;
        }

        public async Task<ResponseOrderDto> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderById(id);

            if(order == null)
            {
                throw new Exception($"There is no order with id: {id}");
            }

            return new ResponseOrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                TotalItems = order.TotalItems,
                Items = order.ItemsInOrder.Select(item => new OrderItemDto
                {
                    ItemName = item.Product.Name,
                    Quantity = item.Quantity
                }).ToList()
            };
        }

        private async Task<double> TotalPrice(List<OrderItemDto> Items)
        {
            double totalAmount = 0.0;
            foreach (var item in Items)
            {
                ResponseProductDto product = await _productService.GetProductByName(item.ItemName);
                var discount = _discountService.ApplyDiscount(item.Quantity, product);
                
                totalAmount += (item.Quantity * (double) product.Price) * discount;
                //Console.WriteLine($"Quantity: {item.Quantity}, Price: {product.Price}, Discount: {product.Discount.Percentage / 100}");

            }
            return totalAmount;
        }

        private async Task<List<OrderItem>> AddItemsToOrder(OrderDto orderDto, Order newOrder)
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            foreach(var orderItemDto in orderDto.Items)
            {
                var product = await _productService.GetProductByName(orderItemDto.ItemName);
                OrderItem orderItem = new OrderItem
                {
                    Order = newOrder,
                    ProductId = product.Id,
                    DiscountId = product.DiscountId,
                    Discount = product.Discount,
                    Quantity = orderItemDto.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = orderItemDto.Quantity * (double) product.Price
                };

                orderItems.Add(orderItem);
            }

            return orderItems;
        }
    }
}
