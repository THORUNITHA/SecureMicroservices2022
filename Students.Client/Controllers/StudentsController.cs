using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Students.Client.ApiServices;
using Students.Client.Data;
using Students.Client.Models;

namespace Students.Client.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly IStudentApiService _studentApiService;


        public StudentsController(IStudentApiService studentApiService)
        {
            _studentApiService = studentApiService ?? throw new ArgumentNullException(nameof(studentApiService));
        }


        // GET: Students


        public async Task<IActionResult> Index()
        {
            await LogTokenAndClaims();
            var students = await _studentApiService.GetStudents();
            students = FilterMovies(students.ToList());

            return View(students);

        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> OnlyAdmin()
        {
            var userInfo = await _studentApiService.GetUserInfo();
            return View(userInfo);
        }
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }




        public async Task LogTokenAndClaims()
        {
            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            Debug.WriteLine($"Identity token: {identityToken}");

            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            return View();
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,Rating,ReleaseDate,ImageUrl,Owner")] Student student)
        {

            return View();
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            return View();
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Rating,ReleaseDate,ImageUrl,Owner")] Student student)
        {

            return View();
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            return View();
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            return View();
        }

        private bool StudentExists(int id)
        {
            return true;
        }
        private List<Student> FilterMovies(List<Student> students)
        {
            return students.FindAll(m => m.Owner.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
