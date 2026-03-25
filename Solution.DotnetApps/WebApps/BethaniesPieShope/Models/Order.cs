namespace BethaniesPieShope.Models;

public class Order
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Pincode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Emailid { get; set; } = string.Empty;
    public decimal OrderTotal { get; set; } 
    public DateTime OrderDate { get; set; }
    public List<OrderDetail> orderDetails { get; set; } = default!;
}