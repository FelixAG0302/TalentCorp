using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using TalentCorp.Authentication;
using TalentCorp.Context;

namespace TalentCorp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<TalentCorpContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("TalentCorpContext")));

        builder.Services.AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Authentication}/{action=Login}/{id?}");

        app.Run();
    }
}