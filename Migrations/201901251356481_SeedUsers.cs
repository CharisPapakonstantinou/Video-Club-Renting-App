namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'1ab53eb1-2fab-414c-8247-142b62d02648', N'admin@vidly.com', 0, N'AEbBZqN9cBC6dVSruAE1Fe3iF0d2a1eDyVixaQqf1JusCCeedhQ03eQADuZh5a5w7Q==', N'08b9e003-ede0-434a-a922-676138b0ee74', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5282eccc-0dee-4399-9f25-303440655a6d', N'guest@vidly.com', 0, N'AEmpBot7yCcOZv7xNiuymq2T41KcXV49dt4mCwl0m4L6JH5prO/ioSoawTTMqflTOA==', N'bc52778e-6a31-4e69-8834-405aad4270ca', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

                    INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'5c937d48-a7ed-42d6-9a68-5e116eaab720', N'CanManageMovies')

                    INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1ab53eb1-2fab-414c-8247-142b62d02648', N'5c937d48-a7ed-42d6-9a68-5e116eaab720')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
