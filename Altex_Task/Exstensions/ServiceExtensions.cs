//global using LoggerService;
//global using Contracts;
//using Entities;
//using Repository;

//namespace Altex_Task.Exstensions
//{
//    public static class ServiceExtensions
//    {
//        public static void ConfigureCors(this IServiceCollection services)
//        {
//            services.AddCors(options =>
//            {
//                options.AddPolicy("CorsPolicy",
//                    builder => builder.AllowAnyOrigin()
//                    .AllowAnyMethod()
//                    .AllowAnyHeader());
//            });
//        }

//        public static void ConfigureIISINtegration(this IServiceCollection services)
//        {
//            services.Configure<IISOptions>(options =>
//            {

//            });
//        }
//        public static void ConfigureJWT(this IServiceCollection services)
//        {
//            services.AddAuthentication(opt =>
//            {
//                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            })
//            .AddJwtBearer(options =>
//            {
//                options.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuer = false,
//                    ValidateAudience = false,
//                    ValidateLifetime = true,
//                    ValidateIssuerSigningKey = true,

//                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKey"))
//                };
//            });
//        }


//        public static void AddSqlite<TDbContext>(this IServiceCollection services, string connectionString)
//            where TDbContext : DbContext
//        {
//            services.AddDbContext<TDbContext>(option =>
//            {
//                option.UseSqlServer(connectionString);
//            });
//        }

//        public static void ConfigureIdentity<TDbContext, TUser>(this IServiceCollection services)
//            where TUser : IdentityUser
//            where TDbContext : DbContext
//        {
//            services.AddIdentity<TUser, IdentityRole>(options =>
//            {
//                options.Password.RequiredLength = 1;
//                options.Password.RequireNonAlphanumeric = false;
//                options.Password.RequireDigit = false;
//                options.Password.RequireUppercase = false;
//                options.User.RequireUniqueEmail = true;
//            }).AddEntityFrameworkStores<TDbContext>();
//        }

//        public static void ConfigureLoggerService(this IServiceCollection services)
//        {
//            services.AddSingleton<ILoggerManager, LoggerManager>();
//        }

//        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
//        {
//            var connectionString = config["mysqlconnection:conncetionString"];

//            services.AddDbContext<RepositoryContext>(o => o.UseMySql(connectionString,
//                MySqlServerVersion.LatestSupportedServerVersion));
//            /*o => o.UseSqlServer(connectionString, op => op.MigrationsAssembly("Altex_Task"))*/

//        }

//        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
//        {
//            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
//        }
//    }
//}
