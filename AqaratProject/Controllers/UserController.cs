using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BL;
using AqaratProject.Data;

using AqaratProject.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TawredatProject;

namespace AqaratProject.Controllers
{
    public class UserController : Controller
    {
        private readonly Al3QaratContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
       
        public UserController(RoleManager<IdentityRole> RoleManager,UserManager<ApplicationUser> userManager, Al3QaratContext db)
        {
            _userManager = userManager;

            _db = db;
            _roleManager = RoleManager;
        }

        [Authorize(Roles ="Admin")]

        public IActionResult Index()
        {
            var userList = _db.Users.ToList();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            foreach (var user in userList)
            {
                var role = userRole.FirstOrDefault(u => u.UserId == user.Id);
                if (role == null)
                {
                    user.Role = "None";
                }
                else
                {
                    user.Role = roles.FirstOrDefault(u => u.Id == role.RoleId).Name;
                }
            }
            ViewBag.Regions = _userManager.Users.ToList();
            return View();

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string userId)
        {
            ViewBag.cities = _roleManager.Roles.ToList();
            var objFromDb = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (objFromDb == null)
            {
                return NotFound();
            }
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            var role = userRole.FirstOrDefault(u => u.UserId == objFromDb.Id);
            if (role != null)
            {
                objFromDb.RoleId = roles.FirstOrDefault(u => u.Id == role.RoleId).Id;
            }
            objFromDb.RoleList = new List<System.Web.Mvc.SelectListItem>();
            objFromDb.RoleList2 = new List<System.Web.Mvc.SelectListItem>();
            objFromDb.RoleList3 = new List<string>();
            var userRole2 = _db.UserRoles.Where(u => u.UserId == objFromDb.Id).ToList();
            objFromDb.RoleListMain = _roleManager.Roles.ToList();
            foreach (var i in userRole2)
            {
                
                System.Web.Mvc.SelectListItem a = new System.Web.Mvc.SelectListItem();
                a.Value = i.RoleId;
                a.Selected = true;
                a.Text = _db.Roles.Where(u => u.Id == i.RoleId).FirstOrDefault().Name;
                objFromDb.RoleList.Add(a);


            }
           
                foreach(var l in objFromDb.RoleListMain)
                {
                    if(!objFromDb.RoleList.Any(a=> a.Value == l.Id)) 
                    {
                        System.Web.Mvc.SelectListItem a = new System.Web.Mvc.SelectListItem();
                        a.Value = l.Id;
                        a.Selected = false;
                        a.Text = _db.Roles.Where(u => u.Id == l.Id).FirstOrDefault().Name;
                        objFromDb.RoleList.Add(a);
                        objFromDb.RoleList3.Add(a.Text);
                    }
                }
           
            


          
           
            return View(objFromDb);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            var objFromDb = _db.Users.FirstOrDefault(u => u.Id == user.Id);
            objFromDb.FirstName = user.FirstName;
            objFromDb.Email = user.Email;
            objFromDb.RoleList = user.RoleList;
            objFromDb.AccountType = user.AccountType;
            ViewBag.cities = _roleManager.Roles.ToList();
           
              
                    List<string> previousRoleName = new List<string>();
            foreach (var i in objFromDb.RoleList)
            {
                previousRoleName.Add(_db.Roles.Where(u => u.Id == i.Value).FirstOrDefault().Name);
            }
            //removing the old role
            await _userManager.RemoveFromRolesAsync(objFromDb, previousRoleName);
            _db.SaveChanges();
            List<string> newRoleName = new List<string>();
            foreach(var i in objFromDb.RoleList.Where(c => c.Selected)) 
            {
                newRoleName.Add(_db.Roles.Where(u => u.Id == i.Value).FirstOrDefault().Name);
            }


                //add new role
                var a = await _userManager.AddToRolesAsync(objFromDb, newRoleName);
                var b =await _userManager.UpdateAsync(objFromDb);
                _db.SaveChanges();
                if (a.Succeeded == true && b.Succeeded == true )
                {
                    TempData[SD.Success] = "User has been edited successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData[SD.Error] = "User has not been edited successfully.";
                    return View(user);
                }
           
               
           


            
        }




        [HttpPost]
        public IActionResult LockUnlock(string userId)
        {
            var objFromDb = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (objFromDb == null)
            {
                return NotFound();
            }
            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                //user is locked and will remain locked untill lockoutend time
                //clicking on this action will unlock them
                objFromDb.LockoutEnd = DateTime.Now;
                TempData[SD.Success] = "User unlocked successfully.";
            }
            else
            {
                //user is not locked, and we want to lock the user
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
                TempData[SD.Success] = "User locked successfully.";
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public IActionResult Delete(string userId)
        {
            var objFromDb = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (objFromDb == null)
            {
                return NotFound();
            }
            _db.Users.Remove(objFromDb);
            _db.SaveChanges();
            TempData[SD.Success] = "User deleted successfully.";
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            var objFromDb = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (objFromDb == null)
            {
                return NotFound();
            }
            var userRoles = _db.UserRoles.Where(u => u.UserId == objFromDb.Id).ToList();
            List<string> a = new List<string>();
            foreach(var i in userRoles)
            {
                var previousRoleName = _db.Roles.Where(u => u.Id == i.RoleId).Select(e => e.Name).FirstOrDefault();
                a.Add(previousRoleName);
            }
            var b = a;

            var model = new ApplicationUser()
            {
                Id = userId
            };

            //foreach (var i in a)
            //{
            //    UserRole userClaim = new UserRole
            //    {
            //        ClaimType = claim.Type
            //    };
            //    if (existingUserClaims.Any(c => c.Type == claim.Type))
            //    {
            //        userClaim.IsSelected = true;
            //    }
            //    model.Claims.Add(userClaim);
            //}

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var existingUserClaims = await _userManager.GetClaimsAsync(user);

            var model = new UserClaimsViewModel()
            {
                UserId = userId
            };

            foreach (Claim claim in ClaimStore.claimsList)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };
                if (existingUserClaims.Any(c => c.Type == claim.Type))
                {
                    userClaim.IsSelected = true;
                }
                model.Claims.Add(userClaim);
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserClaims(UserClaimsViewModel userClaimsViewModel)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userClaimsViewModel.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var claims = await _userManager.GetClaimsAsync(user);
            var result = await _userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                TempData[SD.Error] = "Error while removing claims";
                return View(userClaimsViewModel);
            }

            result = await _userManager.AddClaimsAsync(user,
                userClaimsViewModel.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.IsSelected.ToString()))
                );

            if (!result.Succeeded)
            {
                TempData[SD.Error] = "Error while adding claims";
                return View(userClaimsViewModel);
            }

            TempData[SD.Success] = "Claims updated successfully";
            return RedirectToAction(nameof(Index));
        }

      
    }
}
