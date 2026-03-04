To view the docs folder files -> Right click Soln -> Add New Solution folder which creates a logical folder
	Right click this folder and choose the file to be viewed
	Create in similar way but manually move the files in Windows explorer

**Description:**

1. Add Swagger package and add in program.cs

Under EntityModels folder:
2. Create an EntityModel class

Under DTOs folder:
3. Create DTOs representing API request and response properties with data annotations


**Notes**:

1. Keep the DTO in sync with Database for eg: length
2. Setup Range([0, .MaxValue]) so that it does not accept negative values
3. required on property is compile time which will not allow to create instance without a value for this property
   however, = default! supresses the warning and confirms this will be assigned a non-null value
4. [Required] is runtime validation annotation
5. Attribute order does not matter


6. When we want to update certain fields that are non-null add ? to the type in DTO and donot keep it as [Required]
7. Make sure to have .ForAllMembers(condition for != null) while mapping, so that it will overwrite only non-null values in the retrieved entity 