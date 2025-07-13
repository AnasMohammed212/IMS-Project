using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS_DataAccess;

namespace IMS_Business
{
    public class clsCategory
    {
        enum enMode { AddNew = 1, Update = 0 }
        enMode Mode;

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public clsCategory()
        {
            this.CategoryID = -1;
            this.CategoryName = "";
            Mode = enMode.AddNew;
        }

        public clsCategory(int categoryID, string categoryName)
        {
            this.CategoryID = categoryID;
            this.CategoryName = categoryName;
            Mode = enMode.Update;
        }

        public static clsCategory Find(int categoryID)
        {
            string categoryName = "";
            bool isFound = clsCategoryData.GetCategoryInfoByID(categoryID, ref categoryName);
            if (isFound)
                return new clsCategory(categoryID, categoryName);
            else
                return null;
        }
        public static clsCategory Find(string categoryName)
        {
            int categoryID = -1;
            bool isFound = clsCategoryData.GetCategoryInfoByName(ref categoryID, categoryName);
            if (isFound)
                return new clsCategory(categoryID, categoryName);
            else
                return null;
        }
        private async Task<bool> _AddNewCategory()
        {
            this.CategoryID = await clsCategoryData.AddNewCategory(this.CategoryName);
            return (this.CategoryID != -1);
        }

        private async Task<bool> _UpdateCategory()
        {
            return await clsCategoryData.UpdateCategory(this.CategoryID, this.CategoryName);
        }

        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewCategory())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateCategory();
            }
            return false;
        }

        public static async Task<DataTable> GetAllCategories()
        {
            return await clsCategoryData.GetAllCategories();
        }

        public static async Task<bool> DeleteCategory(int categoryID)
        {
            return await clsCategoryData.DeleteCategory(categoryID);
        }

        public static async Task<bool> IsCategoryExist(int categoryID)
        {
            return await clsCategoryData.IsCategoryExist(categoryID);
        }
        public static async Task<bool> IsCategoryExist(string categoryName)
        {
            return await clsCategoryData.IsCategoryExist(categoryName);
        }

    }
}
