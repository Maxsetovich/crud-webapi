using crud_webapi.Data;
using crud_webapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud_webapi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : Controller
{
    private readonly UserApiDbContext dbContext;

    public UsersController(UserApiDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await dbContext.Users.ToListAsync());
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        var user = await dbContext.Users.FindAsync(id);
        
        if(user == null) 
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            FullName = addUserRequest.FullName,
            Country = addUserRequest.Country,
            TeacherFullName = addUserRequest.TeacherFullName,
            ContractDate = (addUserRequest.ContractDate).AddDays(1),
            ContractMoney = addUserRequest.ContractMoney,
            Status = addUserRequest.Status
        };

        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        return Ok(user);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id, UpdateUserRequest updateUserRequest)
    {
        var user = await dbContext.Users.FindAsync(id);

        if (user != null)
        {
            user.FullName = updateUserRequest.FullName;
            user.Country = updateUserRequest.Country;
            user.TeacherFullName = updateUserRequest.TeacherFullName;
            user.ContractDate = updateUserRequest.ContractDate.AddDays(1);
            user.ContractMoney = updateUserRequest.ContractMoney;
            user.Status = updateUserRequest.Status;

            await dbContext.SaveChangesAsync();

            return Ok(user);
        }
        return NotFound();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        var user = await dbContext.Users.FindAsync(id);
        
        if (user != null)
        {
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return Ok(user);
        }

        return NotFound();
    }
}
