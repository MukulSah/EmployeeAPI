using EmployeeDBAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeDBAPI.Controllers
{
    [BasicAuthentication]
    public class EmployeeController : ApiController
    {
        EmployeeTableMKSEntities _dbcontext = new EmployeeTableMKSEntities();

        [Route("api/omnie/v1/Check")]
        [HttpGet]
        public HttpResponseMessage CheckAPIStatus()
        {
            return Request.CreateResponse(HttpStatusCode.OK, $"Services are availabel");
        }
         

        [Route("api/omnie/v1/Get")]
        [HttpGet]
        public IHttpActionResult GetAllEmployeeDetail()
        {
            var query = (from E in _dbcontext.Employees
                         join de in _dbcontext.Departments on E.DeptId equals de.DeptId
                         join lo in _dbcontext.Locations on de.LocationId equals lo.LocationId
                        

                         select new EmployeeClass()
                         {
                             EmpId = E.EmpId,
                             FirstName = E.FirstName,
                             LastName = E.LastName,
                             EmailId = E.EmailId,
                             DOJ = E.DOJ,
                             ManagerId = E.ManagerId,
                             Salary = E.Salary,
                             Bonus = E.Bonus,
                             DeptId = de.DeptId,
                             DeptName = de.DeptName,
                             LocationId = de.LocationId,
                             LAddressLine1 = lo.LAddressLine1,
                             LState = lo.LState,
                             LCity = lo.LCity,
                             LCountry = lo.LCountry,
                             LPincode = lo.LPincode
                         }).ToList();
            return Ok(query);
        }

        [Route("api/omnie/v1/GetManagerDetails")]
        [HttpGet]
        public IHttpActionResult GetManagerDetails()
        {
            try
            {
                using (var dbContext = new EmployeeTableMKSEntities())
                {
                    var query = (from E in _dbcontext.Employees
                                 join mngrname in _dbcontext.Employees on E.ManagerId equals mngrname.EmpId

                                 select new ManagerClass()
                                 {
                                     EmpId = E.EmpId,
                                     FirstName = E.FirstName,
                                     ManagerName = mngrname.FirstName
                                 }).ToList();
                    return Ok(query);
                }
            }
            catch (Exception)
            {
                string errorMessage = "An error occurred while processing your request. Please try again later.";
                return Content(HttpStatusCode.InternalServerError, errorMessage);
            }
        }

        [Route("api/omnie/v1/Create")]
        [HttpPost]
        public HttpResponseMessage CreateEmployeeDetails(Employee employee)
        {
            try
            {
                var employeeDetails = new Employee()
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    EmailId = employee.EmailId,
                    DOJ = employee.DOJ,
                    ManagerId = employee.ManagerId,
                    Salary = employee.Salary,
                    Bonus = employee.Bonus,
                    DeptId = employee.DeptId
                };
                using (var dbContext = new EmployeeTableMKSEntities())
                {
                    dbContext.Employees.Add(employeeDetails);
                    dbContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, $"Employee {employee.FirstName} has been created");
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Entered input is wrong, Please check the credentials");
            }
        }


        [Route("api/omnie/v1/Getid/{id}")]
        [HttpGet]
        public IHttpActionResult GetEmployeebyId(int id)
        {

            try
            {
                var query = (from E in _dbcontext.Employees.Where(E => E.EmpId == id)
                             join de in _dbcontext.Departments on E.DeptId equals de.DeptId
                             join lo in _dbcontext.Locations on de.LocationId equals lo.LocationId

                             select new EmployeeClass()
                             {
                                 EmpId = E.EmpId,
                                 FirstName = E.FirstName,
                                 LastName = E.LastName,
                                 DOJ = E.DOJ,
                                 ManagerId = E.ManagerId,
                                 Salary = E.Salary,
                                 Bonus = E.Bonus,
                                 DeptId = de.DeptId,
                                 DeptName = de.DeptName,
                                 LocationId = de.LocationId,
                                 LAddressLine1 = lo.LAddressLine1,
                                 LState = lo.LState,
                                 LCity = lo.LCity,
                                 LCountry = lo.LCountry,
                                 LPincode = lo.LPincode
                             }).SingleOrDefault();

                if (query != null)
                {
                    return Ok(query);
                }
                else
                {
                    return NotFound();
                }
            }

            catch (Exception)
            {
                string errorMessage = "An error occurred while processing your request. Please try again later.";
                return Content(HttpStatusCode.InternalServerError, errorMessage);
            }
        }


        [Route("api/onmnie/v1/Update/{email}/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateEmployeeDBbyEmailorId(string email,int? id, [System.Web.Http.FromBody] Employee employee)
        {
            try 
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                using (EmployeeTableMKSEntities _dbcontext = new EmployeeTableMKSEntities())
                {
                    var existingEmployee = (email != null)? _dbcontext.Employees.FirstOrDefault(e => e.EmailId == email)
                                             : _dbcontext.Employees.FirstOrDefault(e => e.EmpId == id);

                    if (existingEmployee == null)
                        return NotFound();

                    existingEmployee.FirstName = employee.FirstName ?? existingEmployee.FirstName;
                    existingEmployee.LastName = employee.LastName ?? existingEmployee.LastName;
                    existingEmployee.DOJ = employee.DOJ ?? existingEmployee.DOJ;
                    existingEmployee.ManagerId = employee.ManagerId ?? existingEmployee.ManagerId;
                    existingEmployee.Salary = employee.Salary ?? existingEmployee.Salary;
                    existingEmployee.Bonus = employee.Bonus ?? existingEmployee.Bonus;
                    existingEmployee.DeptId = employee.DeptId ?? existingEmployee.DeptId;



                    _dbcontext.SaveChanges();
                    return Ok($"Employee {employee.FirstName} updated successfully.");

                }
            }
            catch (Exception)
            {
                string errorMessage = "An error occurred while processing your request. Please try again later.";
                return Content(HttpStatusCode.InternalServerError, errorMessage);
            }
        }

        //[Route("api/onmnie/v1/Update/{id}")]
        //[HttpPut]
        //public IHttpActionResult UpdateEmployeeDBbyId(int id, [System.Web.Http.FromBody] Employee employee)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);  

        //        using (EmployeeTableMKSEntities _dbcontext = new EmployeeTableMKSEntities())
        //        {
        //            var existingEmployee = _dbcontext.Employees.FirstOrDefault(e => e.EmpId == id);

        //            if (existingEmployee == null)
        //                return NotFound();
        //            existingEmployee.FirstName = employee.FirstName ?? existingEmployee.FirstName;
        //            existingEmployee.LastName = employee.LastName ?? existingEmployee.LastName;
        //            existingEmployee.DOJ = employee.DOJ ?? existingEmployee.DOJ;
        //            existingEmployee.ManagerId = employee.ManagerId ?? existingEmployee.ManagerId;
        //            existingEmployee.Salary = employee.Salary ?? existingEmployee.Salary;
        //            existingEmployee.Bonus = employee.Bonus ?? existingEmployee.Bonus;
        //            existingEmployee.DeptId = employee.DeptId ?? existingEmployee.DeptId;

        //            _dbcontext.SaveChanges();
        //            return Ok($"Employee {employee.FirstName} updated successfully.");

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        string errorMessage = "An error occurred while processing your request. Please try again later.";
        //        return Content(HttpStatusCode.InternalServerError, errorMessage);
        //    }
        //}

        [Route("api/onmnie/v1/Delete/{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteAccountbyId(int id)
        {
            try
            {
                if (id < 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest,$"Do check your EmployeeId. {id} is not valid !");


                using (var ctx = new EmployeeTableMKSEntities())
                {
                    var employeeToDelete = ctx.Employees.SingleOrDefault(e => e.EmpId == id);

                    if(employeeToDelete ==null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, $"Employee with ID {id} not found");
                    }

                    // ctx.Entry(employeeToDelete).State = System.Data.Entity.EntityState.Deleted;
                    ctx.Employees.Remove(employeeToDelete);
                    ctx.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.NoContent, $"Employee with ID {id} has been deleted.");
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }


        [Route("api/omnie/MasterDepartment/v1")]
        [HttpGet]
        public IHttpActionResult MasterDepartmentList()
        {
            using (var _dbContext = new EmployeeTableMKSEntities()) ;
            var list = _dbcontext.Departments.ToList();
            return Ok(list);
        }

        [Route("api/omnie/mDepartmentwithloc/v1")]
        [HttpGet]
        public IHttpActionResult MasterDepartmentwithlocList()
        {
            try
            {

                var query = (from department in _dbcontext.Departments
                             join loc in _dbcontext.Locations on department.DeptId equals loc.LocationId
                             select new DepartmentwithlocClass()
                             {
                                 DeptId = department.DeptId,
                                 DeptName = department.DeptName,
                                 LocationId = loc.LocationId,
                                 LAddressLine1 = loc.LAddressLine1,
                                 LCountry = loc.LCountry,
                                 LState = loc.LState,
                                 LCity = loc.LCity,
                                 LPincode = loc.LPincode
                             });
                if (query != null)
                {
                    return Ok(query);
                }
                else
                {
                    return NotFound();
                }
            }

            catch (Exception)
            {
                string errorMessage = "An error occurred while processing your request. Please try again later.";
                return Content(HttpStatusCode.InternalServerError, errorMessage);
            }
        }
    }
}

