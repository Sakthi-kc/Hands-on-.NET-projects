To view the docs folder files -> Right click Soln -> Add New Solution folder which creates a logical folder
	Right click this folder and choose the file to be viewed
	Create in similar way but manually move the files in Windows explorer

**Description:**

1. Add Swagger package and add in program.cs

Under EntityModels folder:
2. Create an EntityModel class

Under DTOs folder:
3. Create DTOs representing API request and response properties

4. Add EntityFrameowrkCore SQLServer and Tools package

Under Data folder:
5. Create a DBContext file, inject DBContext and add DbSet<EntityModelClass> tableName;

6. Create SQLConnectionString and add in appSettings.Development.json

7. Add the SqlServer with the connection string in program.cs

8. Create a constructor in DBContext and add the options to base DBContext

Under same Data folder:
9. Create a Config folder
	10. Create an EntityTypeConfig file, inject IEntityTypeConfiguration<EntityModelClass> 
	11. Add void Configure(EntityTypeBuilder builder) method to define the database table schema and data which can be modified whenever needed

12. Add a protected override void OnModelCreating(ModelBuilder modelbuilder) method in DBContext class and add this config class reference


**Notes**

In Entity Config file,

a. We can add a property as primary key with HasKey() and auto increment values using UseIdentity()
b. We can define schema properties for all the columns
c. We can define a column as computed using SQL command in HasComputedColumnSql()