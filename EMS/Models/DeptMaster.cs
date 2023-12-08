using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLayer
{
    public class EmpProfile
    {
        [Key]
        public int EmpCode { get; set; }
        public string EmpName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public int DeptCode { get; set; }
        public virtual DeptMaster DeptMaster { get; set; }
    }
    public class DeptMaster
    {
        [Key]
        public int DeptCode { get; set; }
        public string DeptName { get; set; }
        public virtual ICollection<EmpProfile> EmpProfiles { get; set; }
    }
    public class EmsContext : DbContext
    {
        public EmsContext() : base("name=EmsContext")
        {
            Database.SetInitializer<EmsContext>(new DropCreateDatabaseIfModelChanges<EmsContext>());

        }
        public DbSet<EmpProfile> EmpProfiles { get; set; }
        public DbSet<DeptMaster> DeptMasters { get; set; }
    }
    public class EmsIntializer : DropCreateDatabaseIfModelChanges<EmsContext>
    {
        protected override void Seed(EmsContext context)
        {
            base.Seed(context);
            DeptMaster d1 = new DeptMaster { DeptCode = 1, DeptName = "Sales" };
            DeptMaster d2 = new DeptMaster { DeptCode = 2, DeptName = "IT" };
            context.DeptMasters.Add(d1);
            context.SaveChanges();
            context.DeptMasters.Add(d2);
            context.SaveChanges();
            EmpProfile empProfile = new EmpProfile
            {
                EmpCode = 1,
                EmpName = "Rohit",
                DateOfBirth = new
           DateTime(2000, 12, 11),
                DeptCode = 1,
                Email = "Rohit@gmail.com"
            };
            context.EmpProfiles.Add(empProfile);
            context.SaveChanges();
        }
    }
}
EmpProfileRepository.cs
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace DataAccessLayer
{
    public class EmpProfileRepository : IRepository<EmpProfile>
    {
        private readonly EmsContext context;
        public EmpProfileRepository(EmsContext dbContext)
        {
            this.context = dbContext;
        }
        public IEnumerable<EmpProfile> GetAll()
        {
            return context.EmpProfiles.ToList();
        }
        public EmpProfile GetByCode(int code)
        {
            return context.EmpProfiles.FirstOrDefault(e => e.EmpCode == code);
        }
        public void Insert(EmpProfile entity)
        {
            context.EmpProfiles.Add(entity);
            Save();
        }
        public void Update(EmpProfile entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            Save();
        }
        public void Delete(int code)
        {
            var entity = context.EmpProfiles.Find(code);
            if (entity != null)
            {
                context.EmpProfiles.Remove(entity);
                Save();
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}