using Microsoft.EntityFrameworkCore;
using SertifikasiAPI.Model;

namespace SertifikasiAPI.Context
{
    public class DBContext : DbContext
    {
        public DBContext()
        {

        }
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public virtual DbSet<penggunaModel> PenggunaModels { get; set; }

        public virtual DbSet<LoginModel> LoginViewModels { get; set; }
    }
}
