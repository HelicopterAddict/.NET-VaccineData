//<Author = Anoopbir Singh Sidhu>
//<Sno = 040984994>

using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Newtonsoft.Json.Linq;

namespace Control
{
    /// <summary>
    /// The control/business layer of the project
    /// </summary>
    public class VaccineControl
    {
        /// <summary>
        /// List containing records obtained from the file/persisted data
        /// </summary>
        public List<VaccineData> VaxList { get; set; }

        /// <summary>
        /// List containing column names obtained from the file/persisted data
        /// </summary>
        public string[] ColNames { get; private set; }

        /// <summary>
        /// The object through which all CRUD is done
        /// </summary>
        private DatabaseModel db = new("VaccineBeta");

        /// <summary>
        /// Puts persisted data into the VaxList list <see cref="VaxList"/>
        /// </summary>
        public void ReadIntoMemory()
        {
            VaxList = db.LoadRecords<VaccineData>();
            LoadColNames();
        }

        /// <summary>
        /// Adds a new record into VaxList <see cref="VaxList"/>
        /// </summary>
        /// <param name="userRecord"></param>
        public void AddNewRecord(string[] userRecord)
        {
            VaccineData newRec = new(userRecord);
            VaxList.Add(newRec);
            db.InsertRecord<VaccineData>(newRec);
        }

        /// <summary>
        /// Removes a record from VaxList <see cref="VaxList"/>
        /// </summary>
        /// <param name="id">Index of the element to be removed</param>
        public void DeleteRecord(int id)
        {
            db.DeleteRecord<VaccineData>(VaxList[id].Id);
            VaxList.Remove(VaxList[id]);
        }

        /// <summary>
        /// Returns size of VaxList <see cref="VaxList"/>
        /// </summary>
        /// <returns>Size of the list</returns>
        public int GetListSize()
        {
            return VaxList.Count;
        }

        /// <summary>
        /// Edits a record in VaxList <see cref="VaxList"/>
        /// </summary>
        /// <param name="id">Index of the element to be edited</param>
        /// <param name="field">Position of the element in the row</param>
        /// <param name="value">The new value</param>
        public void EditRecord(int id, int field, string value)
        {
            switch (field)
            {
                case 0:
                    VaxList[id].Pruid = value;
                    break;
                case 1:
                    VaxList[id].PreName = value;
                    break;
                case 2:
                    VaxList[id].PrfName = value;
                    break;
                case 3:
                    VaxList[id].WeekEnd = value;
                    break;
                case 4:
                    VaxList[id].ProductName = value;
                    break;
                case 5:
                    VaxList[id].NumTotalAtLeast1Dose = value;
                    break;
                case 6:
                    VaxList[id].NumTotalPartially = value;
                    break;
                case 7:
                    VaxList[id].NumTotalFully = value;
                    break;
                case 8:
                    VaxList[id].PropAtLeast1Dose = value;
                    break;
                case 9:
                    VaxList[id].PropPartially = value;
                    break;
                case 10:
                    VaxList[id].PropFully = value;
                    break;
                default:
                    break;
            }

            db.UpdateRecord(VaxList[id].Id, VaxList[id]);
        }

        /// <summary>
        /// Returns data from VaxList <see cref="VaxList"/> in the form of a string array for display and editing purposes
        /// </summary>
        /// <param name="id">Index of the element</param>
        /// <returns>A string array of the row elements</returns>
        public string[] DataAsArray(int id) => StringMutator(DataAsCsvString(id));

        /// <summary>
        /// Used only for initializing database collection by taking objects from the file into a list,
        /// and pushing the list elements into the collection
        /// </summary>
        public void WriteAllToDb()
        {
            foreach(VaccineData record in VaxList)
            {
                db.InsertRecord(record);
            }
        }

        /// <summary>
        /// Sorts the list by the number of people with at least 1 dose
        /// </summary>
        public void SortByAtLeast1Dose()
        {
            VaxList = VaxList.OrderBy(o => int.Parse(o.NumTotalAtLeast1Dose)).ToList();
        }

        /// <summary>
        /// Sorts the list by the number of partially vaccinated people
        /// </summary>
        public void SortByPartialDose()
        {
            VaxList = VaxList.OrderBy(o => int.Parse(o.NumTotalPartially)).ToList();
        }

        /// <summary>
        /// Sorts the list by the number of fully vaccinated people
        /// </summary>
        public void SortByFullDose()
        {
            VaxList = VaxList.OrderBy(o => int.Parse(o.NumTotalFully)).ToList();
        }

        /// <summary>
        /// Returns data from VaxList <see cref="VaxList"/> in the form of a comma separated string for writing purposes
        /// </summary>
        /// <param name="id">Index of the element</param>
        /// <returns>A string formatted as a CSV row</returns>
        private string DataAsCsvString(int id)
        {
            VaccineData vax = VaxList[id];
            return $"{vax.Pruid},{vax.PreName},{vax.PrfName}," +
                   $"{vax.WeekEnd},{vax.ProductName}," +
                   $"{vax.NumTotalAtLeast1Dose},{vax.NumTotalPartially}," +
                   $"{vax.NumTotalFully},{vax.PropAtLeast1Dose}," +
                   $"{vax.PropPartially},{vax.PropFully}";
        }

        /// <summary>
        /// Splits a csv row into a string array, and then truncates it
        /// </summary>
        /// <param name="input">The string to split and truncate</param>
        /// <returns>Mutated string which can be used to read into the VaccineData constructor</returns>
        private string[] StringMutator(string input)
        {
            string[] columns = input.Split(',');
            Array.Resize(ref columns, 11);
            return columns;
        }

        private void LoadColNames()
        {
            ColNames = JObject.Parse("{" + string.Join(",", db.GetJsonById<VaccineData>(VaxList[0].Id).Split(',').Skip(1).ToArray())).Properties().Select(prop => prop.Name).ToArray();
        }
    }
}
