using BethanysHRM.App.Services.Contracts;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BethanysHRM.App.Pages;

public partial class EmployeeEdit
{
    [Inject]
    public IEmployeeService EmployeeService { get; set; }
    [Inject]
    public IJobCategoryService JobCategoryService { get; set; }
    [Inject]
    public ICountryServices CountryServices { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = new Employee();
    public IEnumerable<Country> Countries { get; set; } = new List<Country>();
    public IEnumerable<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

    protected bool Saved;
    protected string StatusClass = string.Empty;
    protected string Message = string.Empty;
    protected bool IsLoading;

    private IBrowserFile ProfileImage = default!;
    protected override async Task OnParametersSetAsync()
    {

        try
        {
            Saved = false;
            IsLoading = true;
            JobCategories = await JobCategoryService.GetJobCategories();
            Countries = await CountryServices.GetCountries();

            if (EmployeeId == 0)
            {
                Employee = new Employee { CountryId = 1, JobCategoryId = 1, JoinedDate = DateTime.UtcNow, BirthDate = DateTime.UtcNow.AddYears(-20) };
            }
            else
            {
                Employee = await EmployeeService.GetEmployeeDetails(EmployeeId);
            }
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task HandleValidSubmit()
    {
        Saved = false;
        //crete a new employee
        if (EmployeeId == 0)
        {
            //add profile image.
            if(ProfileImage is not null)
            {
                var file = ProfileImage;
                var stream = file.OpenReadStream();
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                stream.Close();

                Employee.ImageContent = ms.ToArray();
                Employee.ImageName = file.Name;
            }

            var newemployee = await EmployeeService.AddEmployee(Employee);
            if (newemployee != null)
            {
                StatusClass = "alert-success";
                Message = "New employee added successfully.";
                Saved = true;
            }
            else
            {
                StatusClass = "alert-danger";
                Message = "Something went wrong adding the new employee. Please try again.";
                Saved = false;
            }

        }
        else
        {
            await EmployeeService.UpdateEmployee(Employee);
            StatusClass = "alert-success";
            Message = "Employee updated successfully.";
            Saved = true;
        }
    }

    public void HandleInvalidSubmit()
    {
        StatusClass = "alert-danger";
        Message = "There are some validation errors. Please try again.";
    }

    public async Task DeleteEmployee()
    {
        await EmployeeService.DeleteEmployee(EmployeeId);
        StatusClass = "alert-success";
        Message = "Deleted successfully";

        Saved = true;
    }

    public void NavigateToOverview()
    {
        NavigationManager.NavigateTo("/employeeoverview");
    }

    public void OnInputFileChange(InputFileChangeEventArgs e)
    {
        ProfileImage = e.File;
        StateHasChanged();
    }
}
