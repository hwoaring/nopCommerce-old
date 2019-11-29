using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;
using Nop.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Security;
using Nop.Core.Infrastructure;

using Senparc.CO2NET;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Cache.Redis;
using Senparc.CO2NET.Cache.Memcached;
using Senparc.CO2NET.RegisterServices;
using Senparc.CO2NET.Utilities;

using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.Cache.Memcached;
using Senparc.Weixin.MP;
using Senparc.Weixin.WxOpen;
using Senparc.Weixin.Work;
using Senparc.Weixin.TenPay;
using Senparc.Weixin.Open;
using Senparc.Weixin.Open.ComponentAPIs;

namespace Senparc.Weixin.MP.CommonService.Infrastructure.Extensions
{
    /// <summary>
    /// Represents extensions of IApplicationBuilder
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure SenparcWeixin
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseSenparcWeixin(this IApplicationBuilder application)
        {
            var senparcSetting = EngineContext.Current.Resolve<SenparcSetting>();
            var senparcWeixinSetting = EngineContext.Current.Resolve<SenparcWeixinSetting>();
            var hostingEnvironment = EngineContext.Current.Resolve<IHostingEnvironment>();

            // 启动 CO2NET 全局注册，必须！
            //关于 UseSenparcGlobal() 的更多用法见 CO2NET Demo：https://github.com/Senparc/Senparc.CO2NET/blob/master/Sample/Senparc.CO2NET.Sample.netcore/Startup.cs
            var register = RegisterService.Start(hostingEnvironment, senparcSetting).UseSenparcGlobal();

            //如果需要自动扫描自定义扩展缓存，可以这样使用：
            //register.UseSenparcGlobal(true);
            //如果需要指定自定义扩展缓存，可以这样用：
            //register.UseSenparcGlobal(false, GetExCacheStrategies);

            #region CO2NET 全局配置

            #region 全局缓存配置（按需）

            //当同一个分布式缓存同时服务于多个网站（应用程序池）时，可以使用命名空间将其隔离（非必须）
            register.ChangeDefaultCacheNamespace("DefaultCO2NETCache");

            #region 配置和使用 Redis          -- DPBMARK Redis

            //配置全局使用Redis缓存（按需，独立）
            var redisConfigurationStr = senparcSetting.Cache_Redis_Configuration;
            var useRedis = !string.IsNullOrEmpty(redisConfigurationStr) && redisConfigurationStr != "#{Cache_Redis_Configuration}#";  /*默认值，不启用*/
            if (useRedis)//这里为了方便不同环境的开发者进行配置，做成了判断的方式，实际开发环境一般是确定的，这里的if条件可以忽略
            {
                /* 说明：
                 * 1、Redis 的连接字符串信息会从 Config.SenparcSetting.Cache_Redis_Configuration 自动获取并注册，如不需要修改，下方方法可以忽略
                 * 2、如需手动修改，可以通过下方 SetConfigurationOption 方法手动设置 Redis 链接信息（仅修改配置，不立即启用）
                 */
                Senparc.CO2NET.Cache.Redis.Register.SetConfigurationOption(redisConfigurationStr);

                //以下会立即将全局缓存设置为 Redis
                Senparc.CO2NET.Cache.Redis.Register.UseKeyValueRedisNow();//键值对缓存策略（推荐）
                //Senparc.CO2NET.Cache.Redis.Register.UseHashRedisNow();//HashSet储存格式的缓存策略

                //也可以通过以下方式自定义当前需要启用的缓存策略
                //CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);//键值对
                //CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisHashSetObjectCacheStrategy.Instance);//HashSet
            }
            //如果这里不进行Redis缓存启用，则目前还是默认使用内存缓存 

            #endregion                        // DPBMARK_END

            #region 配置和使用 Memcached      -- DPBMARK Memcached

            //配置Memcached缓存（按需，独立）
            var memcachedConfigurationStr = senparcSetting.Cache_Memcached_Configuration;
            var useMemcached = !string.IsNullOrEmpty(memcachedConfigurationStr) && memcachedConfigurationStr != "#{Cache_Memcached_Configuration}#";
            if (useMemcached) //这里为了方便不同环境的开发者进行配置，做成了判断的方式，实际开发环境一般是确定的，这里的if条件可以忽略
            {
                application.UseEnyimMemcached();

                /* 说明：
                * 1、Memcached 的连接字符串信息会从 Config.SenparcSetting.Cache_Memcached_Configuration 自动获取并注册，如不需要修改，下方方法可以忽略
               /* 2、如需手动修改，可以通过下方 SetConfigurationOption 方法手动设置 Memcached 链接信息（仅修改配置，不立即启用）
                */
                Senparc.CO2NET.Cache.Memcached.Register.SetConfigurationOption(memcachedConfigurationStr);

                //以下会立即将全局缓存设置为 Memcached
                Senparc.CO2NET.Cache.Memcached.Register.UseMemcachedNow();

                //也可以通过以下方式自定义当前需要启用的缓存策略
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => MemcachedObjectCacheStrategy.Instance);
            }

            #endregion                        //  DPBMARK_END

            #endregion

            #region 注册日志（按需，建议）

            register.RegisterTraceLog(ConfigTraceLog);//配置TraceLog

            #endregion

            //Senparc.CO2NET.APM.Config.DataExpire = TimeSpan.FromMinutes(60);//测试APM缓存过期时间（默认情况下可以不用设置）

            #endregion

            #region 微信相关配置


            /* 微信配置开始
             * 
             * 建议按照以下顺序进行注册，尤其须将缓存放在第一位！
             */

            //注册开始

            #region 微信缓存（按需，必须在 register.UseSenparcWeixin() 之前）

            //微信的 Redis 缓存，如果不使用则注释掉（开启前必须保证配置有效，否则会抛错）         -- DPBMARK Redis
            if (useRedis)
            {
                application.UseSenparcWeixinCacheRedis();
            }                                                                                     // DPBMARK_END


            // 微信的 Memcached 缓存，如果不使用则注释掉（开启前必须保证配置有效，否则会抛错）    -- DPBMARK Memcached
            if (useMemcached)
            {
                application.UseSenparcWeixinCacheMemcached();
            }                                                                                      // DPBMARK_END


            #endregion


            //开始注册微信信息，必须！
            register.UseSenparcWeixin(senparcWeixinSetting, senparcSetting)
            //注意：上一行没有 ; 下面可接着写 .RegisterXX()

            #region 注册公众号或小程序（按需）

            //注册公众号（可注册多个）                                                -- DPBMARK MP
                .RegisterMpAccount(senparcWeixinSetting, "公众号")// DPBMARK_END


                //注册多个公众号或小程序（可注册多个）                                        -- DPBMARK MiniProgram
                //.RegisterWxOpenAccount(senparcWeixinSetting, "【盛派网络小助手】小程序")// DPBMARK_END

                //除此以外，仍然可以在程序任意地方注册公众号或小程序：
                //AccessTokenContainer.Register(appId, appSecret, name);//命名空间：Senparc.Weixin.MP.Containers
            #endregion

            #region 注册企业号（按需）           -- DPBMARK Work

                //注册企业微信（可注册多个）
                //.RegisterWorkAccount(senparcWeixinSetting, "【盛派网络】企业微信")

                //除此以外，仍然可以在程序任意地方注册企业微信：
                //AccessTokenContainer.Register(corpId, corpSecret, name);//命名空间：Senparc.Weixin.Work.Containers
            #endregion                          // DPBMARK_END

            #region 注册微信支付（按需）        -- DPBMARK TenPay

                //注册旧微信支付版本（V2）（可注册多个）
                //.RegisterTenpayOld(senparcWeixinSetting, "【盛派网络小助手】公众号")//这里的 name 和第一个 RegisterMpAccount() 中的一致，会被记录到同一个 SenparcWeixinSettingItem 对象中

                //注册最新微信支付版本（V3）（可注册多个）
                .RegisterTenpayV3(senparcWeixinSetting, "公众号") //这里的 name 和第一个 RegisterMpAccount() 中的一致,记录到同一个 SenparcWeixinSettingItem 对象中

            #endregion                          // DPBMARK_END

            #region 注册微信第三方平台（按需）  -- DPBMARK Open

                //注册第三方平台（可注册多个）
                //.RegisterOpenComponent(senparcWeixinSetting,
                //   //getComponentVerifyTicketFunc
                //   async componentAppId =>
                //   {
                //       var dir = Path.Combine(ServerUtility.ContentRootMapPath("~/App_Data/OpenTicket"));
                //       if (!Directory.Exists(dir))
                //       {
                //           Directory.CreateDirectory(dir);
                //       }

                //       var file = Path.Combine(dir, string.Format("{0}.txt", componentAppId));
                //       using (var fs = new FileStream(file, FileMode.Open))
                //       {
                //           using (var sr = new StreamReader(fs))
                //           {
                //               var ticket = await sr.ReadToEndAsync();
                //               return ticket;
                //           }
                //       }
                //   },

                //  //getAuthorizerRefreshTokenFunc
                //  async (componentAppId, auhtorizerId) =>
                //  {
                //      var dir = Path.Combine(ServerUtility.ContentRootMapPath("~/App_Data/AuthorizerInfo/" + componentAppId));
                //      if (!Directory.Exists(dir))
                //      {
                //          Directory.CreateDirectory(dir);
                //      }

                //      var file = Path.Combine(dir, string.Format("{0}.bin", auhtorizerId));
                //      if (!File.Exists(file))
                //      {
                //          return null;
                //      }

                //      using (Stream fs = new FileStream(file, FileMode.Open))
                //      {
                //          var binFormat = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //          var result = (RefreshAuthorizerTokenResult)binFormat.Deserialize(fs);
                //          return result.authorizer_refresh_token;
                //      }
                //  },

                //     //authorizerTokenRefreshedFunc
                //     (componentAppId, auhtorizerId, refreshResult) =>
                //     {
                //         var dir = Path.Combine(ServerUtility.ContentRootMapPath("~/App_Data/AuthorizerInfo/" + componentAppId));
                //         if (!Directory.Exists(dir))
                //         {
                //             Directory.CreateDirectory(dir);
                //         }

                //         var file = Path.Combine(dir, string.Format("{0}.bin", auhtorizerId));
                //         using (Stream fs = new FileStream(file, FileMode.Create))
                //         {
                //             //这里存了整个对象，实际上只存RefreshToken也可以，有了RefreshToken就能刷新到最新的AccessToken
                //             var binFormat = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //             binFormat.Serialize(fs, refreshResult);
                //             fs.Flush();
                //         }
                //     }, "【盛派网络】开放平台")

            //除此以外，仍然可以在程序任意地方注册开放平台：
            //ComponentContainer.Register();//命名空间：Senparc.Weixin.Open.Containers
            #endregion                          // DPBMARK_END

            ;

            /* 微信配置结束 */

            #endregion
        }

        /// <summary>
        /// 配置微信跟踪日志
        /// </summary>
        private static void ConfigTraceLog()
        {
            //这里设为Debug状态时，/App_Data/WeixinTraceLog/目录下会生成日志文件记录所有的API请求日志，正式发布版本建议关闭

            //如果全局的IsDebug（Senparc.CO2NET.Config.IsDebug）为false，此处可以单独设置true，否则自动为true
            CO2NET.Trace.SenparcTrace.SendCustomLog("系统日志", "系统启动");//只在Senparc.Weixin.Config.IsDebug = true的情况下生效

            //全局自定义日志记录回调
            CO2NET.Trace.SenparcTrace.OnLogFunc = () =>
            {
                //加入每次触发Log后需要执行的代码
            };

            //当发生基于WeixinException的异常时触发
            WeixinTrace.OnWeixinExceptionFunc = ex =>
            {
                //加入每次触发WeixinExceptionLog后需要执行的代码

                //发送模板消息给管理员                             -- DPBMARK Redis
                var eventService = new Senparc.Weixin.MP.CommonService.EventService();
                eventService.ConfigOnWeixinExceptionFunc(ex);      // DPBMARK_END
            };
        }
    }
}
