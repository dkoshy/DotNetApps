using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BethaniesPieShope.ViewModels;

public class OrderViewModel
{
    [Required(ErrorMessage = "Please eneter first name.")]
    [StringLength(50)]
    [DisplayName("First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please eneter first name.")]
    [StringLength(50)]
    [DisplayName("First Name")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your address")]
    [StringLength(100)]
    [Display(Name = "Address Line 1")]
    public string AddressLine1 { get; set; } = string.Empty;

    [Display(Name = "Address Line 2")]
    public string AddressLine2 { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your city")]
    [StringLength(50)]
    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your country")]
    [StringLength(50)]
    public string Country { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your pin code")]
    [Display(Name = "pin code")]
    [StringLength(10, MinimumLength = 4)]
    public string Pincode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your phone number")]
    [StringLength(25)]
    [Display(Name = "Phone Number")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your Email address")]
    [StringLength(50)]
    [Display(Name = "Email address")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "The email address is not entered in a correct format")]
    public string Emailid { get; set; } = string.Empty;

}
