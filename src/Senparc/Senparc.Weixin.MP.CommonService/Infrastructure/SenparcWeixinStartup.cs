using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;

using Senparc.Weixin.MP.CommonService.Infrastructure.Extensions;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.RegisterServices;

namespace Senparc.Weixin.MP.CommonService.Infrastructure
{
    /// <summary>
    /// Represents object for the configuring SenparcWeixin features and middleware on application startup
    /// </summary>
    public class SenparcWeixinStartup : INopStartup
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSenparcGlobalServices(configuration)   //Senparc.CO2NET 全局注册
                    .AddSenparcWeixinServices(configuration);  //Senparc.Weixin 注册
                    //.AddSenparcWebSocket<CustomNetCoreWebSocketMessageHandler>();  //Senparc.WebSocket 注册（按需）
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            //use SenparcWeixin
            application.UseSenparcWeixin();
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 101;
    }
}