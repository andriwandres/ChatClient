using AutoMapper;
using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.Dtos.User;
using ChatClient.Core.Models.ViewModels.Contact;
using ChatClient.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatClient.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ContactController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService, IAuthService authService, IMapper mapper)
        {
            _mapper = mapper;
            _authService = authService;
            _contactService = contactService;
        }

        [Authorize]
        [HttpGet("GetContacts")]
        public async Task<ActionResult<IEnumerable<ContactViewModel>>> GetContacts()
        {
            User user = await _authService.GetUser();

            IEnumerable<UserRelationship> relationships = await _contactService.GetContacts(user.UserId);

            IEnumerable<ContactViewModel> contacts = _mapper.Map<IEnumerable<ContactViewModel>>(relationships, options =>
            {
                options.Items["UserId"] = user.UserId;
            });

            return Ok(contacts);
        }

        [Authorize]
        [HttpGet("GetUserByCode")]
        public async Task<ActionResult<ContactSearchViewModel>> GetUserByCode([FromQuery] CodeQueryDto query)
        {
            User user = await _contactService.GetUserByCode(query.Code);

            if (user == null)
            {
                return NotFound();
            }

            ContactSearchViewModel contact = _mapper.Map<User, ContactSearchViewModel>(user);

            return Ok(contact);
        }
    }
}
