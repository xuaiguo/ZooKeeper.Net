﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZKClientNET.Exceptions;
using ZKClientNET.Serialize;

namespace ZKClientNET.Client
{
    /// <summary>
    /// ZKClient辅助创建类
    /// </summary>
   public  class ZKClientBuilder
    {
        private int connectionTimeout = int.MaxValue;
        private IZKSerializer zkSerializer = new SerializableSerializer();
        private string servers;
        private int sessionTimeout = 30000;
        private int retryTimeout = -1;

        /// <summary>
        /// 创建ZKClient
        /// </summary>
        /// <returns></returns>
        public static ZKClientBuilder NewZKClient()
        {
            ZKClientBuilder builder = new ZKClientBuilder();
            return builder;
        }

        /// <summary>
        /// 创建ZKClient
        /// </summary>
        /// <param name="servers"></param>
        /// <returns></returns>
        public static ZKClientBuilder NewZKClient(string servers)
        {
            ZKClientBuilder builder = new ZKClientBuilder();
            builder.Servers(servers);
            return builder;
        }

        /// <summary>
        /// 组件并初始化ZKClient
        /// </summary>
        /// <returns></returns>
        public ZKClient Build()
        {
            if (string.IsNullOrEmpty(servers))
            {
                throw new ZKException("Servers can not be empty !");
            }
            ZKClient zkClient = new ZKClient(servers, new TimeSpan(0, 0, 0, 0, sessionTimeout), new TimeSpan(0, 0, 0, 0, connectionTimeout), zkSerializer, retryTimeout);
            return zkClient;
        }

        /// <summary>
        /// 设置服务器地址
        /// </summary>
        /// <param name="servers"></param>
        /// <returns></returns>
        public ZKClientBuilder Servers(string servers)
        {
            this.servers = servers;
            return this;
        }

        /// <summary>
        /// 设置序列化类，可选.
        /// </summary>
        /// <param name="zkSerializer"></param>
        /// <returns></returns>
        public ZKClientBuilder Serializer(IZKSerializer zkSerializer)
        {
            this.zkSerializer = zkSerializer;
            return this;
        }

        /// <summary>
        /// 设置会话失效时间，可选
        /// （默认值：30000，实际大小ZooKeeper会重新计算大概在2* tickTime ~ 20 * tickTime）
        /// </summary>
        /// <param name="sessionTimeout"></param>
        /// <returns></returns>
        public ZKClientBuilder SessionTimeout(int sessionTimeout)
        {
            this.sessionTimeout = sessionTimeout;
            return this;
        }

        /// <summary>
        /// 连接超时时间，可选。
        ///默认值int.MaxValue
        /// </summary>
        /// <param name="connectionTimeout"></param>
        /// <returns></returns>
        public ZKClientBuilder ConnectionTimeout(int connectionTimeout)
        {
            this.connectionTimeout = connectionTimeout;
            return this;
        }

        /// <summary>
        ///重试超时时间，可选，主要用于ZooKeeper与服务器断开后的重连。
        /// 默认值-1，也就是没有超时限制
        /// </summary>
        /// <param name="retryTimeout"></param>
        /// <returns></returns>
        public ZKClientBuilder RetryTimeout(int retryTimeout)
        {
            this.retryTimeout = retryTimeout;
            return this;
        }
    }
}
