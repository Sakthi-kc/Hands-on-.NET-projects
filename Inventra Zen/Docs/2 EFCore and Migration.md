1. Add EntityFrameowrkCore SQLServer and Tools package

Under Data folder:
2. Create a DBContext file, inject DBContext and add DbSet<EntityModelClass> tableName;

3. Create SQLConnectionString and add in appSettings.Development.json

4. Add the SqlServer with the connection string in program.cs

5. Create a constructor in DBContext and add the options to base DBContext

Under same Data folder:
6. Create a Config folder
7. Create an EntityTypeConfig file, inject IEntityTypeConfiguration<EntityModelClass> 
8. Add void Configure(EntityTypeBuilder builder) method to define the database table schema which can be modified whenever needed

9. Add a protected override void OnModelCreating(ModelBuilder modelbuilder) method in DBContext class and add this config class reference


**Notes**

Fluent API in EF Core is a way to configure your entity classes using code, instead of using attributes/annotations in the class.
This is achieved using EntityConfig file.

In Entity Config file, means this is a fluent api. It can have the following:

a. We can add a property as primary key with HasKey() and auto increment values using UseIdentity()
b. We can define schema properties for all the columns
c. We can define a column as computed using SQL command in HasComputedColumnSql()
d. We can seed intial data using HasData() which will add this row to DB when that id does not exist and only via migration

10. Migration files are created using EFCore console which generates SQL commands to update database
11. To update database in SQL Server, use Add-Migration {Comment} and then Update-Database
12. We can modify the config file, then use Add-Migration and Update-Database
13. We can also do Remove-Migration for last non applied migration. If applied, Update-Database {PrevMigName} then Remove-Migration
14. We can also do Drop-Database which will completely remove the table