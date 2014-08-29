using FluentMigrator;
using System;

namespace DCT.Beano.Management.Migrations
{

    public abstract class SqlMigration : Migration
    {
        public override void Up()
        {
            RunEmbeddedScript("U{0}.sql");
        }

        public override void Down()
        {
            RunEmbeddedScript("D{0}.sql");
        }

        //Grab the migration number from {this} class attribute and run the corresponding script. Migration Number is injected into fileFormat
        private void RunEmbeddedScript(string fileFormat)
        {
            // grab migration attribute on the (derived) class
            var migrationAttribute = Attribute.GetCustomAttribute(this.GetType(), typeof(MigrationAttribute)) as MigrationAttribute;

            // pad migration number out to 4 numbers
            var migrationNumber = migrationAttribute.Version.ToString("d4");

            // run the sql script using the fileformat supplied
            Execute.EmbeddedScript(string.Format(fileFormat, migrationNumber));
        }
    }
}
