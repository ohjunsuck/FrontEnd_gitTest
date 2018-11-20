using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Board22.CommonEditor
{
    public class CommonEditor
    {
        private string _contents = string.Empty;
        private string _formType = string.Empty;
        private string _userCode = string.Empty;

        public CommonEditor(string contens, string formtype, string usercode)
        {
            this._contents = contens;
            this._formType = formtype;
            this._userCode = usercode;
		}

		public void ConvertImageTagData(ref string ReturnValue, bool isThumb = true)
        {
            string Contents = this._contents;
            string SearchString = new Board22.CommonEditor.RegularText(Contents, Board22.CommonEditor.RegularType.TagExcept).ToString();
            string AttachFileUploadPath = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["UploadData"] + "Approval/");

            DateTime today = DateTime.Now;
            string year = today.Year.ToString();
            string month = (today.Month < 10) ? "0" + today.Month.ToString() : today.Month.ToString();
            string day = (today.Day < 10) ? "0" + today.Day.ToString() : today.Day.ToString();
            string second = (today.Second < 10) ? "0" + today.Second.ToString() : today.Second.ToString();
            string dateSeq = year + month + day + second + today.Millisecond.ToString();

            string DatePhysicalPath = year + "\\" + month + "\\" + day;
            string DateURLPath = year + "/" + month + "/" + day;
            string fileName = _userCode + "_" + dateSeq;

            //만약에 이미지 데이터를 넣을 경우 이미지 파일로 변환한다. -> xss 공격 코드로 인해서 이미지 깨지는 현상 방지
            if (System.Text.RegularExpressions.Regex.IsMatch(Contents, "<img [^<>]*>"))
            {
                System.Text.RegularExpressions.MatchCollection ImgMatch = System.Text.RegularExpressions.Regex.Matches(Contents, "<img [^<>]*>", System.Text.RegularExpressions.RegexOptions.Multiline);
                string ImgData = string.Empty;

                try
                {
                    for (int i = 0; i < ImgMatch.Count; i++)
                    {
                        ImgData = ImgMatch[i].Value;

                        var base64Data = System.Text.RegularExpressions.Regex.Match(ImgData, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value.Replace(">", "").Replace("<", "");
                        if (base64Data == null || base64Data == string.Empty) continue;

                        var widthData = System.Text.RegularExpressions.Regex.Match(base64Data, "width=\"([0-9]{1,4})\"").Value;
                        var heightData = System.Text.RegularExpressions.Regex.Match(base64Data, "height=\"([0-9]{1,4})\"").Value;
                        var altData = System.Text.RegularExpressions.Regex.Match(base64Data, "alt=\"([0-9]{1,4})\"").Value;

                        if (string.IsNullOrEmpty(widthData) == false)
                            base64Data = base64Data.Replace(widthData, "");
                        else
                            base64Data = base64Data.Replace("width=\"\"", "");

                        if (string.IsNullOrEmpty(heightData) == false)
                            base64Data = base64Data.Replace(heightData, "");
                        else
                            base64Data = base64Data.Replace("height=\"\"", "");

                        if (string.IsNullOrEmpty(altData) == false)
                            base64Data = base64Data.Replace(altData, "");
                        else
                            base64Data = base64Data.Replace("alt=\"\"", "");

                        base64Data = base64Data.Replace("\"", "");

                        var binData = Convert.FromBase64String(base64Data);

                        using (var stream = new System.IO.MemoryStream(binData))
                        {
                            //실제 파일로 쓴다. --> 썸네일 형식.
                            System.Drawing.Image OrgImage = System.Drawing.Image.FromStream(stream, false, false);


                            string AttachFilePath = AttachFileUploadPath + "\\" + this._formType + "\\" + DatePhysicalPath;

                            if (System.IO.Directory.Exists(string.Format("{0}\\thumb\\", AttachFilePath)) == false)
                            {
                                //디렉토리가 없으면 생성
                                System.IO.Directory.CreateDirectory(string.Format("{0}\\thumb\\", AttachFilePath));
                            }

                            //향상된 이미지의 썸네일 생성
                            Board22.CommonEditor.File AttachFile = new Board22.CommonEditor.File();
                            AttachFile.SetThumbnailImage(OrgImage, 920, string.Format("{0}\\thumb\\{1}.{2}", AttachFilePath, fileName + "_" + i.ToString(), "jpg"));
                            OrgImage.Save(string.Format("{0}\\{1}.{2}", AttachFilePath, fileName + "_" + i.ToString(), "jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                            OrgImage.Dispose();

                            //해당 부분에 BLOB을 URL로 변경한다.
							if (isThumb)
								Contents = Contents.Replace(ImgMatch[i].Value, "<a href=\"/Data/Approval/" + this._formType + "/" + DateURLPath + "/" + fileName + "_" + i.ToString() + ".jpg\" target=\"_blank\"><img src=\"/data/Approval/" + this._formType + "/" + DateURLPath + "/thumb/" + fileName + "_" + i.ToString() + ".jpg\" alt=\"editorimage\" " + widthData + " " + heightData + "  /></a>");
							else
								Contents = Contents.Replace(ImgMatch[i].Value, "<img src=\"/data/Approval/" + this._formType + "/" + DateURLPath + "/" + fileName + "_" + i.ToString() + ".jpg\"/></a>");
						}


                    }

                    ReturnValue = Contents;
                }
                catch 
                {
                    throw;
                }

            }
            else
            {
                ReturnValue = Contents;
            }
        }
        
        /// <summary>
        /// 태그를 제거한 텍스트를 넘겨준다.
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        private string GetText(string Value)
        {
            Board22.CommonEditor.RegularText Reqular = new Board22.CommonEditor.RegularText(Value, Board22.CommonEditor.RegularType.TagExcept);
            return Reqular.ToString();
        }

        /// <summary>
        /// XSS 공격 우회코드 추가
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        private string GetExceptXss(string Value)
        {
            Board22.CommonEditor.RegularText Reqular = new Board22.CommonEditor.RegularText(Value, Board22.CommonEditor.RegularType.Xss);
            return Reqular.ToString();
        }
    }


    public enum RegularType : byte
    {
        None = 0x00,
        TagExcept = 0x01, //태그 제거 스크립트
        Numeric = 0x02, //숫자만
        AlphabetNumeric = 0x03, //숫자 영문자만
        Email = 0x04,		//이메일
        Alphabet = 0x05,  //영문자만
        SqlInjection = 0x06,    //SQL Injection제거,
        Xss = 0x07,             //xss 공격
        End = 0xFF
    }


    #region 정규식
    public class RegularText
    {
        private string Value { get; set; }
        private RegularType Type { get; set; }

        public RegularText(string _value, RegularType _type)
        {
            this.Value = _value;
            this.Type = _type;
        }

        /// <summary>
        /// 정규식으로 표현
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                if (string.IsNullOrEmpty(this.Value) == true) return string.Empty;

                if (this.Type == RegularType.TagExcept) return System.Text.RegularExpressions.Regex.Replace(this.Value, @"<(.|\n)*?>", string.Empty);

                string[] arFilter = {"'", ";", "--", "select", "insert", "update", "delete", "drop", "union", "and", "or", "1=1", "sp_", "xp_", "@variable"
                                        , "@@variable", "exec", "sysobject", "into", "set", "values", "where", "order by", "group by"};


                if (this.Type == RegularType.SqlInjection)
                {
                    foreach (string szFilter in arFilter)
                    {
                        if (this.Value.IndexOf(szFilter, StringComparison.OrdinalIgnoreCase) >= 0) this.Value = this.Value.Replace(szFilter, "").Trim();
                    }
                    return this.Value;
                }

                if (this.Type == RegularType.Xss)
                {
                    string[] arXssFilter = { "javascript", "eval", "print", "perl", "script", "import", "export", "vbscript", "mocha", "livescript", "expression", "echo", "<?", "<%" };
                    foreach (string szFilter in arXssFilter)
                    {
                        if (this.Value.ToLower().IndexOf(szFilter, StringComparison.OrdinalIgnoreCase) >= 0) this.Value = this.Value.Replace(szFilter, "*").Trim();
                    }
                    return this.Value;
                }

                if (this.Type == RegularType.Numeric) return System.Text.RegularExpressions.Regex.Replace(this.Value, @"[^0-9]", string.Empty);

                if (this.Type == RegularType.AlphabetNumeric) return System.Text.RegularExpressions.Regex.Replace(this.Value, "[^0-9a-zA-Z]", string.Empty);

                if (this.Type == RegularType.Alphabet) return System.Text.RegularExpressions.Regex.Replace(this.Value, "[^a-zA-Z]", string.Empty);

                if (this.Type == RegularType.Email)
                {
                    string szEmailValue = System.Text.RegularExpressions.Regex.Replace(this.Value, "/^(w+(?:.w+)*)@((?:w+.)*w[w-]{0,66}).([a-z]{2,6}(?:.[a-z]{2})?)$/i", string.Empty);

                    if (string.IsNullOrEmpty(szEmailValue)) return string.Empty;

                    //@.이 존재하는지 본다..
                    if (this.Value.IndexOf('@') > -1
                        && this.Value.IndexOf('.') > -1)
                    {
                        string[] arEmail = this.Value.Split('@');
                        if (arEmail.Length < 2) return string.Empty;

                        this.Type = RegularType.AlphabetNumeric;
                        this.Value = arEmail[0];
                        string szReturnValue = this.ToString();

                        this.Type = RegularType.AlphabetNumeric;
                        this.Value = arEmail[1].Split('.')[0];
                        szReturnValue += "@" + this.ToString();

                        this.Type = RegularType.AlphabetNumeric;
                        this.Value = arEmail[1].Split('.')[1];
                        szReturnValue += "." + this.ToString();
                        return szReturnValue;
                    }
                    else
                    {
                        this.Type = RegularType.AlphabetNumeric;
                        return this.ToString();
                    }
                }
                return this.Value;
            }
            catch (System.ArgumentException)
            {
                return string.Empty;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

       
    }

    public class File
    {
        /// <summary>
        /// 파일명으로 이미지 파일인지 여부 체크
        /// </summary>
        /// <param name="_FileName"></param>
        /// <returns></returns>
        public bool IsImageFile(string _FileName)
        {
            string[] arAllowImageExt = { ".gif", ".jpg", ".png", ".bmp" };
            var isImageFile = from c in arAllowImageExt where _FileName.ToLower().LastIndexOf(c) > -1 select c;
            if (isImageFile == null || isImageFile.ToArray().Length <= 0) return false;

            //파일 헤더 분석.
            try
            {
                using (System.IO.FileStream myFileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open))
                {
                    byte[] byBuffer = new byte[4];
                    myFileStream.Read(byBuffer, 0, 4);
                    myFileStream.Close();
                    return IsImageByFileHeader(byBuffer, _FileName.Substring(_FileName.Length - 3, 3).ToLower());
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 파일을 읽어서 헤더값을 분석해서 이미지 인지 여부를 체크한다.(bmp, gif, jpg, bmp, png)
        /// </summary>
        /// <param name="byImageHeader"></param>
        /// <param name="_FileName"></param>/// 
        /// <returns></returns>
        public bool IsImageByFileHeader(byte[] byImageHeader, string _FileName)
        {
            //JPEG      ->       FF   D8
            //bmp      ->       42  4D
            //tif          ->       49  49  2A
            //gif         ->       47  49   46
            string szFileExt = _FileName.Substring(_FileName.Length - 3, 3).ToLower();
            bool IsImageFile = false;
            switch (szFileExt)
            {
                case "jpg":
                    if ((byImageHeader[0] == 0xFF) && (byImageHeader[1] == 0xD8))
                        IsImageFile = true;
                    else
                        IsImageFile = false;
                    break;
                case "gif":
                    if ((byImageHeader[0] == 0x47) && (byImageHeader[1] == 0x49) && (byImageHeader[2] == 0x46))
                        IsImageFile = true;
                    else
                        IsImageFile = false;
                    break;
                case "bmp":
                    if ((byImageHeader[0] == 0x42) && (byImageHeader[1] == 0x4D))
                        IsImageFile = true;
                    else
                        IsImageFile = false;
                    break;
                case "png":
                    if ((byImageHeader[0] == 0x89) && (byImageHeader[1] == 0x50) && (byImageHeader[2] == 0x4E) && (byImageHeader[3] == 0x47))
                        IsImageFile = true;
                    else
                        IsImageFile = false;
                    break;
                default:
                    IsImageFile = false;
                    break;
            }
            return IsImageFile;
        }

        /// <summary>
        /// 파일 업로드가 가능한지 여부 체크한다.
        /// </summary>
        /// <param name="_FileName"></param>
        /// <returns></returns>
        public bool IsAllowedUploadFile(string _FileName)
        {
            //파일 헤더 분석.
            try
            {
                using (System.IO.FileStream myFileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open))
                {
                    byte[] byBuffer = new byte[4];
                    myFileStream.Read(byBuffer, 0, 4);
                    myFileStream.Close();
                    return IsAllowdByFileHeader(byBuffer);
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 프로그램 파일은 서버에 쓸수없다.
        /// </summary>
        /// <param name="byHeader"></param>
        /// <returns></returns>
        public bool IsAllowdByFileHeader(byte[] byHeader)
        {
            //프로그램 파일은 쓸수 없다.
            bool IsAllowedFile = false;

            if ((byHeader[0] == 0x4D) && (byHeader[1] == 0x5A))
                IsAllowedFile = false;
            else
                IsAllowedFile = true;
            return IsAllowedFile;
        }

        /// <summary>
        /// 썸네일 이미지 생성
        /// </summary>
        /// <param name="Source">이미지 배열</param>
        /// <param name="WantImageWidth">원하는 가로 길이</param>
        /// <param name="DestFileName">저장파일이름</param>
        /// <returns>썸네일된 이미지 객체</returns>
        public void SetThumbnailImage(System.Drawing.Image Source, int WantImageWidth, string DestFileName)
        {
            int srcWidth = Source.Width;
            int srcHeight = Source.Height;
            int thumbHeight = 0;

            if (Source.Width > WantImageWidth)
            {
                thumbHeight = ((Source.Width - (Source.Width - WantImageWidth)) * Source.Height) / Source.Width;
            }
            else
            {
                WantImageWidth = Source.Width;
                thumbHeight = Source.Height;
            }
            using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(WantImageWidth, thumbHeight))
            {
                System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, WantImageWidth, thumbHeight);
                gr.DrawImage(Source, rectDestination, 0, 0, srcWidth, srcHeight, System.Drawing.GraphicsUnit.Pixel);

                bmp.Save(DestFileName);
                bmp.Dispose();
            }
        }


    }
    #endregion
}