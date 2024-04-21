using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignatureAPI.API_DAL;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;

namespace SignatureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignatureController : ControllerBase
    {
        private readonly SignatureAPI_DAL _DAL;

        public SignatureController(SignatureAPI_DAL dAL)
        {
            _DAL = dAL;
        }

        [HttpPost]
        public IActionResult Create(Profile model)
        {
            bool result = _DAL.SignatureGeneration(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IEnumerable<Profile> Get(string email)
        {
            return _DAL.Get(email).ToList();
        }
    }
}
