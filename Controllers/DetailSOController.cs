using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using AMWService.DbContext;
using AMWService.Models;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;

namespace AMWService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DetailSOController : ControllerBase
    {
        //private string formatDate = "dd/MM/yyyy";
        private string sqlcon = "Data Source=191.20.110.4,3001\\MSSQLLocalDB;Initial Catalog=AMW-SERVICE;User Id=sa;Password=@mwte@mp@55;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly DbConfig _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DetailSOController(DbConfig context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            //this._hostEnvironment = hostEnvironment;
        }
        //GET: api/Comment
        //[HttpGet("ServiceID={id:int}")]
        //public async Task<ActionResult<ViewServiceOrder>> Getamv_ServiceOrders(int id)
        //{
        //    var todoItem = await _context.amv_ServiceOrders.FindAsync(id);

        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return todoItem;
        //    //return await _context.amv_ServiceOrders.ToListAsync();
        //}
        //[HttpPost("DocID")]
        //public async Task<ActionResult<IEnumerable<SP_Doc_ServiceOrders>>> Getsa(int id)
        //{
        //    string ServiceID = "EXEC [dbo].[SP_Doc_ServiceOrders]" + "@id =" + id + "'";
        //    var spg = _context.SP_Doc_ServiceOrders.FromSqlRaw(ServiceID).ToList();
        //}
        [HttpGet("ViewServiceOrders")]
        public async Task<ActionResult<IEnumerable<OutputSPViewSO>>> GetListSO([FromQuery] InputViewSO input)
        {

            Dapper.DynamicParameters datas = new Dapper.DynamicParameters();
            string sqlParam = "";
            string values = "";
            int index = 0;
            foreach (var p in input.GetType().GetProperties())
            {
                if (p.GetValue(input) != null)
                {
                    sqlParam += "@" + p.Name + "=@" + p.Name + index + ",";
                    datas.Add("@" + p.Name + index, p.GetValue(input));
                    index++;
                }
            }
            if (!string.IsNullOrWhiteSpace(sqlParam))sqlParam = sqlParam.Remove(sqlParam.Length - 1);
            using (SqlConnection conn = new SqlConnection(sqlcon))
            {
                conn.Open();
                List<dynamic> res = Dapper.SqlMapper.Query<dynamic>(conn,"EXEC [dbo].[SP_View_ServiceOrders]" + sqlParam , datas ).ToList();
                //int x = datas.Get<int>("res");
                return Ok(res);
            }

            //if (int.TryParse("1", out int i))
            //{
            //    return false;
            //}
            //string ExSP = "EXEC [dbo].[SP_View_ServiceOrders] "+values;
            //return await _context.OutputViewSPs.ToListAsync();
        }

        [HttpGet("PieSolve")]  //All Documet For userID
        public async Task<ActionResult<IEnumerable<OutputSPViewSO>>> GetPiesolve([FromQuery] InputViewChart input)
        {

            Dapper.DynamicParameters datas = new Dapper.DynamicParameters();
            string sqlParam = "";
            string values = "";
            int index = 0;
            foreach (var p in input.GetType().GetProperties())
            {
                if (p.GetValue(input) != null)
                {
                    sqlParam += "@" + p.Name + "=@" + p.Name + index + ",";
                    datas.Add("@" + p.Name + index, p.GetValue(input));
                    index++;
                }
            }
            if (!string.IsNullOrWhiteSpace(sqlParam)) sqlParam = sqlParam.Remove(sqlParam.Length - 1);

            using (SqlConnection conn = new SqlConnection(sqlcon))
            {
                conn.Open();
                List<dynamic> res = Dapper.SqlMapper.Query<dynamic>(conn, "EXEC [dbo].[SP_DASHBOARD_CHART_PIE_SOLVE]" + sqlParam, datas).ToList(); //แก้แค่ชื่อ Store Product กับ class input
                //int x = datas.Get<int>("res");
                return Ok(res);
            }
        }

        [HttpGet("PieSolveToday")] //Solve ToDay for Userid
        public async Task<ActionResult<IEnumerable<OutputSPViewSO>>> GetPiesolveToday([FromQuery] InputViewChart input)
        {

            Dapper.DynamicParameters datas = new Dapper.DynamicParameters();
            string sqlParam = "";
            string values = "";
            int index = 0;
            foreach (var p in input.GetType().GetProperties())
            {
                if (p.GetValue(input) != null)
                {
                    sqlParam += "@" + p.Name + "=@" + p.Name + index + ",";
                    datas.Add("@" + p.Name + index, p.GetValue(input));
                    index++;
                }
            }
            if (!string.IsNullOrWhiteSpace(sqlParam)) sqlParam = sqlParam.Remove(sqlParam.Length - 1);

            using (SqlConnection conn = new SqlConnection(sqlcon))
            {
                conn.Open();
                List<dynamic> res = Dapper.SqlMapper.Query<dynamic>(conn, "EXEC [dbo].[SP_DASHBOARD_CHART_PIE_SOLVE_TODAY]" + sqlParam, datas).ToList(); //แก้แค่ชื่อ Store Product กับ class input
                //int x = datas.Get<int>("res");
                return Ok(res);
            }
        }

        [HttpGet("PieSolveYesterday")] //Solve YesterDay for Userid
        public async Task<ActionResult<IEnumerable<OutputSPViewSO>>> GetPiesolveYesterday([FromQuery] InputViewChart input)
        {

            Dapper.DynamicParameters datas = new Dapper.DynamicParameters();
            string sqlParam = "";
            string values = "";
            int index = 0;
            foreach (var p in input.GetType().GetProperties())
            {
                if (p.GetValue(input) != null)
                {
                    sqlParam += "@" + p.Name + "=@" + p.Name + index + ",";
                    datas.Add("@" + p.Name + index, p.GetValue(input));
                    index++;
                }
            }
            if (!string.IsNullOrWhiteSpace(sqlParam)) sqlParam = sqlParam.Remove(sqlParam.Length - 1);

            using (SqlConnection conn = new SqlConnection(sqlcon))
            {
                conn.Open();
                List<dynamic> res = Dapper.SqlMapper.Query<dynamic>(conn, "EXEC [dbo].[SP_DASHBOARD_CHART_PIE_SOLVE_YESTERDAY]" + sqlParam, datas).ToList(); //แก้แค่ชื่อ Store Product กับ class input
                //int x = datas.Get<int>("res");
                return Ok(res);
            }
        }

        [HttpGet("ServiceID")]
        public async Task<ActionResult<IEnumerable<OutputSP>>> Getoutput([FromQuery] int Serviceid)
        {
            string ExSP = "EXEC [dbo].[SP_Doc_ServiceOrders]" + "@id = '" + Serviceid + "'";
            return await _context.OutputSPs.FromSqlRaw(ExSP).ToListAsync();
        }

        [HttpGet("FixCause")]
        public async Task<ActionResult<IEnumerable<Fix_Casue>>> GetFixCause([FromQuery] int Serviceid)
        {
            var resultcause = (await _context.tb_Fix_Cause.Select(x => new Fix_Casue()
            {
                CauseName = x.CauseName,
                HowToFix = x.HowToFix,
                Effort = x.Effort,
                Service_ID = x.Service_ID,
                CreateBy = x.CreateBy,
                CreateDate = x.CreateDate
            }).ToListAsync()).Where(x => x.Service_ID == Serviceid).OrderByDescending(sort => sort.CreateDate).ToList();

            return Ok(resultcause);
        }

        [HttpGet("LogHistory")]
        public async Task<ActionResult<IEnumerable<LogUpdate>>> GetLogHistory([FromQuery] int Serviceid)
        {
            var resultlog = (await _context.tbl_LogUpdate.Select(x => new LogUpdate()
            {
                ServiceId = x.ServiceId,
                Status = x.Status,
                Reason = x.Reason,
                UpdateDate = x.UpdateDate,
                UpdateBy = x.UpdateBy
            }).ToListAsync()).Where(x => x.ServiceId == Serviceid).OrderByDescending(sort => sort.UpdateDate).ToList();

            return Ok(resultlog);
        }

        [HttpGet("GetImage")]
        public async Task<ActionResult<IEnumerable<PicRepair>>> GetPicRepair([FromQuery] int Serviceid)
        {
            var result = (await _context.tb_Pic_Repair.Select(x => new PicRepair()
            {
                Pic_Repair_Id = x.Pic_Repair_Id,
                PR_Name = x.PR_Name,
                PR_Repair_Id = x.PR_Repair_Id,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.PR_Name)
            }).ToListAsync()).Where(x => x.PR_Repair_Id == Serviceid).ToList();

            return Ok(result);

        }

        [HttpPut("UpdateStatusSO={id}")]
        public async Task<IActionResult> PutStatus(int id , [FromBody] UpdateStatus updateSO)
        {
            if (id != updateSO.ID)
            {
                return BadRequest("Service Order not Found");
            }

            _context.Entry(updateSO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new Response { Status = "Success", Message = "Update Success" });
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("HowToFix")] //insert Cause / How to Fix
        public async Task<ActionResult<Fix_Casue>> PostServiceOrder([FromForm] Fix_Casue fix)
        {
            _context.tb_Fix_Cause.Add(fix);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Success" });
        }

    }
}
