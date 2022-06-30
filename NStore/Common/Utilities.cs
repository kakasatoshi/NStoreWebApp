﻿using NStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace NStore.Common
{
    public class Utilities
    {
        public static string ServiceURL { get; set; }

        public static bool SendMail(string subject, string message, string email)
        {
            try
            {
                var mailMsg = new MailMessage();

                mailMsg.To.Add(new MailAddress(email, ""));
                mailMsg.From = new MailAddress("info@csccinema.vn", "CSC Cinema");

                mailMsg.Subject = subject;
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message, null,
                                                                           MediaTypeNames.Text.Plain));
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message, null,
                                                                            MediaTypeNames.Text.Html));

                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                var credentials = new System.Net.NetworkCredential("info@csccinema.vn", "123456");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = true;

                smtpClient.Send(mailMsg);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public static object SendDataRequest(string ApiUrl, object input = null)
        {
            HttpClient client = new();
            client.BaseAddress = new Uri("https://localhost:44322/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync(ApiUrl, input).Result;
            object kq = null;
            if (response.IsSuccessStatusCode)
            {
                kq = response.Content.ReadFromJsonAsync<object>().Result;
            }
            return kq;
        }

        public static T SendDataRequest<T>(string ApiUrl, object input = null)
        {
            //var thanhvien = HttpContext.Session.Get<ThanhVienModel.Output.ThongTinThanhVien>("ThanhVien");
            var thanhvien = AppContext.Current.Session.Get<MemberModel.Output.MemberInfo>("ThanhVien");
            var nhanvien = AppContext.Current.Session.Get<UserModel.Output.UserInfo>("NhanVien");
            HttpClient client = new();
            client.BaseAddress = new Uri("https://localhost:5001");
            client.DefaultRequestHeaders.Accept.Clear();
            if (thanhvien != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", thanhvien.AccessToken);
            else if (nhanvien != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", nhanvien.AccessToken);
            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync(ApiUrl, input).Result;
            T result = default(T);
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadFromJsonAsync<T>().Result;
            }
            return result;
        }

        public class PropertyCopier<TParent, TChild> where TParent : class
                                            where TChild : class
        {
            public static void Copy(TParent parent, TChild child)
            {
                var parentProperties = parent.GetType().GetProperties();
                var childProperties = child.GetType().GetProperties();

                foreach (var parentProperty in parentProperties)
                {
                    if (parentProperty.Name.ToLower() == "id") continue;
                    foreach (var childProperty in childProperties)
                    {
                        if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType && childProperty.SetMethod != null)
                        {
                            if (parentProperty.GetValue(parent) != null)
                                childProperty.SetValue(child, parentProperty.GetValue(parent));
                            break;
                        }
                    }
                }
            }
        }
    }
}