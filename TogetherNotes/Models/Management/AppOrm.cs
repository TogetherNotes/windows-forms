using System.Linq;

namespace TogetherNotes.Models.Management
{
    public static class AppOrm
    {
        public static int SelectTotalOfApp()
        {
            int totalApp = Orm.db.app
                    .Count();

            return totalApp;
        }

        public static int SelectTotalOfAppWithRole(string role)
        {
            int totalApp = Orm.db.app
                    .Where(app => app.role == role)
                    .Count();

            return totalApp;
        }
    }
}