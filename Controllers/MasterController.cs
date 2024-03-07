using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using AMWService.DbContext;
using AMWService.Models;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;
using AMWService.IdentityAuth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMWService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly DbConfig _context;
        private readonly UserManager<User> _userManager;

        //
        private string sqlcon = "Data Source=191.20.110.4,3001\\MSSQLLocalDB;Initial Catalog=AMW-SERVICE;User Id=sa;Password=@mwte@mp@55;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public MasterController(DbConfig context)
        {
            _context = context;
        }

        [HttpGet("GetOperator")]
        public async Task<ActionResult<IEnumerable<ViewOperator>>> GetOperator([FromQuery] InputOperator input)
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
                List<dynamic> res = Dapper.SqlMapper.Query<dynamic>(conn, "EXEC [dbo].[SP_View_Operator]" + sqlParam, datas).ToList();

                return Ok(res);
            }
        }

        [HttpPost("Operator")] //insert Operator
        public async Task<ActionResult<Operator>> PostOperator([FromForm] Operator createop)
        {
            _context.mst_Operator.Add(createop);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Success" });
        }

        [HttpPut("OperatorID={id}")]
        public async Task<IActionResult> PutOperatorid(int id, [FromForm] Operator updateOperator)
        {
            if (id != updateOperator.ID)
            {
                return BadRequest("Operator ID Don't Match");
            }
            //_context.Entry(updateOperator).State = EntityState.Modified;
            try
            {
                var result = await _context.mst_Operator.FirstOrDefaultAsync(x => x.ID == id);

                if (result == null)
                {
                    return NotFound();
                }
                if (updateOperator.Status == 2) //Status remove
                {
                    result.Status = updateOperator.Status;
                    result.Updatedate = DateTime.Now;
                    result.UpdateBy = updateOperator.UpdateBy;
                }
                else  //Update Default
                {
                    result.ID = updateOperator.ID;
                    result.OP_Customer_id = updateOperator.OP_Customer_id;
                    result.OP_Name = updateOperator.OP_Name;
                    result.OP_Name_Eng = updateOperator.OP_Name_Eng;
                    result.OP_NickName = updateOperator.OP_NickName;
                    result.OP_Phone = updateOperator.OP_Phone;
                    result.OP_Email = updateOperator.OP_Email;
                    result.OP_Line_id = updateOperator.OP_Line_id;
                    result.Updatedate = DateTime.Now;
                    result.UpdateBy = updateOperator.UpdateBy;
                }
                //await _context.Update(result);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok(new Response { Status = "Success", Message = "Success" });
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<IEnumerable<ViewUser>>> GetUser([FromQuery] InputUser input)
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
                List<dynamic> res = Dapper.SqlMapper.Query<dynamic>(conn, "EXEC [dbo].[SP_View_User]" + sqlParam, datas).ToList();

                //int x = datas.Get<int>("res");
                return Ok(res);
            }
        }

        [HttpGet("GetMAProject")]
        public async Task<ActionResult<IEnumerable<ViewMAProject>>> GetMAProject([FromQuery] InputMAProject input)
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
                List<dynamic> res = Dapper.SqlMapper.Query<dynamic>(conn, "EXEC [dbo].[SP_View_MAProject]" + sqlParam, datas).ToList();

                //int x = datas.Get<int>("res");
                return Ok(res.Select(x => new ViewMAProject()
                {
                    ID = x.ID,
                    MA_Customer_Id = x.MA_Customer_Id,
                    Customer = x.Customer,
                    MA_Code = x.MA_Code,
                    MA_Name = x.MA_Name,
                    MA_PM = x.MA_PM,
                    MA_Service = x.MA_Service,
                    MA_Insurance = x.MA_Insurance,
                    File_Name = x.File_Name,
                    //MA_Detail = x.MA_Detail
                    MA_Effective_Start = x.MA_Effective_Start,
                    MA_Effective_End = x.MA_Effective_End,
                    Status = x.Status,
                    FileSrc = String.Format("{0}://{1}{2}/FileMA/{3}", Request.Scheme, Request.Host, Request.PathBase, x.File_Name)
                }));
            }
        }

        [HttpPost("MAProject")] //insert MA
        public async Task<ActionResult<MA_Project>> PostMA_Project([FromForm] MA_Project create_ma)
        {
            _context.mst_MA_Project.Add(create_ma);
            await _context.SaveChangesAsync();
 
            return Ok(new Response { Status = "Success", Message = "Success" });
        }

        [HttpPost("FileMA")] //บันทึกไฟล์ pdf
        public async Task<ActionResult<FileMA>> PostFilepdf([FromForm] FileMA fileMA, List<IFormFile> MAFile)
        {
            string Upload = null;
            if (fileMA == null || fileMA.File_Name.Length == 0)
            {
                return BadRequest("No file is uploaded.");
            }

            foreach (var i in MAFile)
            {

                var path = Directory.GetCurrentDirectory() + "/FileMA/";
                Upload = Guid.NewGuid().ToString() + Path.GetExtension(i.FileName);
                string save = path + Upload;
                using (var stream = new FileStream(save, FileMode.Create))
                {
                    await i.CopyToAsync(stream);
                }

            }
            fileMA.File_Name = Upload;
            _context.tb_File_MA_Project.Add(fileMA);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Success" });
        }

        [HttpPut("MAProjectID={id}")]  //Update MA
        public async Task<IActionResult> PutMAProject(int id, [FromForm] MA_Project updateMA)
        {
            if (id != updateMA.ID)
            {
                return BadRequest("MAProject ID Don't Match");
            }

            try
            {
                var result = await _context.mst_MA_Project.FirstOrDefaultAsync(x => x.ID == id);

                if (result == null)
                {
                    return NotFound();
                }
                if (updateMA.Status == 2) //Status remove
                {
                    result.Status = updateMA.Status;
                    result.UpdateDate = DateTime.Now;
                    result.UpdateBy = updateMA.UpdateBy;
                    //await _context.SaveChangesAsync();
                }
                else   //Update Default
                {
                    result.ID = updateMA.ID;
                    result.MA_Customer_Id = updateMA.MA_Customer_Id;
                    result.MA_Code = updateMA.MA_Code;
                    result.MA_Name = updateMA.MA_Name;
                    result.MA_PM = updateMA.MA_PM;
                    result.MA_Insurance = updateMA.MA_Insurance;
                    result.MA_Service = updateMA.MA_Service;
                    result.MA_Detail = updateMA.MA_Detail;
                    result.MA_Effective_Start = updateMA.MA_Effective_Start;
                    result.MA_Effective_End = updateMA.MA_Effective_End;
                    result.UpdateDate = DateTime.Now;
                    result.UpdateBy = updateMA.UpdateBy;
                    //await _context.Update(result);

                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok(new Response { Status = "Success", Message = "Success" });
        }


        //[HttpGet("GetMachineListx")]
        //public async Task<ActionResult<IEnumerable<ViewMachineList>>> GetMachineList()
        //{
        //    return await _context.amv_MachineList.ToListAsync();
        //}

        [HttpGet("GetMachineList")]
        public async Task<ActionResult<IEnumerable<ViewMachineList>>> GetMachineList([FromQuery] InputMachineList input)
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
                List<dynamic> res = Dapper.SqlMapper.Query<dynamic>(conn, "EXEC [dbo].[SP_View_MachineList]" + sqlParam, datas).ToList();

                //int x = datas.Get<int>("res");
                return Ok(res);
            }
        }

        [HttpPost("MachineList")] //insert MachineList
        public async Task<ActionResult<MachineList>> PostMachineList([FromForm] MachineList machine)
        {
            _context.mst_MachineList.Add(machine);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Success" });
        }

        [HttpPut("MachineListID={id}")] //Update MC
        public async Task<IActionResult> PutmachineList(int id, [FromForm] MachineList updateMachinelist)
        {
            if (id != updateMachinelist.ID)
            {
                return BadRequest("MachineList ID Don't Match");
            }

            try { 
                var result = await _context.mst_MachineList.FirstOrDefaultAsync(x => x.ID == id);
                
                if (result == null)
                {
                    return NotFound();
                }
                if (updateMachinelist.Status == 2) //Status remove
                {
                    result.Status = updateMachinelist.Status;
                    result.Updatedate = DateTime.Now;
                    result.UpdateBy = updateMachinelist.UpdateBy;
                    //await _context.SaveChangesAsync();
                }
                else   //Update Default
                {
                    result.ID = updateMachinelist.ID;
                    result.MC_Customer_Id = updateMachinelist.MC_Customer_Id;
                    result.MC_Code = updateMachinelist.MC_Code;
                    result.MC_Name = updateMachinelist.MC_Name;
                    result.MC_Size = updateMachinelist.MC_Size;
                    result.MC_Count = updateMachinelist.MC_Count;
                    result.Updatedate = DateTime.Now;
                    result.UpdateBy = updateMachinelist.UpdateBy;
                    //await _context.Update(result);
                    
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
    }

            return Ok(new Response { Status = "Success", Message = "Success" });
        }
    }
}
