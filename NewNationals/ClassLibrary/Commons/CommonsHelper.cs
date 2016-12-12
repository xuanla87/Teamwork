﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClassLibrary.Commons
{
    public class CommonsHelper
    {
        public static string SessionAdminCp = "SessionAdminCPLogin";

        /// <summary>
        /// Trọn trạng thái sử dụng
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> Dropdown_Status()
        {
            var getStatus = new List<SelectListItem>
            {
                new SelectListItem { Text = "Bật", Value = "1" },
                new SelectListItem { Text = "Tắt", Value = "0"}
            };
            return getStatus;
        }
        /// <summary>
        /// Chọn giới tính
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> Dropdown_Gender()
        {
            var getStatus = new List<SelectListItem>
            {
                new SelectListItem { Text = "Nam", Value = "True" },
                new SelectListItem { Text = "Nữ", Value = "False"}
            };
            return getStatus;
        }

        /// <summary>
        /// Chọn kiểu Menu
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> TypeCategory()
        {
            var getStatus = new List<SelectListItem>
            {
                new SelectListItem { Text = "Menu Trên", Value = "1" },
                new SelectListItem { Text = "Menu Dưới", Value = "2" },
                new SelectListItem { Text = "Menu Trong", Value = "3"}
            };
            return getStatus;
        }

        /// <summary>
        /// Chọn vị trí menu
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> PositionType()
        {
            var getStatus = new List<SelectListItem>
            {
                new SelectListItem { Text = "Top", Value = "1" },
                new SelectListItem { Text = "Bottom", Value = "2" },
                new SelectListItem { Text = "Inside", Value = "3"}
            };
            return getStatus;
        }

        /// <summary>
        /// Hàm rút ngắn chuỗi ký tự
        /// </summary>
        /// <param name="value">chuỗi truyền vào</param>
        /// <param name="count">Số lượng ký tự cần lấy</param>
        /// <returns></returns>
        public static string RutGon(string value, int count)
        {
            string values = value;
            if (values.Length >= count)
            {
                string valueCut = values.Substring(0, count - 3);
                string[] valuearray = valueCut.Split(' ');
                string valuereturn = "";
                for (int i = 0; i < valuearray.Length - 1; i++)
                {
                    valuereturn = valuereturn + " " + valuearray[i];
                }
                return valuereturn + "...";
            }
            else
            {
                return values;
            }
        }

        /// <summary>
        /// Hàm chuyển từ tiếng việt có dấu thành không dấu
        /// </summary>
        /// <param name="ipStrChange"></param>
        /// <returns></returns>
        public static string FilterCharCommas(string ipStrChange)
        {
            if (string.IsNullOrEmpty(ipStrChange))
                return "";
            Regex vRegRegex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string vStrFormD = ipStrChange.Normalize(System.Text.NormalizationForm.FormD);
            string rt = vRegRegex.Replace(vStrFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').Replace(" ", "-").Replace("/", "-");
            rt = rt.Replace("\\", "-").Replace("'", "-").Replace(":", "-").Replace("&", "-").Replace(".", "").Replace(":", "-").Replace("%", "phan-tram").Replace("?", "-").Replace("\"", "-");
            return rt;
        }

        /// <summary>
        /// hàm tạo chuỗi mã hóa
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncrytPassword(string input)
        {
            var md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(input));
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x5"));
            }
            return strBuilder.ToString().ToUpper();
        }
        /// <summary>
        /// Lấy thông tin thông tin địa chỉ IP của user đăng nhập
        /// </summary>
        public static string GetIpAddress
        {
            get
            {
                HttpRequest currentRequest = HttpContext.Current.Request;
                string ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (ipAddress == null || ipAddress.ToLower() == "unknown")
                    ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"];

                return ipAddress;
            }
        }

        /// <summary>
        /// Lấy thông tin trình duyệt User sử dụng
        /// </summary>
        public static string GetPlatform
        {
            get
            {
                return HttpContext.Current.Request.Browser.Platform;
            }
        }
    }
}
