using Weblog.Application.Common.Interfaces;
using Weblog.Infrastructure.Files;
using Weblog.Infrastructure.Persistence;
using Weblog.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using System.Reflection;
using Weblog.Infrastructure.Persistence.Configurations.MongoDb;
using Weblog.Infrastructure.Models;

namespace Weblog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("WeblogDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IBlogsSerieRepository, BlogsSerieRepository>();
        services.AddScoped<IMongoDbSettings, MongoDbSettings>();

        services.AddScoped<IDomainEventService, DomainEventService>();

        services.AddTransient<IDateTime, DateTimeService>();
        //services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

        services.AddAuthentication();

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        services.AddSettingModels(configuration);
        RegisterMongoDbClassMaps();
        return services;
    }

    /// <summary>
    /// Apply mongodb entities configurations.
    /// </summary>
    public static void RegisterMongoDbClassMaps()
    {
        Type baseType = typeof(IBaseBsonClassMap<,>);
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type bsonClassMapType = typeof(BsonClassMap<>);

        var configClasses = assembly.GetTypes().Where(t =>
                t.IsClass &&
                t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == baseType))
            .Select(t => new
                {
                    Type = t,
                    EntityType = t.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == baseType).GenericTypeArguments[0]
                })
            .ToList();

        foreach (var configClass in configClasses)
        {
            // BsonClassMap<Entity>
            Type bsonClassMapGenType = bsonClassMapType.MakeGenericType(configClass.EntityType);
            // BsonClassMap<Entity> Instance
            object classMapInstance = Activator.CreateInstance(bsonClassMapGenType);

            // EntityConfiguration : IBaseBsonClassMap<Entity, EntityConfiguration>
            object instance = Activator.CreateInstance(configClass.Type);

            // bc.AutoMap()
            bsonClassMapGenType.GetMethod(nameof(BsonClassMap.AutoMap)).Invoke(classMapInstance, null);

            configClass.Type.GetMethod("Configure").Invoke(instance, new object[] { classMapInstance });

            BsonClassMap.RegisterClassMap((BsonClassMap)classMapInstance);
        }
    }

    /// <summary>
    /// Registers the interfaces that has inherite <see cref="IIsSettingModel"/>.
    /// </summary>
    public static void AddSettingModels(this IServiceCollection services, IConfiguration configuration)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type isSettingType = typeof(IIsSettingModel);

        var settingInterfaces = assembly.GetTypes().Where(t =>
                t.IsInterface &&
                t.GetInterfaces().Any(i => i.IsInterface && i == isSettingType)).ToList();

        var settingClasses = assembly.GetTypes().Where(t =>
                t.IsClass &&
                t.GetInterfaces().Any(i => i.IsInterface && settingInterfaces.Any(ii => ii == i))).ToList();

        foreach (var @interface in settingInterfaces)
        {
            var @class = settingClasses.First(c => c.GetInterfaces().Any(i => i.IsInterface && i == @interface));

            var obj = configuration.GetSection($"Settings:{@class.Name}").Get(@class);

            services.AddSingleton(@interface, obj);
        }
    }
}
