namespace crud_webapi.Models;

public class UpdateUserRequest
{
    public string FullName { get; set; } = String.Empty;
    public string Country { get; set; } = String.Empty;
    public string TeacherFullName { get; set; } = String.Empty;
    public DateTime ContractDate { get; set; }
    public decimal ContractMoney { get; set; }
    public string Status { get; set; } = String.Empty;
}
