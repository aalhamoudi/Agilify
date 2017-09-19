using Microsoft.Azure.Mobile.Server.Tables;

namespace AgilifyService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AgilifyService.Data.AgilifyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new EntityTableSqlGenerator());

        }

        protected override void Seed(AgilifyService.Data.AgilifyContext context)
        {

        }
    }
}
