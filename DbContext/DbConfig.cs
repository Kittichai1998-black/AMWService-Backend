using AMWService.IdentityAuth;
using AMWService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AMWService.DbContext
{
    public class DbConfig : IdentityDbContext<User>
    {
        public DbConfig(DbContextOptions<DbConfig> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        //User
        public DbSet<User> AspNetUsers { get; set; }
        public DbSet<ViewUser> AspNetUserS { get; set; }

        //Master table
        public DbSet<Customers> mst_Customer { get; set; }
        public DbSet<Operator> mst_Operator { get; set; }
        public DbSet<Company> mst_Company { get; set; }
        public DbSet<Columns> mst_Status { get; set; }
        public DbSet<Problem> mst_Type { get; set; }
        public DbSet<RootCause> mst_RootCauseType { get; set; }
        public DbSet<Priolity>mst_Priority { get; set; }
        public DbSet<User_Role>mst_User_Role { get; set; }
        public DbSet<User_Roles> mst_User_Roles { get; set; }
        public DbSet<MA_Project> mst_MA_Project { get; set; }
        public DbSet<MachineList> mst_MachineList { get; set; }
        public DbSet<RemoveOperator> Mst_Operator { get; set; }
        public DbSet<Role> mst_Role { get; set; }
        public DbSet<Project> mst_Project { get; set; }
        public DbSet<Employee>mst_Employee { get; set; }

        //table
        public DbSet<CreateSO> tb_ServiceOrder { get; set; }
        public DbSet<UpdateStatus> Tb_ServiceOrder { get; set; }
        public DbSet<ServiceOrder> tb_Repair { get; set; }
        public DbSet<MessageBox>tb_Comments { get; set; }
        public DbSet<PicRepair> tb_Pic_Repair { get; set; }
        public DbSet<FileMA> tb_File_MA_Project { get; set; }
        public DbSet<PicComment> tb_Pic_Repair_AQ { get; set; }
        public DbSet<Fix_Casue> tb_Fix_Cause { get; set; }
    
        public DbSet<WorkGroup> tb_WorkGroup { get; set; }
        public DbSet<LogUpdate> tbl_LogUpdate { get; set; }



        //Table View
        public DbSet<Department>mst_Department { get; set; }
        public DbSet<UserOwner>amv_User { get; set; }
        public DbSet<ViewServiceOrder> amv_ServiceOrders { get; set; }
        public DbSet<ViewOperator> amv_Operator { get; set; }
        public DbSet<ViewMAProject> amv_MA_Project { get; set; }
        public DbSet<ViewMachineList> amv_MachineList { get; set; }

        //Database Store Procedures
        public  DbSet<OutputSP>OutputSPs { get; set; }
        public DbSet<InputViewSO>Inputview { get; set; }
        public DbSet<OutputSPViewSO> OutputViewSPs { get; set; }
    }
}
