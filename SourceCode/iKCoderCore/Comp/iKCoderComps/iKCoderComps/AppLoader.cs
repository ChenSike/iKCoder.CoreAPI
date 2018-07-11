using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace iKCoderComps
{
    public class AppLoader
    {
		public static void LoadConfiguration_AllowCros(ref IServiceCollection services)
		{
			services.AddCors(options =>
			options.AddPolicy("AllowSameDomain", builder => builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));
		}
    }
}
