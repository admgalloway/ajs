﻿using FluentMigrator;

namespace WeeWorld.ADS.Migrations

{
    [Migration(0003)]
    public class M0003 : SqlMigration
    {
        // nothing to setup, only attribute names - SqlMigration base class 
        // impliments Up() and Down() to run the corresponding SQL scripts
    }
}
