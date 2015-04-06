using System;
using System.Drawing;
using System.Web;
namespace COM.SingNo.Common
{
    /// <summary>
    /// 随机旋转的可视验证码图象
    /// </summary>
    public  class VerifyCode
    {
        public enum StringMode
        {
            /// <summary>
            /// 小写字母
            /// </summary>
            LowerLetter,
            /// <summary>
            /// 大写字母
            /// </summary>
            UpperLetter,
            /// <summary>
            /// 混合大小写字母
            /// </summary>
            Letter,
            /// <summary>
            /// 数字
            /// </summary>
            Digital,
            /// <summary>
            /// 混合数字与大小字母
            /// </summary>
            Mix
        }
        private static string _key = "verify";
        //是否消除锯齿
        public static bool FontTextRenderingHint = false;
        //验证码字体
        public static string ValidateCodeFont = "Arial";
        public static int ImageHeight = 28;
        
        #region 创建指定长度的和模式的随机数
        private static string GenerateRandomString(int length, StringMode mode)
        {
            string rndStr = string.Empty;
            if (length == 0)
                return rndStr;
            Random random = new Random();
            switch (mode)
            {
                case StringMode.Digital:
                    char[] digitals = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                    for (int i = 0; i < length; ++i)
                        rndStr += digitals[random.Next(0, digitals.Length)];
                    break;
                case StringMode.LowerLetter:
               //     char[] lowerLetters = new char[26] {
               //'a', 'b', 'c', 'd', 'e', 'f', 'g', 
               //'h', 'i', 'j', 'k', 'l', 'm', 'n', 
               //'o', 'p', 'q', 'r', 's', 't', 
               //'u', 'v', 'w', 'x', 'y', 'z' };
                    char[] lowerLetters = new char[23] {
               'a', 'b', 'c', 'd', 'e', 'f', 'g', 
               'h', 'j', 'k', 'm', 'n', 
               'o', 'p', 'q', 'r', 's', 't', 
               'u', 'v', 'w', 'x', 'z' };
                    for (int i = 0; i < length; ++i)
                        rndStr += lowerLetters[random.Next(0, lowerLetters.Length)];
                    break;
                case StringMode.UpperLetter:
               //     char[] upperLetters = new char[26] {
               //'A', 'B', 'C', 'D', 'E', 'F', 'G', 
               //'H', 'I', 'J', 'K', 'L', 'M', 'N', 
               //'O', 'P', 'Q', 'R', 'S', 'T', 
               //'U', 'V', 'W', 'X', 'Y', 'Z' };
                    char[] upperLetters = new char[24] {
               'A', 'B', 'C', 'D', 'E', 'F', 'G', 
               'H', 'J', 'K', 'M', 'N', 
               'O', 'P', 'Q', 'R', 'S', 'T', 
               'U', 'V', 'W', 'X', 'Y', 'Z' };
                    for (int i = 0; i < length; ++i)
                        rndStr += upperLetters[random.Next(0, upperLetters.Length)];
                    break;
                case StringMode.Letter:
               //     char[] letters = new char[52]{
               //'a', 'b', 'c', 'd', 'e', 'f', 'g', 
               //'h', 'i', 'j', 'k', 'l', 'm', 'n', 
               //'o', 'p', 'q', 'r', 's', 't', 
               //'u', 'v', 'w', 'x', 'y', 'z',
               //'A', 'B', 'C', 'D', 'E', 'F', 'G', 
               //'H', 'I', 'J', 'K', 'L', 'M', 'N', 
               //'O', 'P', 'Q', 'R', 'S', 'T', 
               //'U', 'V', 'W', 'X', 'Y', 'Z' };
                    char[] letters = new char[47]{
               'a', 'b', 'c', 'd', 'e', 'f', 'g', 
               'h', 'j', 'k', 'm', 'n', 
               'o', 'p', 'q', 'r', 's', 't', 
               'u', 'v', 'w', 'x', 'z',
               'A', 'B', 'C', 'D', 'E', 'F', 'G', 
               'H', 'J', 'K', 'M', 'N', 
               'O', 'P', 'Q', 'R', 'S', 'T', 
               'U', 'V', 'W', 'X', 'Y', 'Z' };
                    for (int i = 0; i < length; ++i)
                        rndStr += letters[random.Next(0, letters.Length)];
                    break;
                default:
                    //     char[] mix = new char[62]{
                    //'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                    //'a', 'b', 'c', 'd', 'e', 'f', 'g', 
                    //'h', 'i', 'j', 'k', 'l', 'm', 'n', 
                    //'o', 'p', 'q', 'r', 's', 't', 
                    //'u', 'v', 'w', 'x', 'y', 'z',
                    //'A', 'B', 'C', 'D', 'E', 'F', 'G', 
                    //'H', 'I', 'J', 'K', 'L', 'M', 'N', 
                    //'O', 'P', 'Q', 'R', 'S', 'T', 
                    //'U', 'V', 'W', 'X', 'Y', 'Z' };
                    char[] mix = new char[56]{
               '0', '2', '3', '4', '5', '6', '7', '8', '9',
               'a', 'b', 'c', 'd', 'e', 'f', 'g', 
               'h', 'j', 'k', 'm', 'n', 
               'o', 'p', 'q', 'r', 's', 't', 
               'u', 'v', 'w', 'x', 'z',
               'A', 'B', 'C', 'D', 'E', 'F', 'G', 
               'H', 'J', 'K', 'M', 'N', 
               'O', 'P', 'Q', 'R', 'S', 'T', 
               'U', 'V', 'W', 'X', 'Y', 'Z' };
                    for (int i = 0; i < length; ++i)
                        rndStr += mix[random.Next(0, mix.Length)];
                    break;
            }
            return rndStr;
        }
        #endregion

     
        /// <summary>
        /// 显示验证码
        /// </summary>
        /// <param name="text">验证码字符</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="mode">随机字符模式</param>
        /// <param name="clrFont">字体颜色</param>
        /// <param name="clrBg">背景颜色</param>
        private static void ShowVerifyCode(string text, int fontSize, Color clrFont, Color clrBg)
        {
            //获得验证码字符
            char[] CodeCharArray = text.ToCharArray(0, text.Length);
            //图像的宽度-与验证码的长度成一定比例
            int ImageWidth = (int)(text.Length * fontSize * 1.3 + 4);
            //创建一个长20，宽iwidth的图像对象
            Bitmap Image = new Bitmap(ImageWidth, ImageHeight);
            //创建一个新绘图对象
            Graphics ImageGraphics = Graphics.FromImage(Image);
            //清除背景色，并填充背景色
            //Note:Color.Transparent为透明
            ImageGraphics.Clear(clrBg);
            //绘图用的字体和字号
            Font CodeFont = new Font(ValidateCodeFont, fontSize, FontStyle.Bold);
            //绘图用的刷子大小
            Brush ImageBrush = new SolidBrush(clrFont);
            //字体高度计算
            int FontHeight = (int)Math.Max(ImageHeight - fontSize - 3, 2);
            //创建随机对象
            Random rand = new Random();
            //开始随机安排字符的位置，并画到图像里
            for (int i = 0; i < text.Length; i++)
            {
                //生成随机点，决定字符串的开始输出范围
                int[] FontCoordinate = new int[2];
                FontCoordinate[0] = (int)(i * fontSize + rand.Next(1)) + 3;
                FontCoordinate[1] = rand.Next(FontHeight);
                Point FontDrawPoint = new Point(FontCoordinate[0], FontCoordinate[1]);
                //消除锯齿操作
                if (FontTextRenderingHint)
                    ImageGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
                else
                    ImageGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                //格式化刷子属性-用指定的刷子、颜色等在指定的范围内画图
                ImageGraphics.DrawString(CodeCharArray[i].ToString(), CodeFont, ImageBrush, FontDrawPoint);
            }

            //创建铅笔对象
            Pen ImagePen = new Pen(clrFont, 1);
            //创建随机点
            Point[] RandPoint = new Point[2];
            //随机画线
            for (int i = 0; i < 6; i++)
            {
                RandPoint[0] = new Point(rand.Next(Image.Width), rand.Next(Image.Height));
                RandPoint[1] = new Point(rand.Next(Image.Width), rand.Next(Image.Height));
                ImageGraphics.DrawLine(ImagePen, RandPoint[0], RandPoint[1]);
            }

            ////画图片的前景噪音点
            //for (int i = 0; i < 100; i++)
            //{
            //    int x = rand.Next(Image.Width);
            //    int y = rand.Next(Image.Height);
            //    Image.SetPixel(x, y, Color.FromArgb(rand.Next()));
            //}
            //画图片的边框线
           // ImageGraphics.DrawRectangle(new Pen(Color.Silver), 0, 0, Image.Width - 1, Image.Height - 1);
            ImageGraphics.Dispose();
            //保存图片数据
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            Image.Save(stream, System.Drawing.Imaging.ImageFormat.Gif);
            Image.Dispose();
            stream.Dispose();
            CodeFont.Dispose();
            ImageBrush.Dispose();
            ImagePen.Dispose();
            //输出图片
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.CacheControl = "no-cache"; 
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "image/gif";
            HttpContext.Current.Response.BinaryWrite(stream.ToArray());
        }

        public static void WriteVerifyCode(int len, int size, StringMode stringMode, Color fontColor, Color bgColor)
        {
            string code = GenerateRandomString(len, stringMode);
            ShowVerifyCode(code, size, fontColor, bgColor);
            HttpContext.Current.Session[_key] = code;
        }
        /// <summary>
        /// false:验证码不正确,true:正确
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool Verify(string code, bool ignoreCase)
        {
            if (HttpContext.Current.Session[_key] == null)
            {
                return false;
            }
            else
            {
                var r = string.Compare(code, HttpContext.Current.Session[_key].ToString(), ignoreCase).Equals(0);
                HttpContext.Current.Session[_key] = null;
                return r;
            }
        }
    }
}
