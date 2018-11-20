using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Board22
{
	/// <summary>
	/// 데이터베이스 연결정보
	/// </summary>
	public class DBConnection
	{
		/// <summary>
		/// Gwcommon 데이터베이스 연결문자열
		/// </summary>
        public static string Gwcommon { get { return ConfigurationManager.ConnectionStrings["Red"].ConnectionString; } }        
	}
}
