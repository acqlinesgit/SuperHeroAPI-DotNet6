using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _employee;

        public EmployeeController(DataContext employee)
        {
            _employee = employee;
        }
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployee()
        {
            return Ok(await _employee.Employees.ToArrayAsync());
        }
        public class EmployeepParameter
        {
            public string Name { get; set; }
            public string Adaccount { get; set; }
            public string Password { get; set; }
        }
        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(EmployeepParameter Parameter, string CreateUser, int iDeptID)
        {
            string sRemark = string.Empty;
            if (string.IsNullOrEmpty(Parameter.Name.Trim()))
            {
                sRemark += "名字不得為空" + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(Parameter.Adaccount.Trim()))
            {
                sRemark += "帳號不得為空" + Environment.NewLine; ;
            }
            if (string.IsNullOrEmpty(Parameter.Password.Trim()))
            {
                sRemark += "密碼不得為空" + Environment.NewLine; ;
            }
            if (!string.IsNullOrEmpty(sRemark.Trim()))
            {
                return BadRequest(sRemark);
            }
            if (Parameter.Adaccount.Trim().Length <= 7)
            {
                sRemark += "帳號不得小於8碼" + Environment.NewLine; ;
            }
            if (Parameter.Password.Trim().Length <= 7)
            {
                sRemark += "密碼不得小於8碼" + Environment.NewLine; ;
            }
            if (!string.IsNullOrEmpty(sRemark.Trim()))
            {
                return BadRequest(sRemark);
            }
            var employeeList = await _employee.Employees.ToListAsync();
            if (employeeList.Count > 0)
            {
                var result = from p in employeeList where p.Adaccount.Trim() == Parameter.Adaccount.Trim() select p;
                if (result.Any())
                {
                    return BadRequest("帳號已重複");
                }
            }
            try
            {
                Employee employee = new Employee();
                employee.Adaccount = Parameter.Adaccount;
                employee.Password = Parameter.Password;
                employee.EmployeeDeptID = iDeptID;
                employee.BornDay = DateTime.Now;
                employee.BornTime = DateTime.Now;
                employee.UpdateDay = DateTime.Now;
                employee.CreateDay = DateTime.Now;
                employee.CreateUser = "MINGHAN.ZHONG";
                employee.ModifyUser = "MINGHAN.ZHONG";
                employee.EmployeeName = Parameter.Name;
                _employee.Employees.Add(employee);
                await _employee.SaveChangesAsync();
                return Ok(await _employee.Employees.ToArrayAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + Environment.NewLine + ex.InnerException);
            }
        }
    }
}
