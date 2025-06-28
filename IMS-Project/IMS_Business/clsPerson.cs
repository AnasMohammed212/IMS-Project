using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS_DataAccess;
namespace IMS_Business
{
    public class clsPerson
    {
        enum enMode { AddNew=0,Update=1}
        enMode Mode = enMode.AddNew;
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }
        public DateTime DateOfBirth { get; set; }
        public short Gender {  get; set; }
        public string Address {  get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        //public clsCountry CountryInfo { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool isActive {  get; set; }
        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.Gender = -1;
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";
            this.CreatedAt = DateTime.Now;
            this.isActive = true;
            Mode = enMode.AddNew;
        }
        private clsPerson(int PersonID,string FirstName,string SecondName,string ThirdName,
            string LastName,short Gender,DateTime DateOfBirth,string Address,string Phone,
            string Email,int NationalityCountryID,string ImagePath,DateTime CreatedAt,
            bool IsActive)
        {
            this.PersonID=PersonID;
            this.FirstName=FirstName;
            this.SecondName=SecondName;
            this.ThirdName=ThirdName;
            this.LastName=LastName;
            this.Gender = Gender;
            this.DateOfBirth=DateOfBirth;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.CreatedAt = CreatedAt;
            //this.CountryInfo = clsCountry.Find(NationalityCountryID);
            Mode= enMode.Update;
        }
        public static clsPerson Find(int PersonID)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            short Gender = 0;
            DateTime DateOfBirth = DateTime.Now,CreatedAt=DateTime.Now;
            int NationalityCountryID = -1;
            bool IsActive = true;
            bool IsFound = clsPersonData.GetPersonInfoByID(PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName,ref DateOfBirth,ref Gender,ref Address ,ref Phone,ref Email,ref NationalityCountryID,ref ImagePath,ref CreatedAt,ref IsActive);
            if(IsFound)
                return new clsPerson(PersonID,FirstName,SecondName,ThirdName,LastName,Gender,DateOfBirth,Address,Phone,Email,NationalityCountryID,ImagePath,CreatedAt,IsActive);
            else return null;
        }
        private async Task<bool> _AddNewPerson()
        {
            this.PersonID= await clsPersonData.AddNewPerson(this.FirstName,this.SecondName,this.ThirdName,this.LastName,this.DateOfBirth,this.Gender,this.Address,this.Phone,this.Email,this.NationalityCountryID,this.ImagePath,this.CreatedAt,this.isActive);
            return (this.PersonID != -1);
        }
        private async Task<bool> _UpdatePerson()
        {
            return await clsPersonData.UpdatePerson(this.PersonID,this.FirstName,this.SecondName,this.ThirdName,this.LastName,this.DateOfBirth,this.Gender,this.Address,this.Phone,this.Email,this.NationalityCountryID,this.ImagePath,this.CreatedAt,this.isActive);
        }
        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:                 
                        return await _UpdatePerson();
            }
              return false;
        }
        public static async Task<DataTable> GetAllPeople()
        {
            return await clsPersonData.GetAllPeople();
        }
        public static async Task<bool> isPersonExist(int ID)
        {
            return  await clsPersonData.IsPersonExist(ID);
        }
        public static async Task<bool> DeletePerson(int ID)
        {
            return await clsPersonData.DeletePerson(ID);
        }
    }
}
