using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLA.Data;
using PLA.Communications;

namespace PLA.Logic
{
    public class RegistrationLogic
    {
        DatabaseRepository<Registration> dbRegistration = new DatabaseRepository<Registration>();

        public int AddRegistaration(RegistrationData model)
        {
            try
            {
                Registration obj = new Registration();
                obj.FirstName = model.FirstName;
                obj.LastName = model.LastName;
                obj.UserName = model.UserName;
                obj.Email = model.Email;
                obj.Password = model.Password;
                obj.Status = model.Status;
                obj.Date = model.Date;
                dbRegistration.Insert(obj);
                dbRegistration.SaveChanges();

                return obj.Id;
            }
            catch (Exception)
            {
                
                throw;
            }            
        }
        public void EditRegistaration(RegistrationData model)
        {
            try
            {
                Registration obj = (from a in dbRegistration.Table
                                    where a.Id == model.Id
                                    select a).FirstOrDefault<Registration>();

                obj.FirstName = model.FirstName;
                obj.LastName = model.LastName;
                obj.UserName = model.UserName;
                obj.Email = model.Email;
                obj.Password = model.Password;
                

                dbRegistration.Update(obj);
                dbRegistration.SaveChanges();
            
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<RegistrationData> GetAllRegistartion()
        {
            List<RegistrationData> result = (from a in dbRegistration.Table
                                             select new RegistrationData
                                             {
                                                 Id = a.Id,
                                                 UserName = a.UserName,
                                                 FirstName = a.FirstName,
                                                 LastName = a.LastName,
                                                 Email = a.Email,
                                                 Password = a.Password,
                                                 Status = a.Status,
                                                 Date = a.Date
                                             }).ToList<RegistrationData>();
            return result;
        }
        public RegistrationData GetDetailRegistartionById(int Id)
        {
            RegistrationData result = (from a in dbRegistration.Table
                                       where a.Id == Id
                                       select new RegistrationData
                                       {
                                           Id = a.Id,
                                           UserName = a.UserName,
                                           FirstName = a.FirstName,
                                           LastName = a.LastName,
                                           Email = a.Email,
                                           Password = a.Password,
                                           Status = a.Status,
                                           Date = a.Date
                                       }).FirstOrDefault<RegistrationData>();
            return result;
        }
        public RegistrationData GetDetailRegistartionByUsernameorPasword(string UserName, string Password)
        {
            RegistrationData result = (from a in dbRegistration.Table
                                       where a.UserName == UserName && a.Password == Password
                                       select new RegistrationData
                                       {
                                           Id = a.Id,
                                           UserName = a.UserName,
                                           FirstName = a.FirstName,
                                           LastName = a.LastName,
                                           Email = a.Email,
                                           Password = a.Password,
                                           Status = a.Status,
                                           Date = a.Date
                                       }).FirstOrDefault<RegistrationData>();
            return result;
        }
        public void ChangeStatus(RegistrationData model)
        {
            try
            {
                Registration obj = (from a in dbRegistration.Table
                                    where a.Id == model.Id
                                    select a).FirstOrDefault<Registration>();

                if (obj.Status == true)
                {
                    obj.Status = false;
                }
                else
                {
                    obj.Status = true;
                }


                dbRegistration.Update(obj);
                dbRegistration.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
