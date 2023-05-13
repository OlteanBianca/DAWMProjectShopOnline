using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.AppDbContext;
using OnlineShop.Repositories;
using OnlineShop.Services;
using System.Text;

namespace OnlineShop.Settings
{
    public static class Dependencies
    {
        #region Private Methods
        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IShopService, ShopService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IInventoryService, InventoryService>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<UnitOfWork>();
            services.AddScoped<UserRepository>();
            services.AddScoped<RoleRepository>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<ShopRepository>();
            services.AddScoped<OrderedProductRepository>();
            services.AddScoped<InventoryRepository>();
        }
        #endregion

        #region Public Methods
        public static void Inject(WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Services.AddEndpointsApiExplorer();
            applicationBuilder.Services.AddControllers();
            applicationBuilder.Services.AddSwaggerGen();

            applicationBuilder.Services.AddDbContext<ShopDbContext>(options =>
            {
                options.UseSqlServer(applicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });

            applicationBuilder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = "LabAPI-Backend",
                    ValidAudience = "LabAPI-Anyone",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(applicationBuilder.Configuration["JWT:SecurityKey"]))
                };
            });

            applicationBuilder.Services.AddAuthorization();

            AddRepositories(applicationBuilder.Services);
            AddServices(applicationBuilder.Services);
        }
        #endregion
    }
}
