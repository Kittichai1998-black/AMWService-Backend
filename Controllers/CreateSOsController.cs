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
using System.Net;
using System.Text;
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.JsonPatch;

namespace AMWService.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CreateSOsController : ControllerBase
    {
        //private string formatDate = "dd/MM/yyyy";
        private readonly  DbConfig _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CreateSOsController(DbConfig context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        [Authorize]
        //GET: api/CreateSOs
        [HttpGet("GetColumns")] //สถานะเอกสาร
        public async Task<ActionResult<IEnumerable<Columns>>> Getam_Status()
        {
            return await _context.mst_Status.ToListAsync();
        }

        [Authorize]
        [HttpGet("GetCustommer")]//ชื่อลูกค้า
        public async Task<ActionResult<IEnumerable<Customers>>> Getmst_Custommer()
        {
            return await _context.mst_Customer.ToListAsync();
        }

        [Authorize]
        [HttpGet("GetProject")] //โปรเจค
        public async Task<ActionResult<IEnumerable<Project>>> Getmst_Project([FromQuery] int Customer_id)
        {
            var result = (await _context.mst_Project.Select(x => new Project()
            {
                ID = x.ID,
                Project_Code = x.Project_Code,
                Project_Name = x.Project_Name,
                Customer_Id = x.Customer_Id,
                Status = x.Status,
            }).ToListAsync()).Where(x => x.Customer_Id == Customer_id).ToList();

            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetOperator")] //Operator ลูกค้า
        public async Task<ActionResult<IEnumerable<Operator>>> Getmst_Operator([FromQuery] int Operator_id, [FromQuery] int Customer_id)
        {
            var result = (await _context.mst_Operator.Select(x => new Operator() {
                ID = x.ID,
                OP_Customer_id = x.OP_Customer_id,
                OP_Name = x.OP_Name,
                OP_Name_Eng = x.OP_Name_Eng,
                OP_NickName = x.OP_NickName,
                OP_Phone = x.OP_Phone,
                OP_Email = x.OP_Email,
                OP_Line_id = x.OP_Line_id,
                OP_Line_Pic = x.OP_Line_Pic,
                Status = x.Status
            }).ToListAsync()).Where(x => x.ID == Operator_id || x.OP_Customer_id == Customer_id).ToList();

            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetCompany")] //ข้อมูลบริษัทลูกค้า
        public async Task<ActionResult<IEnumerable<Company>>> Getmst_Company([FromQuery] int Customer_id)
        {
            var result = (await _context.mst_Company.Select(x => new Company()
            {
                ID = x.ID,
                CP_Short_Name = x.CP_Short_Name,
                CP_Name = x.CP_Name,
                CP_Address = x.CP_Address,
                CP_Phone = x.CP_Phone,
                CP_Email = x.CP_Email,
                Customer_Id = x.Customer_Id,
                CP_Latitude = x.CP_Latitude,
                CP_Longitude = x.CP_Longitude,
                Status = x.Status,
            }).ToListAsync()).Where(x => x.Customer_Id == Customer_id).ToList();

            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetProblum")]
        public async Task<ActionResult<IEnumerable<Problem>>> Getam_Type()
        {
            return await _context.mst_Type.ToListAsync();
        }

        [Authorize]
        [HttpGet("GetRootcause")]//ปัญหาที่เกี่ยวข้อง
        public async Task<ActionResult<IEnumerable<RootCause>>> Getam_RootCauseType()
        {
            return await _context.mst_RootCauseType.ToListAsync();
        }

        [Authorize]
        [HttpGet("GetPriolity")]//ความสำคัญ
        public async Task<ActionResult<IEnumerable<Priolity>>> Getam_Priority()
        {
            return await _context.mst_Priority.ToListAsync();
        }

        [Authorize]
        [HttpGet("GetDepartment")] //แผนก
        public async Task<ActionResult<IEnumerable<Department>>> Getmst_Department()
        {
            return await _context.mst_Department.ToListAsync();
        }

        [Authorize]
        [HttpGet("GetRole")] //Role
        public async Task<ActionResult<IEnumerable<Role>>> Getmst_Role()
        {
            return await _context.mst_Role.ToListAsync();
        }

        [Authorize]
        [HttpGet("GetUser")] //แสดง  User
        public async Task<ActionResult<IEnumerable<UserOwner>>> Getamv_User([FromQuery] int Department_id)
        {
            //return await _context.amv_User.ToListAsync();
            var result = (await _context.amv_User.Select(x => new UserOwner()
            {
                UserID = x.UserID,
                UserName = x.UserName,
                FirsName = x.FirsName,
                DepartmentID = x.DepartmentID,
                Status = x.Status
            }).ToListAsync()).Where(x => x.DepartmentID == Department_id).ToList();

            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetWorkGroup")] //Group ผู้รับผิดชอบ
        public async Task<ActionResult<IEnumerable<WorkGroup>>> GetWorkGroup([FromQuery] int service_id)
        {
            var result = (await _context.tb_WorkGroup.Select(x => new WorkGroup()
            {
                ID = x.ID,
                Service_ID = x.Service_ID,
                UserID = x.UserID,
                UserName = x.UserName,
                FirsName = x.FirsName,
                DepartmentID = x.DepartmentID,
                Status = x.Status
            }).ToListAsync()).Where(x => x.Service_ID == service_id && x.Status == 1).ToList();

            return Ok(result);
        }

        [Authorize]
        [HttpPut("Service={id}")]
        public async Task<IActionResult> PutService(int id, [FromForm] CreateSO update)
        {
            if (id != update.ID)
            {
                return BadRequest("Service ID Don't Match");
            }
            //_context.Entry(updateOperator).State = EntityState.Modified;
            try
            {
                var result = await _context.tb_ServiceOrder.FirstOrDefaultAsync(x => x.ID == id);

                if (result == null)
                {
                    return NotFound();
                }
                if (update.Status == 2) //Status remove
                {
                    result.Status = update.Status;
                    result.UpdateDate = DateTime.Now;
                    result.UpdateBy = update.UpdateBy;
                }
                else  //Update Default
                {
                    //result.ID = updateOperator.ID;
                    result.Customer_Id = update.Customer_Id;
                    result.Project_Id = update.Project_Id;
                    result.Operator_Id = update.Operator_Id;
                    result.Rootcause_Id = update.Rootcause_Id;
                    result.Priority_Id = update.Priority_Id;
                    result.Problem = update.Problem;
                    result.Department_Id = update.Department_Id;
                    result.DueDate = update.DueDate;
                    result.UpdateDate = DateTime.Now;
                    result.UpdateBy = update.UpdateBy;
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

        [Authorize]
        [HttpPut("ServiceID={id}")]
        public async Task<IActionResult> PutStatus(int id,[FromBody]UpdateStatus updateSO)
        {
            if (id != updateSO.ID)
            {
                return BadRequest();
            }

            _context.Entry(updateSO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    return NotFound();
            }

            return NoContent();
        }

        //[Authorize]
        [HttpPost] //สร้างเอกสาร
        public async Task<ActionResult<CreateSO>> PostCreate([FromForm] CreateSO createSO )
        {
            //createSO.Image = await Uploadimage(createSO.ImageFile);
            try
            {
                _context.tb_ServiceOrder.Add(createSO);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseCreate { Message = "error", Status = 0 });
            }
            var result = (await _context.tb_ServiceOrder.Join(_context.mst_Customer,
                s => s.Customer_Id,
                c => c.ID,
                (s,c) => new ResponseCreate
            {
                ID = s.ID,
                Code = s.Code,
                Customer = c.Customer,
                Problem = s.Problem,
                Status = s.Status,
                Message = "success"
            }).ToListAsync()).OrderByDescending(u => u.ID).FirstOrDefault();

            //********** Send Line notify 
            try
            {
                var token = "1wWzUdGrrW0u7DZhXUWHgqnpCqwjr0Gf4HAUGCtxiAd";
                var message = "ServiceOrder:" + result.Code + "\n" +
                              "Customer:"+ result.Customer + "\n" +
                              "ปัญหา:" + result.Problem + "\n" +
                              "http://191.20.110.4:8090/DetailDoc?ServiceID=" + result.ID ;
                message = System.Web.HttpUtility.UrlEncode(message, Encoding.UTF8);
                var request = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify");
                var postData = string.Format("message={0}", message);
                var data = Encoding.UTF8.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.Headers.Add("Authorization", "Bearer " + token);
                var stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost("WorkGroup")] // เพิ่มผู้รับผิดชอบเคส
        public async Task<ActionResult<WorkGroup>> PostWorkGroup([FromForm] WorkGroup workGroup)
        {
            //createSO.Image = await Uploadimage(createSO.ImageFile);
            _context.tb_WorkGroup.Add(workGroup);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Success" });
        }

        [Authorize]
        [HttpPut("EditWorkGroup={id}")]
        public async Task<IActionResult> PutService(int id , [FromForm] WorkGroup  editGroup)
        {
            if (id != editGroup.ID)
            {
                return BadRequest("Service ID Don't Match");
            }
            try
            {
                var result = await _context.tb_WorkGroup.FirstOrDefaultAsync(x => x.ID == id);

                if (result == null)
                {
                    return NotFound();
                }

                if (editGroup.Status == 2) //Status remove
            {
                result.Status = editGroup.Status;
                result.UpdateDate = DateTime.Now;
                result.UpdateBy = editGroup.UpdateBy;
            }
          
            await _context.SaveChangesAsync();
        }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok(new Response { Status = "Success", Message = "Success" });
        }

        [Authorize]
        [HttpPost("ImageFile")] //บันทึกรูปภาพ
        public async Task<ActionResult<PicRepair>> PostPicRepair([FromForm] PicRepair SaveImg, List<IFormFile> ImageFile)
        {
            string Upload = null;
            if (SaveImg == null || SaveImg.PR_Name.Length == 0)
            {
                return BadRequest("No file is uploaded.");
            }

            foreach (var i in ImageFile)
            {
                        var path = Directory.GetCurrentDirectory() + "/Images/";
                        Upload = Guid.NewGuid().ToString() + Path.GetExtension(i.FileName);
                        string save = path + Upload;
                        using (var stream = new FileStream(save, FileMode.Create))
                        {
                            await i.CopyToAsync(stream);
                        }
            }
            SaveImg.PR_Name = Upload;
            _context.tb_Pic_Repair.Add(SaveImg);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Success" });
        }

        //[HttpPost("LineNotify")] //Line Notify token 1wWzUdGrrW0u7DZhXUWHgqnpCqwjr0Gf4HAUGCtxiAd
        //public void PostNotify([FromBody] LineNotifications notify)
        //{
        //    //Line Notify
        //    try
        //    {
        //        notify.message = System.Web.HttpUtility.UrlEncode(notify.message, Encoding.UTF8);
        //        var request = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify");
        //        var postData = string.Format("message={0}", notify.message);
        //        var data = Encoding.UTF8.GetBytes(postData);
        //        request.Method = "POST";
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        request.ContentLength = data.Length;
        //        request.Headers.Add("Authorization", "Bearer " + notify.lineToken);
        //        var stream = request.GetRequestStream();
        //        stream.Write(data, 0, data.Length);
        //        var response = (HttpWebResponse)request.GetResponse();
        //        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}

        //[HttpPatch("ServiceID={id}")]
        //public async Task<IActionResult> PatchReason(int id , [FromBody] JsonPatchDocument<CreateSO> patchReason)
        //{
        //    var entity = await _context.tb_ServiceOrder.FirstOrDefaultAsync(reas => reas.ID == id);

        //    if(entity == null)
        //    {
        //        return NotFound();
        //    }
        //    patchReason.ApplyTo(entity, ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        ////    }

        //    try
        //    {
        //        _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(entity);
        //}
    }


}

