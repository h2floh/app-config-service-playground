using Microsoft.Extensions.Configuration;
using Microsoft.Azure.AppConfiguration.AspNetCore;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using TestAppConfig;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the connection string
string connectionString = builder.Configuration.GetConnectionString("AppConfiguration")
    ?? throw new InvalidOperationException("The connection string 'AppConfiguration' was not found.");

// Load configuration from Azure App Configuration
// builder.Configuration.AddAzureAppConfiguration(connectionString);

// Load configuration from Azure App Configuration
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(connectionString)
            // Load all keys that start with `TestApp:` and have no label
            .Select("TestApp:*", LabelFilter.Null)
            // Configure to reload configuration if the registered sentinel key is modified
            .ConfigureRefresh(refreshOptions =>
                refreshOptions.Register("TestApp:Settings:Sentinel", refreshAll: true));
    // Load all feature flags with no label
    options.UseFeatureFlags(featureFlagOptions =>
    {
        //featureFlagOptions.Select("TestApp:*", "dev");
        featureFlagOptions.SetRefreshInterval(TimeSpan.FromSeconds(10));
    });
});

// Add services to the container.
builder.Services.AddRazorPages();

// Add Azure App Configuration middleware to the container of services.
builder.Services.AddAzureAppConfiguration();

// Add feature management to the container of services.
builder.Services.AddFeatureManagement().WithTargeting();

// Bind configuration "TestApp:Settings" section to the Settings object
builder.Services.Configure<Settings>(builder.Configuration.GetSection("TestApp:Settings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Use Azure App Configuration middleware for dynamic configuration refresh.
app.UseAzureAppConfiguration();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
