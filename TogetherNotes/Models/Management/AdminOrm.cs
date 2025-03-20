using System.Data.SqlClient;
using System.Linq;

namespace TogetherNotes.Models.Management
{
    public static class AdminOrm
    {
        public static int SelectTotalOfAdmin(int rol)
        {
            int totalAdmins = Orm.db.admin
                    .Where(admin => admin.role_id == rol)
                    .Count();

            return totalAdmins;
        }
    }
}