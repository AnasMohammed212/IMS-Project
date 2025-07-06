using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS_DataAccess;
namespace IMS_Business
{
    public class clsUser
    {
        enum enMode { AddNew = 1, Update = 0 }
        enMode Mode= enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive {  get; set; }
        public clsUser(){ 
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;
            Mode = enMode.AddNew;
        }    
        private clsUser(int UserID,int PersonID,string UserName,string Password,bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            this.PersonInfo = clsPerson.Find(PersonID);
            Mode = enMode.Update;
        }
        public static clsUser FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName="", Password="";
            bool IsActive = true;   
           bool IsFound = clsUserData.GetUserInfoByUserID(UserID,ref PersonID,ref UserName,ref Password,ref IsActive);

            if (IsFound)
                return new clsUser(UserID,PersonID,UserName,Password,IsActive);
            else
                return null;
        }
        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = true;
            bool IsFound = clsUserData.GetUserInfoByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive);

            if (IsFound)
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }
        private async Task<bool>_AddNewUser()
        {
            this.UserID = await clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID!=-1);
        }
        private async Task<bool> _UpdateUser()
        {
            return await clsUserData.UpdateUser(this.UserID,this.PersonID,this.UserName, this.Password, this.IsActive);
        }
        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(await _AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return await _UpdateUser();

            }
            return false;
        }
        public static async Task<DataTable> GetAllUsers()
        {
            return await clsUserData.GetAllUsers();
        }
        public static async Task<bool> IsUserExist(int UserID)
        {
            return await clsUser.IsUserExist(UserID);
        }

        public static async Task<bool> DeleteUser(int UserID)
        {
            return await clsUserData.DeleteUser(UserID);
        }
    }
}
