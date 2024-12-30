using HRMS.Context;
using HRMS.Entities;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Controllers
{
    public class EmployeeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<Employee> employeeList = new List<Employee>();
                using (var context = new HRMSContext())
                {
                    employeeList = await context.Employees.ToListAsync();
                }
                return View(employeeList);
            }
            else
                return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(Employee employee)
        {
            if (User.Identity.IsAuthenticated)
            {
                ResultMessage resultMessage = new ResultMessage();
                try
                {
                    using (var context = new HRMSContext())
                    {
                        context.Employees.Remove(employee);
                        var saveProcess = await context.SaveChangesAsync();

                        if (saveProcess == 1)
                        {
                            resultMessage.Success = true;
                            resultMessage.Message = "Kayıt silindi.";
                        }
                        else
                        {
                            resultMessage.Success = false;
                            resultMessage.Message = "Kayıt silinemedi.";
                        }
                    }
                }
                catch (Exception)
                {
                    resultMessage.Success = false;
                    resultMessage.Message = "Kayıt silinemedi.";
                }

                return Ok(resultMessage);
            }
            else
                return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> AddEmployee()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value;
                return View(new Employee { UserId = Convert.ToInt32(userId) });
            }
            else
                return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeProcess(Employee employee)
        {
            if (User.Identity.IsAuthenticated)
            {
                ResultMessage resultMessage = new ResultMessage();
                try
                {
                    using (var context = new HRMSContext())
                    {
                        var addProcess = await context.Employees.AddAsync(employee);
                        var saveProcess = await context.SaveChangesAsync();

                        if (saveProcess == 1)
                        {
                            resultMessage.Success = true;
                            resultMessage.Message = "Çalışan eklendi.";
                        }
                        else
                        {
                            resultMessage.Success = false;
                            resultMessage.Message = "Çalışan eklenemedi.";
                        }
                    }
                }
                catch (Exception)
                {
                    resultMessage.Success = false;
                    resultMessage.Message = "Çalışan eklenirken beklenmedik bir sorun oluştu.";
                    return Ok(resultMessage);
                }
                return Ok(resultMessage);
            }
            else
                return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> UpdateEmployee(int employeeId)
        {
            if (User.Identity.IsAuthenticated)
            {
                Employee employee = new Employee();
                using (var context = new HRMSContext())
                {
                    employee = await context.Employees.Where(x => x.Id == employeeId).FirstOrDefaultAsync();
                }
                return View(employee);
            }
            else
                return RedirectToAction("Index", "Login");

        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployeeProcess(Employee employee)
        {
            if (User.Identity.IsAuthenticated)
            {
                ResultMessage resultMessage = new ResultMessage();
                try
                {
                    using (var context = new HRMSContext())
                    {
                        context.Employees.Update(employee);
                        var saveProcess = await context.SaveChangesAsync();

                        if (saveProcess == 1)
                        {
                            resultMessage.Success = true;
                            resultMessage.Message = "Kayıt güncellendi.";
                        }
                        else
                        {
                            resultMessage.Success = false;
                            resultMessage.Message = "Kayıt güncellenemedi.";
                        }
                    }

                    return Ok(resultMessage);
                }
                catch (Exception)
                {
                    resultMessage.Success = false;
                    resultMessage.Message = "Kayıt güncellenirken bir sorun oluştu.";
                    return Ok(resultMessage);
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }
    }
}
