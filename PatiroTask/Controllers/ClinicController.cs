using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PatiroTask.Data;
using PatiroTask.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PatiroTask.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ClinicController : Controller
    {
        private readonly IClinicData clinicData;
        private readonly IRoleAccessData roleAccessData;

        public ClinicController(IClinicData clinicData, IRoleAccessData roleAccessData)
        {
            this.clinicData = clinicData;
            this.roleAccessData = roleAccessData;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Clinic> Get() => clinicData.GetClinics();

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Clinic Get(int id) => clinicData.GetClinic(id);

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Clinic updatedClinic)
        {
            try
            {
                var clinic = clinicData.GetClinic(id);

                if (clinic != null)
                {
                    //Should test on id instead in production, but easier to test with name
                    if (clinic.CreatedBy == User.Identity.Name)
                    {
                        clinic = updatedClinic;//Set the original clinic to the updated one
                        clinic.Id = id;//Make sure that the id is still correct
                    }
                    else
                    {
                        ////If every user only has one single role
                        //var role = ((ClaimsIdentity)User.Identity).Claims
                        //    .Where(c => c.Type == ClaimTypes.Role)
                        //    .Select(c => c.Value.ToLower())
                        //    .FirstOrDefault();

                        //if (role != null)
                        //{
                        //    //Hard to make it scaleable when you want special logic for a specific role :-)
                        //    if (role != "partner" || clinic.Members.Any(m => m == User.Identity.Name))
                        //    {
                        //        foreach (var item in roleAccessData.GetRoleAccesses(role))
                        //        {
                        //            var prop = typeof(Clinic).GetProperty(item.Property);
                        //            prop.SetValue(clinic, prop.GetValue(updatedClinic));
                        //        }
                        //    }
                        //}

                        ////If every user can have multiple roles
                        var roles = ((ClaimsIdentity)User.Identity).Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value.ToLower());

                        //Can be shortened to this, when all the debugging isn't necessary
                        foreach (var prop in typeof(Clinic).GetProperties())
                            if (roleAccessData.GetRoleAccessesForProperty(prop.Name)
                                .Where(ra => ra.RoleLowered != "partner" || clinic.Members.Any(m => m == User.Identity.Name))
                                .Select(ra => ra.RoleLowered)
                                .Intersect(roles)
                                .Any())
                                prop.SetValue(clinic, prop.GetValue(updatedClinic));
                    }

                    clinicData.UpdateClinic(id, clinic);
                }

            }
            catch (Exception e)
            {
                //Of course do better error-handling
                Debug.WriteLine(e.Message);
            }
        }
    }
}
