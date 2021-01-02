using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using quewaner.Blog.CMS.Extensions.OSS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace quewaner.Blog.CMS.Controllers
{
    public class UploadController : Controller
    {
        private readonly ILogger<UploadController> _logger;

        public UploadController(ILogger<UploadController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Layui图片上传接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult LayuiUploadFile()
        {

            #region OSS上传  下面有本地上传案例
            IFormFile formFiles = HttpContext.Request.Form.Files?.FirstOrDefault();
            try
            {
                string fileName = $"/Blog/{DateTimeOffset.Now.ToUnixTimeSeconds()}_{formFiles?.FileName}";
                using (MemoryStream memory = new MemoryStream())
                {
                    formFiles.CopyToAsync(memory);
                    var putObjectResult = TencentOSSHelper.UploadFile(fileName, memory.ToArray());
                    if (putObjectResult?.IsSuccessful() ?? false)
                    {
                        return Ok(new { code = 0, msg = "成功", data = new { src = "https://common-1304279371.file.myqcloud.com"+fileName + "/blog" } }); //Layui特定返回格式

                    }
                }
                return Ok(new { code = 1, msg = "失败" });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return Ok(new { code = 1, msg = "失败" });

            }
            #endregion

        }


        /// <summary>
        /// editormd 文件上传接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditorUploadFile()
        {
            IFormFile formFiles = HttpContext.Request.Form.Files?.FirstOrDefault();

            #region 本地上传

            //try
            //{
            //    string fileName = $"{DateTimeOffset.Now.ToUnixTimeSeconds()}_{formFiles?.FileName}";
            //    string savePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "blogImages", fileName);
            //    using (FileStream fs = System.IO.File.Create(savePath))
            //    {
            //        formFiles.CopyToAsync(fs);
            //    }

            //    return Ok(new { success = 1, message = "成功", url = Path.Combine("/blogImages", fileName) });  //编辑器固定的返回格式
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogCritical(ex,ex.Message);
            //    return Ok(new { success = 0, message = "失败" });

            //}
            #endregion


            #region 腾讯云OSS上传
            try
            {
                string fileName = $"/Blog/{DateTimeOffset.Now.ToUnixTimeSeconds()}_{formFiles?.FileName}";
                using (MemoryStream memory = new MemoryStream())
                {
                    formFiles.CopyToAsync(memory);
                    var putObjectResult = TencentOSSHelper.UploadFile(fileName, memory.ToArray());
                    if (putObjectResult?.IsSuccessful() ?? false)
                    {
                        return Ok(new { success = 1, message = "成功", url = "https://common-1304279371.file.myqcloud.com" + fileName+ "/blog" }); //编辑器固定的返回格式
                    }
                }
                return Ok(new { success = 0, message = "失败" });

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return Ok(new { success = 0, message = "失败" });

            }
            #endregion
        }
    }
}
