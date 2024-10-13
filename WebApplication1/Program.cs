// partie ajouter --------------------------Inscription des services Repositories------------------------------------
using WebApplication1.Models.Repositories;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// partie ajouter ----------------------déclare les service addSingleton tasna3 instance wahda min service
// / add transient ; koul appel tasna3 instance ----------------------------------------
builder.Services.AddSingleton<IRepository<Employee>, EmployeeRepository>();
//--------------------------------------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
