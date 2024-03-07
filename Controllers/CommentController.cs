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

namespace AMWService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        //private string formatDate = "dd/MM/yyyy";
        private readonly DbConfig _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CommentController(DbConfig context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }
        //GET: api/Comment
        [HttpGet("Message")] //ShowMessage
        public async Task<ActionResult<IEnumerable<MessageBox>>> Gettb_Comments([FromQuery] int Service_ID)
        {
            var message = (await _context.tb_Comments.Select(x => new MessageBox()
            {
                ID = x.ID,
                Service_ID = x.Service_ID,
                Service_No = x.Service_No,
                UserID = x.UserID,
                UserName = x.UserName,
                Description = x.Description,
                ParentID = x.ParentID,
                Status = x.Status,
                CreateTime = x.CreateTime
            }).ToListAsync()).Where(x => x.Service_ID == Service_ID).ToList();
            
            return Ok(message);
    
        }

        [HttpPost] //SaveMessage
        public async Task<ActionResult<MessageBox>> PostMessageBox([FromForm] MessageBox messageBox)
        {
            _context.tb_Comments.Add(messageBox);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Success" });
        }

        [HttpGet("GetImageComment")] //ShowImageComment
        public async Task<ActionResult<IEnumerable<PicComment>>> GetPicRepair([FromQuery] int RepairID)
        {
                var result = (await _context.tb_Pic_Repair_AQ.Select(x => new PicComment()
                {
                    Pic_Repair_AQ_Id = x.Pic_Repair_AQ_Id,
                    PRAQ_Name = x.PRAQ_Name,
                    PRAQ_Ans_Question_Id = x.PRAQ_Ans_Question_Id,
                    PR_Repair_Id = x.PR_Repair_Id,
                    Comment_Id = x.Comment_Id,
                    ImageSrc = String.Format("{0}://{1}{2}/ImageComment/{3}", Request.Scheme, Request.Host, Request.PathBase, x.PRAQ_Name)
                }).ToListAsync()).Where(x => x.PR_Repair_Id == RepairID).ToList();

            return Ok(result);
        }

        [HttpPost("ImageComment")] //บันทึกรูปภาพ
        public async Task<ActionResult<PicComment>> PostPicRepair([FromForm] PicComment SaveImg, List<IFormFile> ImageFile)
        {
            string Upload = null;
            if (SaveImg == null || SaveImg.PRAQ_Name.Length == 0)
            {
                return BadRequest("No file is uploaded.");
            }
          

            foreach (var i in ImageFile)
            {
      
                var path = Directory.GetCurrentDirectory() + "/ImageComment/";
                    Upload = Guid.NewGuid().ToString() + Path.GetExtension(i.FileName);
                    string save = path + Upload;
                    using (var stream = new FileStream(save, FileMode.Create))
                    {
                        await i.CopyToAsync(stream);
                    }
             
            }
            SaveImg.PRAQ_Name = Upload;
            _context.tb_Pic_Repair_AQ.Add(SaveImg);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Success" });
        }
    }
}
