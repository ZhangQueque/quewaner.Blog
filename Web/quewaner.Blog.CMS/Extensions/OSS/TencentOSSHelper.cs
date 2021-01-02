using COSXML;
using COSXML.Auth;
using COSXML.Model.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quewaner.Blog.CMS.Extensions.OSS
{
    public class TencentOSSHelper
    { /// <summary>
      /// 上传二进制数据
      /// </summary>
      /// <param name="fileName">文件名</param>
      /// <param name="fileData">二进制数据</param>
        public static PutObjectResult UploadFile(string fileName, byte[] fileData)
        {
            try
            {
                //1、CosXmlConfig提供配置 SDK 接口
                //初始化 CosXmlConfig 
                string appid = "1304279371";//设置腾讯云账户的账户标识 APPID
                string region = "ap-shanghai"; //设置一个默认的存储桶地域
                CosXmlConfig config = new CosXmlConfig.Builder()
                  .IsHttps(true)  //设置默认 HTTPS 请求
                  .SetRegion(region)  //设置一个默认的存储桶地域
                  .SetDebugLog(true)  //显示日志
                  .Build();  //创建 CosXmlConfig 对象
                             //2、提供访问凭证
                string secretId = "AKIDtyWqxqEPs42tVreVWGucDZfX9WMMvk4W"; //"云 API 密钥 SecretId";
                string secretKey = "NWOYR7Yhb5MSgfQwBHTGo6hqkNkriKgl"; //"云 API 密钥 SecretKey";
                long durationSecond = 600;  //每次请求签名有效时长，单位为秒
                QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(
                  secretId, secretKey, durationSecond);
                //3、初始化 CosXmlServer
                CosXml cosXml = new CosXmlServer(config, cosCredentialProvider);
                string bucket = "common-1304279371"; //存储桶，格式：BucketName-APPID
                string cosPath = fileName; // 对象键
                PutObjectRequest putObjectRequest = new PutObjectRequest(bucket, cosPath, fileData);

                return cosXml.PutObject(putObjectRequest);
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
                return null;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                return null;

            }
        }
    }
}
