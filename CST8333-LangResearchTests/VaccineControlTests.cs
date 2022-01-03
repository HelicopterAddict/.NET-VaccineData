//<Author = Anoopbir Singh Sidhu>
//<Sno = 040984994>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Control;

namespace CST8333_LangResearchTests
{
    /// <summary>
    /// Tests methods in the controller class VaccineControl
    /// </summary>
    [TestClass]
    public class VaccineControlTests
    {
        /// <summary>
        /// Tests <see cref="VaccineControl.ReadIntoMemory"/>
        /// </summary>
        [TestMethod]
        public void TestReadIntoMemory()
        {
            VaccineControl controller = new();
            controller.ReadIntoMemory();

            // Validates if LoadColNames removes unwanted column or not
            Assert.AreNotEqual(controller.ColNames.Length, 12);
            Assert.AreNotEqual(controller.VaxList.Count, 0);
        }

        /// <summary>
        /// Method that tests <see cref="VaccineControl.EditRecord(int, int, string)"/>
        /// </summary>
        //[TestMethod]
        //public void TestEditRecord()
        //{
        //    // Sample data to be added into the list
        //    string[] sampleData = { "1", "Canada", "Canada", "2020 - 12 - 19", "Not reported", "0", "0", "0", "0", "0", "0" };
        //    VaccineControl controller = new();
        //    controller.ReadIntoMemory(VaccineControl.SAVED_FILE_PATH);
        //    controller.AddNewRecord(sampleData);
        //    int sampleDataId = controller.GetListSize() - 1;
        //    string testValue = "Ontario";
        //    // Changes field of the new record
        //    controller.EditRecord(sampleDataId, 2, testValue);
        //    // Checks if the row updated with the new values
        //    Assert.AreEqual(testValue, controller.DataAsArray(sampleDataId)[2]);
        //}
        // Commented out because edit method changed in Assignment 3
    }
}
