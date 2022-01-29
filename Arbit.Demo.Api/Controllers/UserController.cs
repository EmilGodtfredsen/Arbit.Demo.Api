using Arbit.Demo.Api.Models;
using Arbit.Demo.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arbit.Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //Create user /api/user

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Create(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Creation of user failed.");
                }
                var newUser = await _userService.CreateUser(user);
                return Ok(newUser);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        //Get All users /api/user

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                if (users == null)
                {
                    return Problem("Returned null, unexpected behavior");
                }
                else if (users.Count == 0)
                {
                    return NoContent();
                }
                return Ok(users);
            }
            catch (Exception e)
            {

                return Problem(e.Message);
            }
        }

        // Get by id /api/user

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        //Update user /api/user

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Update([FromRoute] int id, User user)
        {
            try
            {
                var updateUser = await _userService.UpdateUser(id, user);

                if (updateUser == null)
                {
                    return NotFound("Editing of user not possible, User = null.");
                }
                return Ok(updateUser);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }


        //Delete user/api/user

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var deleteUser = await _userService.DeleteUser(id);

                if (deleteUser == null)
                {
                    return NotFound("User with id: " + id + " does not exist");
                }
                return Ok(deleteUser);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

    }
}
