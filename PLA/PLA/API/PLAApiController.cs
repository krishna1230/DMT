using PLA.Data;
using PLA.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using PLA.MyServiceReference1;

namespace PLA.API
{
    public class PLAApiController : ApiController
    {
        #region LoginAdmin
        AdminLoginLogic loginadminlogic = new AdminLoginLogic();
        public int AdminRegister(LoginAdminData objAdmin)
        {
            return loginadminlogic.AdminRegister(objAdmin);
        }
        public List<LoginAdminData> GetAllAdminList()
        {
            return loginadminlogic.GetAllAdminList();
        }
        public LoginAdminData GetAdminDetail(int Id)
        {
            return loginadminlogic.GetAdminDetail(Id);

        }
        public LoginAdminData GetAdminDetailByEmail(string Email)
        {
            return loginadminlogic.GetAdminDetailByEmail(Email);
        }
        public void EditAdminRegister(LoginAdminData objAdmin)
        {
            loginadminlogic.EditAdminRegister(objAdmin);
        }
        public void ChangeAdminPassword(LoginAdminData objAdmin)
        {
            loginadminlogic.ChangeAdminPassword(objAdmin);

        }
        public void DeleteAdmin(int Id)
        {
            loginadminlogic.DeleteAdmin(Id);

        }
        public void EditAdminStatus(int Id)
        {
            loginadminlogic.EditAdminStatus(Id);
        }
        #endregion
        #region Encrypt/Decrypt/Email
        public string encrypt(string message)
        {
            const string passphrase = "Password@123";
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            //to create the object for UTF8Encoding  class
            //TO create the object for MD5CryptoServiceProvider 
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] deskey = md5.ComputeHash(utf8.GetBytes(passphrase));
            //to convert to binary passkey
            //TO create the object for  TripleDESCryptoServiceProvider 
            TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
            desalg.Key = deskey;//to  pass encode key
            desalg.Mode = CipherMode.ECB;
            desalg.Padding = PaddingMode.PKCS7;
            byte[] encrypt_data = utf8.GetBytes(message);
            //to convert the string to utf encoding binary 

            try
            {


                //To transform the utf binary code to md5 encrypt    
                ICryptoTransform encryptor = desalg.CreateEncryptor();
                results = encryptor.TransformFinalBlock(encrypt_data, 0, encrypt_data.Length);

            }
            finally
            {
                //to clear the allocated memory
                desalg.Clear();
                md5.Clear();
            }
            //to convert to 64 bit string from converted md5 algorithm binary code
            return Convert.ToBase64String(results);

        }
        public string decrypt(string message)
        {
            const string passphrase = "Password@123";
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] deskey = md5.ComputeHash(utf8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
            desalg.Key = deskey;
            desalg.Mode = CipherMode.ECB;
            desalg.Padding = PaddingMode.PKCS7;
            byte[] decrypt_data = Convert.FromBase64String(message);
            try
            {
                //To transform the utf binary code to md5 decrypt
                ICryptoTransform decryptor = desalg.CreateDecryptor();
                results = decryptor.TransformFinalBlock(decrypt_data, 0, decrypt_data.Length);
            }
            finally
            {
                desalg.Clear();
                md5.Clear();

            }
            //TO convert decrypted binery code to string
            return utf8.GetString(results);
        }
        public void SendEmail(string subject, string message, string to)
        {
            var FromEmailid = "noreplywhm@gmail.com";
            var Pass = "noreply123#";
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(FromEmailid);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(to));
            SmtpClient smpt = new SmtpClient();
            smpt.Host = "smtp.gmail.com";
            smpt.EnableSsl = true;
            NetworkCredential Networkcred = new NetworkCredential();
            Networkcred.UserName = mailMessage.From.Address;
            Networkcred.Password = Pass;
            smpt.UseDefaultCredentials = false;
            smpt.Credentials = Networkcred;
            smpt.Port = 587;

            smpt.Send(mailMessage);



        }
     
        #endregion
        #region Registration
        RegistrationLogic objReg = new RegistrationLogic();
          public int AddRegistaration(RegistrationData model)
        {
            return objReg.AddRegistaration(model);
        }
        public void EditRegistaration(RegistrationData model)
        {
             objReg.EditRegistaration(model);
        }
        public List<RegistrationData> GetAllRegistartion()
        {
            return objReg.GetAllRegistartion();
        }
        public RegistrationData GetDetailRegistartionById(int Id)
        {
            return objReg.GetDetailRegistartionById(Id);
        }
        public void ChangeStatus(RegistrationData model)
        {
            objReg.ChangeStatus(model);
        }
        public RegistrationData GetDetailRegistartionByUsernameorPasword(string Email, string Password)
        {
            return objReg.GetDetailRegistartionByUsernameorPasword(Email,Password);
        }
        #endregion

        MyServiceClient proxy = new MyServiceClient();

        public string GetName(string name)
        {
            return  proxy.GetName(name);
        }
    }
}