using BethaniesPieShope.DBAccess;
using BethaniesPieShope.Models;

namespace BethaniesPieShope.Repos;

public class OrderRepository : IOrderReopository
{
    private readonly BethaniesPieDbContext _bethaniesPieDbContext;

    public OrderRepository(BethaniesPieDbContext bethaniesPieDbContext)
    {
        _bethaniesPieDbContext = bethaniesPieDbContext;
    }

    public void CreateOrder(Order order)
    {
        order.OrderDate = DateTime.UtcNow;
        order.OrderTotal= order.orderDetails
                           .Sum(o => o.Price * o.Amount);
        _bethaniesPieDbContext.Orders.Add(order);
        _bethaniesPieDbContext.SaveChanges();
    }
}
