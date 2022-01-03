//<Author = Anoopbir Singh Sidhu>
//<Sno = 040984994>

// Demonstrating use of an API library
using ConsoleTables;
using System;
using System.Linq;
using Control;

namespace View
{
    /// <summary>
    /// The view/presentation layer of the project
    /// </summary>
    public class VaccineView
    {
        // Menu options
        const string DISPLAY_TEN_RECORDS_OPT = "1";
        const string DISPLAY_ALL_RECORDS_OPT = "2";
        const string ENTER_NEW_RECORD_OPT = "3";
        const string DELETE_RECORD_OPT = "4";
        const string EDIT_RECORD_OPT = "5";
        //const string SAVE_OPT = "6";
        //const string RESTORE_OPT = "7";
        const string QUIT_OPT = "Q";

        /// <summary>
        /// Controller object
        /// </summary>
        private VaccineControl controller;

        /// <summary>
        /// Part of the ConsoleTables library, https://github.com/khalidabuhakmeh/ConsoleTables
        /// Takes string arrays and outputs formatted tables
        /// </summary>
        private ConsoleTable table;

        /// <summary>
        /// The main interface through which the user interacts with the program
        /// </summary>
        public void Menu()
        {
            controller = new();
            controller.ReadIntoMemory();

            // Making the table for the form
            ConsoleTable menuTable = new("#", "OPTION");
            menuTable.AddRow(DISPLAY_TEN_RECORDS_OPT, "Display 10 records");
            menuTable.AddRow(DISPLAY_ALL_RECORDS_OPT, "Display all records");
            menuTable.AddRow(ENTER_NEW_RECORD_OPT, "Enter a new record");
            menuTable.AddRow(DELETE_RECORD_OPT, "Delete a record");
            menuTable.AddRow(EDIT_RECORD_OPT, "Edit a record");
            //menuTable.AddRow(SAVE_OPT, "Save changes to file");
            //menuTable.AddRow(RESTORE_OPT, "Restore file (revert all changes)");
            menuTable.AddRow(QUIT_OPT, "To exit");

            Console.WriteLine("CST8333-350 Language Research Project");
            string choice = "";
            do
            {
                try
                {
                    Console.WriteLine("\nAnoopbir Singh Sidhu - 040984994\n");
                    menuTable.Write(Format.MarkDown);
                    Console.Write("Enter your choice : ");
                    choice = Console.ReadLine();
                    switch (choice)
                    {
                        // Display 10 records per request
                        case DISPLAY_TEN_RECORDS_OPT:
                            string input;
                            string[] options = {"+", "1", "2", "3"};
                            int index = 0;
                            do
                            {
                                Console.WriteLine("\nPress '+' to print lines" +
                                                  "\nPress 1 to sort by the number of people with at least one dose" +
                                                  "\nPress 2 to sort by the number of partially vaccinated people" +
                                                  "\nPress 3 to sort by the number of fully vaccinated people" +
                                                  "\nPress any other button to return to menu");
                                input = Console.ReadLine();
                                switch(input)
                                {
                                    case "+":
                                        InitTableAddColumns();
                                        for (int i = 0; i < 10 && index < controller.GetListSize(); i++, index++)
                                            AddRowsToTable(index);
                                        table.Write(Format.Alternative);
                                        if (index == controller.GetListSize())
                                        {
                                            Console.WriteLine("End of records");
                                            input = "-1";
                                        }
                                        break;

                                    case "1":
                                        index = 0;
                                        controller.SortByAtLeast1Dose();
                                        Console.WriteLine("\nSuccessfully sorted by the number of people with at least one dose\n");
                                        break;

                                    case "2":
                                        index = 0;
                                        controller.SortByPartialDose();
                                        Console.WriteLine("\nSuccessfully sorted by the number of partially vaccinated people\n");
                                        break;

                                    case "3":
                                        index = 0;
                                        controller.SortByFullDose();
                                        Console.WriteLine("\nSuccessfully sorted by the number of fully vaccinated people\n");
                                        break;

                                    default:
                                        break;
                                }
                            }
                            while (Array.Exists(options, x => x == input));
                            break;

                        // Display all records
                        case DISPLAY_ALL_RECORDS_OPT:
                            string[] opt = {"A", "D", "1", "2", "3" };
                            string userChoice;
                            int order = 0;
                            do
                            {
                                InitTableAddColumns();
                                AddAllRowsToTable(order);
                                table.Write(Format.Alternative);
                                Console.WriteLine("Press 'A' for ascending and 'D' for descending" +
                                                  "\nPress 1 to sort by the number of people with at least one dose" +
                                                  "\nPress 2 to sort by the number of partially vaccinated people" +
                                                  "\nPress 3 to sort by the number of fully vaccinated people" +
                                                  "\nPress any other button to return to menu");
                                userChoice = Console.ReadLine();
                                switch(userChoice)
                                {
                                    case "A":
                                        order = 0;
                                        break;

                                    case "D":
                                        order = 1;
                                        break;

                                    case "1":
                                        controller.SortByAtLeast1Dose();
                                        Console.WriteLine("\nSuccessfully sorted by the number of people with at least one dose\n");
                                        break;

                                    case "2":
                                        controller.SortByPartialDose();
                                        Console.WriteLine("\nSuccessfully sorted by the number of partially vaccinated people\n");
                                        break;

                                    case "3":
                                        controller.SortByFullDose();
                                        Console.WriteLine("\nSuccessfully sorted by the number of fully vaccinated people\n");
                                        break;

                                    default:
                                        break;
                                }
                            }
                            while (Array.Exists(opt, x => x == userChoice));
                            break;

                        // Enter a new record
                        case ENTER_NEW_RECORD_OPT:
                            controller.AddNewRecord(EnterNewRecord());
                            break;

                        // Delete a record
                        case DELETE_RECORD_OPT:
                            Console.Write("Enter ID of entry to delete : ");
                            int deleteId = Convert.ToInt32(Console.ReadLine());
                            PrintRecordDetails(deleteId);
                            Console.WriteLine("Do you want to delete this record from the dataset? (Y for yes, press any other button to cancel)");

                            if (Console.ReadLine().Equals("Y"))
                                controller.DeleteRecord(deleteId);
                            else
                                Console.WriteLine("Operation Cancelled");
                            break;

                        // Edit a record
                        case EDIT_RECORD_OPT:
                            Console.WriteLine("Enter ID of entry to edit");
                            int editId = Convert.ToInt32(Console.ReadLine());
                            PrintRecordDetails(editId);
                            Console.WriteLine("Enter # of field to edit (0 - 10)");
                            int fieldId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter new Value : ");
                            string newValue = Console.ReadLine();
                            while (!Validator(fieldId, newValue))
                            {
                                Console.Write($"Invalid input, please try again\n\nEnter new Value : ");
                                newValue = Console.ReadLine();
                            }
                            controller.EditRecord(editId, fieldId, newValue);
                            PrintRecordDetails(editId);
                            break;

                        //// Save changes made to the list by saving them to Data.csv
                        //case SAVE_OPT:
                        //    controller.SaveToFile();
                        //    Console.WriteLine("Your changes have been saved");
                        //    break;

                        //// Overwrites Data.csv with the first 100 records from vaccination-coverage-byVaccineType.csv
                        //case RESTORE_OPT:
                        //    controller.ReadIntoMemory(VaccineControl.ORIGINAL_FILE_PATH);
                        //    controller.SaveToFile();
                        //    Console.WriteLine("File restored");
                        //    break;

                        // Exits the program
                        case QUIT_OPT:
                            Console.WriteLine("Exiting...");
                            break;

                        // When a non-existent option is chosen, prompts user to try again
                        default:
                            Console.WriteLine("Wrong input, try again (Q to exit)");
                            break;
                    }
                }
                // Encountered when Eg. User enters characters when ID is asked
                catch(FormatException)
                {
                    Console.WriteLine("Wrong input");
                }
                // Encountered when the user enters an ID that doesn't exist
                catch(ArgumentOutOfRangeException)
                {
                    Console.WriteLine("No record with that ID exists");
                }
            } while (choice is not "Q");
        }

        /// <summary>
        /// Initializes the ConsoleTable with columns taken from the data provided
        /// </summary>
        public void InitTableAddColumns()
        {
            table = new(GetColumnNamesWithId());
            string[] blankLine = new string[12];

            for (int i = 0; i < 12; i++)
                blankLine[i] = "";
            table.AddRow(blankLine);
        }

        /// <summary>
        /// Loops through the whole list and puts it into the ConsoleTable
        /// </summary>
        public void AddAllRowsToTable(int order)
        {
            if(order is 0)
            {
                for (int i = 0; i < controller.GetListSize(); i++)
                    AddRowsToTable(i);
            }
            else if(order is 1)
            {
                for (int i = controller.GetListSize() - 1; i > 0; i--)
                    AddRowsToTable(i);
            }
        }

        /// <summary>
        /// Adds an element from the list as a row to the ConsoleTable
        /// </summary>
        /// <param name="index">The index of the element from the list to be added</param>
        public void AddRowsToTable(int index)
        {
            table.AddRow(GetRowsWithId(index));
        }

        /// <summary>
        /// Takes values from the user to create a new record
        /// </summary>
        /// <returns>
        /// A string array which is passed to VaccineControl's AddNewRecord() method
        /// <see cref="VaccineControl.AddNewRecord(string[])"/>
        /// </returns>
        public string[] EnterNewRecord()
        {
            Console.WriteLine("\nEnter record values\n");

            string[] values = new string[11];
            for(int i = 0; i < 11; i++)
            {
                Console.Write($"{controller.ColNames[i]} = ");
                values[i] = Console.ReadLine();
                while (!Validator(i, values[i]))
                {
                    Console.Write($"Invalid input, please try again\n\n{controller.ColNames[i]} = ");
                    values[i] = Console.ReadLine();
                }

                Console.Write("\n");
            }
            return values;
        }

        /// <summary>
        /// Prints a single record as a three column table
        /// </summary>
        /// <param name="id">Index of the record to be printed</param>
        public void PrintRecordDetails(int id)
        {
            string[] colNames = controller.ColNames;
            string[] colValues = controller.DataAsArray(id);
            table = new("#", "Name", "Value");
            for (int i = 0; i < 11; i++)
                table.AddRow(i, colNames[i], colValues[i]);
            table.Write(Format.Alternative);
        }

        /// <summary>
        /// Ensures data integrity when new entries are being made or existing entries are being edited
        /// </summary>
        /// <param name="index">The index of the entry in the string array (11 indexes in total)</param>
        /// <param name="input">The value to check</param>
        /// <returns></returns>
        public bool Validator(int index, string input)
        {
            try
            {
                return index switch
                {
                    // Checking if input is in integer form and is not negative
                    0 or 5 or 6 or 7 => (int.Parse(input) >= 0),
                    // Checking if input is in float for proportion related fields and confines them to a range between 0 and 1
                    8 or 9 or 10 => (double.Parse(input) >= 0 && double.Parse(input) <= 1),
                    // Checking for blank input in case the field is supposed to be a string
                    _ => !string.IsNullOrEmpty(input),
                };
            }

            // If conversion error occurs, return false and subsequently prompt user to try again
            catch(FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// Adds an ID column to the front of the Column Names <see cref="VaccineControl.ColNames"/>
        /// </summary>
        /// <returns>The array with the added ID column</returns>
        public string[] GetColumnNamesWithId()
        {
            return new[] { "ID" }.Concat(controller.ColNames).ToArray();
        }

        /// <summary>
        /// Adds a column to the front of the row which displays the element's position in the list
        /// </summary>
        /// <param name="id">Index of the record</param>
        /// <returns>The array with the added ID column</returns>
        public string[] GetRowsWithId(int id)
        {
            return new[] { $"{id}" }.Concat(controller.DataAsArray(id)).ToArray();
        }
    }
}