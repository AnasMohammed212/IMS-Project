using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS_DataAccess;
namespace IMS_Business
{
    public class clsUnitOfMeasure
    {
        public int UnitID {  get; set; }
        public string UnitName { get; set; }
        public clsUnitOfMeasure()
        {
            this.UnitID = -1;
            this.UnitName = "";
        }
        public clsUnitOfMeasure(int UnitID,string UnitName)
        {
            this.UnitID = UnitID;
            this.UnitName = UnitName;
        }
        public static clsUnitOfMeasure Find(int UnitID)
        {
            string UnitName = "";
            bool IsFound = clsUnitOfMeasureData.GetUnitInfoByID(UnitID, ref UnitName);
            if (IsFound)
                return new clsUnitOfMeasure(UnitID, UnitName);
            else
            return null;
        }
        public static clsUnitOfMeasure Find(string UnitName)
        {
            int UnitID = -1;
            bool IsFound = clsUnitOfMeasureData.GetUnitInfoByName(ref UnitID,  UnitName);
            if (IsFound)
                return new clsUnitOfMeasure(UnitID, UnitName);
            else
                return null;
        }
        public static async Task<DataTable> GetAllUnitsOfMeasure()
        {
            return await clsUnitOfMeasureData.GetAllUnitsOfMeasure();
        }
        public static async Task<bool> IsUnitExist(string unitName)
        {
            return await clsUnitOfMeasureData.IsUnitExist(unitName);
        }

    }
}
