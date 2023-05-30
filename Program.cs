using AutoMapper;
using ScopoERP.Models;
using ScopoERP.Services;
using Microsoft.EntityFrameworkCore;
using ScopoERP.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using ScopoERP.Configuration;
using System.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ERPContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.Configure<Token>(builder.Configuration.GetSection("Token"));

builder.Services.AddTransient<ERPContext>();

// Add services to the container.
builder.Services.AddScoped<DropDownService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<ItemCategoryService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CommissionService>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<UnitService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<RequisitionService>();
builder.Services.AddScoped<InventoryReceiveService>();
builder.Services.AddScoped<InventoryIssueService>();
builder.Services.AddScoped<ProductionService>();
builder.Services.AddScoped<SalesOrderService>();
builder.Services.AddScoped<InvoiceService>();
builder.Services.AddScoped<DesignationService>();
builder.Services.AddScoped<AccountTypeService>();
builder.Services.AddScoped<AccountBalanceService>();
builder.Services.AddScoped<CostCenterService>();
builder.Services.AddScoped<FinancialYearService>();
builder.Services.AddScoped<ParentAccountService>();
builder.Services.AddScoped<SubsidiaryAccountService>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<AttendanceService>();
builder.Services.AddScoped<LeaveApplicationService>();
builder.Services.AddScoped<LeaveTypeService>();
builder.Services.AddScoped<MonthlySalaryService>();
builder.Services.AddScoped<LcService>();
builder.Services.AddScoped<BackToBackLCService>();
builder.Services.AddScoped<DeliveryChallanService>();
builder.Services.AddScoped<CompanyService>();
builder.Services.AddScoped<DivisionService>();
builder.Services.AddScoped<PurchaseOrderService>();
builder.Services.AddScoped<StoreService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<PurchaseReturnService>();
builder.Services.AddScoped<SalesReturnService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<StoreTransferService>();
builder.Services.AddScoped<BankService>();
builder.Services.AddScoped<ProductionUnitService>();
builder.Services.AddScoped<HolidayService>();
builder.Services.AddScoped<WorkingShiftService>();
builder.Services.AddScoped<VoucherTypeService>();
builder.Services.AddScoped<ItemCriteriaService>();
builder.Services.AddScoped<LabTestService>();
builder.Services.AddScoped<FormulationService>();
builder.Services.AddScoped<ProductionPlanService>();
builder.Services.AddScoped<BillingService>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<DiscountTypeService>();
builder.Services.AddScoped<PromotionService>();
builder.Services.AddScoped<SalaryTypeService>();
builder.Services.AddScoped<SalaryIncrementService>();
builder.Services.AddScoped<GeoGraphService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration["Token:Key"]);

    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        RequireExpirationTime = false
    };
});


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ScopoERP.Helpers.MapperProfile));

builder.Services.AddIdentityCore<User>(options =>
        options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ERPContext>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ERP", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
     {
           new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference
                 {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                 }
             },
             new string[] {}
     }
    });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (builder.Environment.IsDevelopment())
// {
//     app.UseDeveloperExceptionPage();
// }

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ERP API V1");

});

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();