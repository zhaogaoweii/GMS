
namespace ZGW.GMS.Core.OMSFile.Helper
{
    /// <summary>
    /// 文件上传模式
    /// </summary>
    public enum UploadMode
    {
        /// <summary>
        /// FTP远程上传
        /// </summary>
        FTP = 1,

        /// <summary>
        /// 共享目录上传操作
        /// </summary>
        UNC = 2,

        /// <summary>
        /// 本地上传
        /// </summary>
        LOC = 3,

        /// <summary>
        /// 无模式
        /// </summary>
        NON = 0
    }
}
