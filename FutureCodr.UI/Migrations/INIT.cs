namespace FutureCodr.UI.Migrations
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Builders;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.0-20911")]
    public sealed class INIT : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(INIT));

        public override void Down()
        {
            base.DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers", null);
            base.DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", null);
            base.DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", null);
            base.DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", null);
            base.DropIndex("dbo.AspNetUserClaims", new string[] { "User_Id" }, null);
            base.DropIndex("dbo.AspNetUserRoles", new string[] { "UserId" }, null);
            base.DropIndex("dbo.AspNetUserRoles", new string[] { "RoleId" }, null);
            base.DropIndex("dbo.AspNetUserLogins", new string[] { "UserId" }, null);
            base.DropTable("dbo.AspNetUserRoles", null);
            base.DropTable("dbo.AspNetUserLogins", null);
            base.DropTable("dbo.AspNetUserClaims", null);
            base.DropTable("dbo.AspNetUsers", null);
            base.DropTable("dbo.AspNetRoles", null);
        }

        public override void Up()
        {
            base.CreateTable("dbo.AspNetRoles", c => new { Id = c.String(false, 0x80, null, null, null, null, null, null), Name = c.String(false, null, null, null, null, null, null, null) }, null).PrimaryKey(t => t.Id, null, true, null);
            base.CreateTable("dbo.AspNetUsers", c => new { Id = c.String(false, 0x80, null, null, null, null, null, null), UserName = c.String(null, null, null, null, null, null, null, null), PasswordHash = c.String(null, null, null, null, null, null, null, null), SecurityStamp = c.String(null, null, null, null, null, null, null, null), Discriminator = c.String(false, 0x80, null, null, null, null, null, null) }, null).PrimaryKey(t => t.Id, null, true, null);
            base.CreateTable("dbo.AspNetUserClaims", c => new { Id = c.Int(false, true, null, null, null, null), ClaimType = c.String(null, null, null, null, null, null, null, null), ClaimValue = c.String(null, null, null, null, null, null, null, null), User_Id = c.String(false, 0x80, null, null, null, null, null, null) }, null).PrimaryKey(t => t.Id, null, true, null).ForeignKey("dbo.AspNetUsers", t => t.User_Id, true, null, null).Index(t => t.User_Id, false, false, null);
            base.CreateTable("dbo.AspNetUserLogins", c => new { UserId = c.String(false, 0x80, null, null, null, null, null, null), LoginProvider = c.String(false, 0x80, null, null, null, null, null, null), ProviderKey = c.String(false, 0x80, null, null, null, null, null, null) }, null).PrimaryKey(t => new { UserId = t.UserId, LoginProvider = t.LoginProvider, ProviderKey = t.ProviderKey }, null, true, null).ForeignKey("dbo.AspNetUsers", t => t.UserId, true, null, null).Index(t => t.UserId, false, false, null);
            base.CreateTable("dbo.AspNetUserRoles", c => new { UserId = c.String(false, 0x80, null, null, null, null, null, null), RoleId = c.String(false, 0x80, null, null, null, null, null, null) }, null).PrimaryKey(t => new { UserId = t.UserId, RoleId = t.RoleId }, null, true, null).ForeignKey("dbo.AspNetRoles", t => t.RoleId, true, null, null).ForeignKey("dbo.AspNetUsers", t => t.UserId, true, null, null).Index(t => t.RoleId, false, false, null).Index(t => t.UserId, false, false, null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201403241818476_INIT";
            }
        }

        string IMigrationMetadata.Source
        {
            get
            {
                return null;
            }
        }

        string IMigrationMetadata.Target
        {
            get
            {
                return Resources.GetString("Target");
            }
        }
    }
}
