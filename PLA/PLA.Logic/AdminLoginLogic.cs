using PLA.Communications;
using PLA.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PLA.Logic
{
    public class AdminLoginLogic
    {
        DatabaseRepository<LoginAdmin> dbAdmin = new DatabaseRepository<LoginAdmin>();
  
        public static string ImageURL = ConfigurationManager.AppSettings["AdminImage"].ToString();

        public int AdminRegister(LoginAdminData objAdmin)
        {
            LoginAdmin admin = new LoginAdmin();
            try
            {
             
                admin.Name = objAdmin.Name;
                admin.EmailId = objAdmin.EmailId;
                admin.ContactNo = objAdmin.ContactNo;
                admin.Password = objAdmin.Password;
                admin.AdminImage = objAdmin.AdminImage;
                admin.CreatedDate = objAdmin.CreatedDate;
                admin.Status = objAdmin.Status;
                dbAdmin.Insert(admin);
                dbAdmin.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
            return admin.Id;
        }
        public List<LoginAdminData> GetAllAdminList()
        {
            List<LoginAdminData> result = new List<LoginAdminData>();
            try
            {
                result = (from info in dbAdmin.Table//.Include(x => x.Role)
                          select new LoginAdminData
                          {
                              Id = info.Id,                            
                              Name = info.Name,
                              EmailId = info.EmailId,
                              ContactNo = info.ContactNo,
                              Password = info.Password,
                              AdminImage =ImageURL+ info.AdminImage,
                              CreatedDate = info.CreatedDate,
                              Status = info.Status
                          }).ToList<LoginAdminData>();
              
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public LoginAdminData GetAdminDetail(int Id)
        {
            LoginAdminData result = new LoginAdminData();
            try
            {
                result = (from info in dbAdmin.Table
                          where info.Id == Id
                          select new LoginAdminData
                          {
                              Id = info.Id,
                            
                              Name = info.Name,
                              EmailId = info.EmailId,
                              ContactNo = info.ContactNo,
                              Password = info.Password,
                              AdminImage = ImageURL + info.AdminImage,
                              AdminPhoto = info.AdminImage,
                              CreatedDate = info.CreatedDate,
                              Status = info.Status
                          }).FirstOrDefault<LoginAdminData>();              
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void EditAdminRegister(LoginAdminData objAdmin)
        {
            LoginAdmin admin = new LoginAdmin();
            admin = (from info in dbAdmin.Table//.Include(x => x.Role)
                     where info.Id == objAdmin.Id
                     select info).FirstOrDefault();
            if(admin!=null)
            {
                try
                {
                    admin.Id = objAdmin.Id;                  
                    admin.Name = objAdmin.Name;
                    admin.EmailId = objAdmin.EmailId;
                    admin.ContactNo = objAdmin.ContactNo;
                    //admin.Password = objAdmin.Password;
                    admin.AdminImage = objAdmin.AdminImage;
                    //admin.CreatedDate = objAdmin.CreatedDate;
                    admin.Status = objAdmin.Status;
                    dbAdmin.Update(admin);
                    dbAdmin.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            
           
        }
        public void ChangeAdminPassword(LoginAdminData objAdmin)
        {
            LoginAdmin admin = new LoginAdmin();
            admin = (from info in dbAdmin.Table//.Include(x => x.Role)
                     where info.Id == objAdmin.Id
                     select info).FirstOrDefault();
            if (admin != null)
            {
                try
                {
                    admin.Id = objAdmin.Id;                   
                    admin.Password = objAdmin.Password;                  
                    dbAdmin.Update(admin);
                    dbAdmin.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }


        }
        public void DeleteAdmin(int Id)
        {
            LoginAdmin admin = new LoginAdmin();

            admin = (from info in dbAdmin.Table
                    where info.Id == Id
                    select info).FirstOrDefault();
            if (admin != null)
            {
                dbAdmin.Delete(admin);
                dbAdmin.SaveChanges();
            }
        }
        public LoginAdminData GetAdminDetailByEmail(string Email)
        {
            LoginAdminData result = new LoginAdminData();
            try
            {
                result = (from info in dbAdmin.Table
                          where info.EmailId == Email
                          select new LoginAdminData
                          {
                              Id = info.Id,                            
                              Name = info.Name,
                              EmailId = info.EmailId,
                              ContactNo = info.ContactNo,
                              Password = info.Password,
                              AdminImage = ImageURL + info.AdminImage,
                              AdminPhoto = info.AdminImage,
                              CreatedDate = info.CreatedDate,
                              Status = info.Status
                          }).FirstOrDefault<LoginAdminData>();
              
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void EditAdminStatus(int Id)
        {
            LoginAdmin result = new LoginAdmin();

            result = (from info in dbAdmin.Table
                      where info.Id == Id
                      select info).FirstOrDefault();
            try
            {
                if (result.Status == true)
                {
                    result.Status = false;
                }
                else
                {
                    result.Status = true;
                }
                dbAdmin.Update(result);
                dbAdmin.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
